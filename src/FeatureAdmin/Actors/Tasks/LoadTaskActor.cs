using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Repository;
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
        public int ActivatedFeaturesLoaded { get; private set; }
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IActorRef featureDefinitionActor;
        private readonly Dictionary<Guid, IActorRef> locationActors;
        private readonly IFeatureRepository repository;

        public LoadTaskActor(IEventAggregator eventAggregator, IFeatureRepository repository,
            string title, Guid id, Location startLocation)
            : base(eventAggregator, title, id)
        {
            locationActors = new Dictionary<Guid, IActorRef>();
            this.repository = repository;
            featureDefinitionActor =
                   Context.ActorOf(Context.DI().Props<FeatureDefinitionActor>());

            ActivatedFeaturesLoaded = 0;
       
            FarmFeatureDefinitions = new ProgressModule(
                5d / 100,
                0d,
                1);

            Farm = new ProgressModule(
                5d / 100,
                FarmFeatureDefinitions.MaxCumulatedQuota,
                1);
            WebApps = new ProgressModule(
                10d / 100,
                Farm.MaxCumulatedQuota);

            SitesAndWebs = new ProgressModule(
               1d - WebApps.MaxCumulatedQuota,
                WebApps.MaxCumulatedQuota);

            Receive<LocationsLoaded>(message => HandleLocationsLoaded(message));
            Receive<FarmFeatureDefinitionsLoaded>(message => FarmFeatureDefinitionsLoaded(message));

            InitiateLoadTask(startLocation);
        }

        public override string StatusReport
        {
            get
            {
                return string.Format("'{0}' (ID: '{1}') - Loaded: {2} web apps, {3} sites and webs, {4} feature definitions, {7} activated features, progress {5:F0}% \nelapsed time: {6}",
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

        /// <summary>
        /// Props provider
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// see also https://getakka.net/articles/actors/receive-actor-api.html
        /// </remarks>
        public static Props Props(IEventAggregator eventAggregator, IFeatureRepository repository,
            string title, Guid id, Location startLocation)
        {
            return Akka.Actor.Props.Create(() => new LoadTaskActor(eventAggregator, repository, title, id, startLocation));
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
                    // minus one, because farm is contained in child count
                    WebApps.Total += location.ChildCount - 1;
                    Farm.Processed++;
                    break;
                case Enums.Scope.ScopeInvalid:
                default:
                    // do not track non valid scopes
                    break;
            }
        }

        public bool TrackLocationsProcessed(LocationsLoaded loadedMessage)
        {
            bool finished = false;

            var parent = loadedMessage.Parent;

            foreach (Location l in loadedMessage.ChildLocations)
            {
                TrackLocationProcessed(l);
            }

            FarmFeatureDefinitions.Processed += loadedMessage.Definitions.Count();

            ActivatedFeaturesLoaded += loadedMessage.ActivatedFeatures.Count();

            return finished;
        }

        private void FarmFeatureDefinitionsLoaded(FarmFeatureDefinitionsLoaded message)
        {

            FarmFeatureDefinitions.Processed = message.FarmFeatureDefinitions.Count();
            
            if (FarmFeatureDefinitions.Completed)
            {
                repository.AddFeatureDefinitions(message.FarmFeatureDefinitions);
                SendProgress();
            }
        }

        private void HandleLocationsLoaded([NotNull] LocationsLoaded message)
        {
            TrackLocationsProcessed(message);

            // if web apps are loaded, load children
            if (message.Parent.Scope == Enums.Scope.Farm)
            {
                foreach (Location l in message.ChildLocations)
                {
                    if (l.Scope == Enums.Scope.WebApplication)
                    {
                        // initiate read of locations
                        var loadWebAppChildren = new LoadLocationQuery(Id, l);
                        LoadTask(loadWebAppChildren);
                    }
                }
            }

            repository.AddLoadedLocations(message);

            SendProgress();
        }

        private void InitiateLoadTask(Location startLocation)
        {
            repository.Clear();

            // initiate read of all feature definitions
            var fdQuery = new Messages.Request.LoadFeatureDefinitionQuery(Id);
            featureDefinitionActor.Tell(fdQuery);

            // initiate read of locations
            var loadQuery = new LoadLocationQuery(Id, startLocation);
            LoadTask(loadQuery);
        }

        private void LoadTask([NotNull] LoadLocationQuery loadQuery)
        {
            _log.Debug("Entered LoadTask");

            var locationId = loadQuery.Location.Id;

            if (!locationActors.ContainsKey(locationId))
            {
                IActorRef newLocationActor =
                    Context.ActorOf(Context.DI().Props<LocationActor>());

                locationActors.Add(locationId, newLocationActor);

                newLocationActor.Tell(loadQuery);
            }
            else
            {
                locationActors[locationId].Tell(loadQuery);
            }
        }
    }
}
