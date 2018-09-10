using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class LoadTaskActor : BaseTaskActor
    {
        public ProgressModule Farm;
        public ProgressModule FarmFeatureDefinitions;
        public ProgressModule SitesAndWebs;
        public ProgressModule WebApps;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IDataService dataService;
        private readonly IActorRef farmLoadActor;
        private readonly IActorRef featureDefinitionActor;
        private readonly Dictionary<string, IActorRef> locationActors;
        private readonly IFeatureRepository repository;
        private bool elevatedPrivileges = false;
        public LoadTaskActor(
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService,
            Guid id)
            : base(eventAggregator, id)
        {
            locationActors = new Dictionary<string, IActorRef>();
            this.repository = repository;
            this.dataService = dataService;

            featureDefinitionActor = Context.ActorOf(
                FeatureDefinitionActor.Props(dataService), "FeatureDefinitionActor");

            farmLoadActor = Context.ActorOf(
                LocationActor.Props(dataService), "FarmLoadActor");

            ActivatedFeaturesLoaded = 0;

            FarmFeatureDefinitions =
                new ProgressModule(5d / 100, 0d, 1);

            Farm = new ProgressModule(5d / 100,
                FarmFeatureDefinitions.MaxCumulatedQuota, 1);

            WebApps = new ProgressModule(10d / 100,
                Farm.MaxCumulatedQuota);

            SitesAndWebs = new ProgressModule(
               1d - WebApps.MaxCumulatedQuota,
                WebApps.MaxCumulatedQuota);

            Receive<LoadTask>(message => InitiateLoadTask(message));
            Receive<LoadedDto>(message => ReceiveLocationsLoaded(message));
            Receive<FarmFeatureDefinitionsLoaded>(message => FarmFeatureDefinitionsLoaded(message));
        }

        public int ActivatedFeaturesLoaded { get; private set; }
        public override double PercentCompleted
        {
            get
            {
                return
                    FarmFeatureDefinitions.OuotaPercentage +
                    Farm.OuotaPercentage +
                    WebApps.OuotaPercentage +
                    SitesAndWebs.OuotaPercentage;
            }
        }

        public override string StatusReport
        {
            get
            {
                return string.Format("'{0}' (TaskID: '{1}') - Loaded: {2} web apps, {3} sites and webs, {4} feature definitions, {7} activated features, progress {5:F0}% \nelapsed time: {6}",
                    Title,
                    Id,
                    WebApps.Processed,
                    SitesAndWebs.Processed,
                    FarmFeatureDefinitions.Processed,
                    PercentCompleted * 100,
                    ElapsedTime,
                    ActivatedFeaturesLoaded
                    );
            }
        }
        /// <summary>
        /// Props provider
        /// </summary>
        /// <param name="eventAggregator">the event aggregator</param>
        /// <param name="repository">feature repository</param>
        /// <param name="dataService">SharePoint data service</param>
        /// <param name="id">task Id</param>
        /// <returns></returns>
        /// <remarks>
        /// see also https://getakka.net/articles/actors/receive-actor-api.html
        /// </remarks>
        public static Props Props(
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService,
            Guid id)
        {
            return Akka.Actor.Props.Create(() => new LoadTaskActor(
                eventAggregator,
                repository,
                dataService,
                id));
        }

        public void TrackLocationProcessed([NotNull] Location location)
        {
            switch (location.Scope)
            {
                case Enums.Scope.Web:
                    SitesAndWebs.Processed++;
                    break;
                case Enums.Scope.Site:
                    SitesAndWebs.Total += location.ChildCount;
                    SitesAndWebs.Processed++;
                    break;
                case Enums.Scope.WebApplication:
                    SitesAndWebs.Total += location.ChildCount;
                    WebApps.Processed++;
                    break;
                case Enums.Scope.Farm:
                    WebApps.Total += location.ChildCount;
                    Farm.Processed++;
                    break;
                case Enums.Scope.ScopeInvalid:
                default:
                    // do not track non valid scopes
                    break;
            }
        }

        public bool TrackLocationsProcessed(LoadedDto loadedMessage)
        {
            bool finished = false;

            var parent = loadedMessage.Parent;

            foreach (Location l in loadedMessage.ChildLocations)
            {
                TrackLocationProcessed(l);
            }

            FarmFeatureDefinitions.Processed += loadedMessage.Definitions.Count();

            var features = loadedMessage.ActivatedFeatures;

            if (features != null)
            {
                ActivatedFeaturesLoaded += features.Count();

                if (features.Any(f => f.Faulty))
                {
                    foreach (var f in features.Where(f => f.Faulty))
                    {
                        LogToUi(
                            Enums.LogLevel.Warning,
                            string.Format(
                                "Unclean feature found: {0}",
                                f.ToString()
                                )
                            );
                    }
                }
            }


            return finished;
        }

        /// <summary>
        /// send cancelation message to all sub actors
        /// </summary>
        /// <param name="cancelMessage"></param>
        protected override void HandleCancelation(FeatureAdmin.Messages.CancelMessage cancelMessage)
        {
            farmLoadActor.Tell(cancelMessage);

            featureDefinitionActor.Tell(cancelMessage);

            foreach (var featureActor in locationActors.Values)
            {
                featureActor.Tell(cancelMessage);
            }
        }

        private void FarmFeatureDefinitionsLoaded(FarmFeatureDefinitionsLoaded message)
        {

            var definitions = message.FarmFeatureDefinitions;

            FarmFeatureDefinitions.Processed = definitions.Count();

            //TODO: check if this can go away, why should it not be completed here?
            if (FarmFeatureDefinitions.Completed)
            {
                repository.AddFeatureDefinitions(definitions);
                SendProgress();

                if (definitions.Any(fd => fd.Scope == Enums.Scope.ScopeInvalid))
                {
                    foreach (var fd in definitions.Where(fd => fd.Scope == Enums.Scope.ScopeInvalid))
                    {
                        LogToUi(
                            Enums.LogLevel.Warning,
                            string.Format(
                                "Invalid feature definition found: {0}",
                                fd.ToString()
                                )
                            );
                    }
                }
            }
        }

        private void InitiateLoadTask(LoadTask loadTask)
        {
            if (!TaskCanceled)
            {
                this.elevatedPrivileges = loadTask.ElevatedPrivileges.Value;

                Start = DateTime.Now;

                Title = loadTask.Title;

                // cleanup repository
                if (loadTask.StartLocation.Scope == Enums.Scope.Farm)
                {
                    // clear repository synchronous before loading
                    repository.Clear();

                    // it could take some time to clear the repository, and errors could happen, 
                    // so the task could be canceled meanwhile ...
                    if (!TaskCanceled)
                    {
                        // initiate read of all feature definitions
                        var fdQuery = new Messages.Request.LoadFeatureDefinitionQuery(Id);


                        featureDefinitionActor.Tell(fdQuery);


                        // initiate read of farm location, start location is null
                        var loadFarm = new LoadChildLocationQuery(Id, null, elevatedPrivileges);

                        farmLoadActor.Tell(loadFarm);

                    }
                }
                else
                {
                    //TODO: clear only start location and belonging activatedfeatures and definitions ...
                    throw new NotImplementedException("As start location for load task, currently only farm level is supported.");
                }

                var loadChildren = new LoadChildLocationQuery(
                        Id,
                        loadTask.StartLocation,
                        elevatedPrivileges);

                // load web applications as farm children
                ReceiveLoadChildrenTask(loadChildren);
            }
        }

        private void ReceiveLoadChildrenTask([NotNull] LoadChildLocationQuery loadQuery)
        {
            if (!TaskCanceled)
            {
                _log.Debug("Entered LoadTask");

                if (loadQuery.Location == null)
                {
                    throw new ArgumentException("The location must not be empty ");
                }

                var akkaFriendlyActorId = loadQuery.Location.UniqueId.Replace('/', '_');

                if (!locationActors.ContainsKey(akkaFriendlyActorId))
                {
                    IActorRef newLocationActor =
                      Context.ActorOf(LocationActor.Props(
                      dataService), akkaFriendlyActorId);

                    locationActors.Add(akkaFriendlyActorId, newLocationActor);
                }

                locationActors[akkaFriendlyActorId].Tell(loadQuery);
            }
        }

        private void ReceiveLocationsLoaded([NotNull] LoadedDto message)
        {
            TrackLocationsProcessed(message);

            // if web apps are loaded, load children
            if (message.Parent != null && message.Parent.Scope == Enums.Scope.Farm)
            {
                foreach (Location l in message.ChildLocations)
                {
                    //TODO: Here, it could be checked, if a site collection is e.g. locked or readonly

                    if (l.Scope == Enums.Scope.WebApplication)
                    {
                        // initiate read of locations
                        var loadWebAppChildren = new LoadChildLocationQuery(Id, l, elevatedPrivileges);
                        ReceiveLoadChildrenTask(loadWebAppChildren);
                    }
                }
            }

            repository.AddLoadedLocations(message);

            SendProgress();
        }
    }
}
