using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class LoadTaskActor : BaseTaskActor
    {
        public ProgressModule Farm;
        public ProgressModule FarmFeatureDefinitions;
        public ProgressModule Preparations;
        public ProgressModule Sites;
        public ProgressModule WebApps;
        public ProgressModule Webs;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IActorRef featureDefinitionActor;
        private readonly Dictionary<Guid, IActorRef> locationActors;
        private bool DefinitionLoadReady = false;
        private bool PreparationReady = false;
        private FarmFeatureDefinitionsLoaded tempFeatureDefinitionStore = null;
        private List<LocationsLoaded> tempLocationStore = new List<LocationsLoaded>();
        public LoadTaskActor(IEventAggregator eventAggregator, string title, Guid id, Location startLocation)
            : base(eventAggregator, title, id)
        {
            locationActors = new Dictionary<Guid, IActorRef>();

            featureDefinitionActor =
                   Context.ActorOf(Context.DI().Props<FeatureDefinitionActor>());

            Preparations = new ProgressModule(
                5d / 100,
                0d,
                FeatureAdmin.Common.Constants.Tasks.PreparationStepsForLoad);

            FarmFeatureDefinitions = new ProgressModule(
                5d / 100,
                Preparations.MaxCumulatedQuota);

            Farm = new ProgressModule(
                5d / 100,
                FarmFeatureDefinitions.MaxCumulatedQuota,
                1);
            WebApps = new ProgressModule(
                10d / 100,
                Farm.MaxCumulatedQuota);
            
            Sites = new ProgressModule(
                40d / 100, 
                WebApps.MaxCumulatedQuota);

            Webs = new ProgressModule(
                1d - Sites.MaxCumulatedQuota, 
                Sites.MaxCumulatedQuota);


            Receive<ClearItemsReady>(message => HandleClearItemsReady(message));
            Receive<LocationsLoaded>(message => HandleLocationsLoaded(message));
            Receive<FarmFeatureDefinitionsLoaded>(message => FarmFeatureDefinitionsLoaded(message));

            InitiateLoadTask(startLocation);
        }

        public override string StatusReport
        {
            get
            {
                return string.Format("'{0}' (ID: '{1}') - Loaded: {2} web apps, {3} site collections, {4} webs, {5} features, progress {6:F0}% \nelapsed time: {7}",
                    Title,
                    Id,
                    WebApps.Processed,
                    Sites.Processed,
                    Webs.Processed ,
                    FarmFeatureDefinitions.Processed,
                    PercentCompleted * 100,
                    (End == null ? DateTime.Now : End.Value).Subtract(
                        Start == null ? DateTime.Now : Start.Value).ToString("c")
                    );
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
        public static Props Props(IEventAggregator eventAggregator, string title, Guid id, Location startLocation)
        {
            return Akka.Actor.Props.Create(() => new LoadTaskActor(eventAggregator, title, id, startLocation));
        }

        public void HandleClearItemsReady(ClearItemsReady message)
        {

            // how many steps are expected is decided in Common.Constants.Tasks.PreparationStepsForLoad
            var preparationReady = TrackPreparationsProcessed(1);

            if (preparationReady)
            {
                PreparationReady = true;
                SendProgress();

                // feature definitions already loaded?
                if (tempFeatureDefinitionStore != null)
                {
                    eventAggregator.BeginPublishOnUIThread(tempFeatureDefinitionStore);
                    tempFeatureDefinitionStore = null;
                }

                // locations already loaded? 
                if (DefinitionLoadReady && tempLocationStore.Count() > 0)
                {
                    foreach (LocationsLoaded loadedMsg in tempLocationStore)
                    {
                        eventAggregator.BeginPublishOnUIThread(loadedMsg);
                    }

                    tempLocationStore.Clear();
                }

            }

        }

        public bool TrackFeatureDefinitionsProcessed(int featuresProcessed)
        {
            return TrackItemsProcessed(featuresProcessed, ref FarmFeatureDefinitions);
        }

        public bool TrackLocationProcessed([NotNull] Location location)
        {
            switch (location.Scope)
            {
                case Enums.Scope.Web:
                    return TrackItemsProcessed(1, ref Webs);
                case Enums.Scope.Site:
                    UpdateExpectedItems(0, 0, location.ChildCount, 0, 0, 0);
                    return TrackItemsProcessed(1, ref Sites);
                case Enums.Scope.WebApplication:
                    UpdateExpectedItems(0, 0, 0, location.ChildCount, 0, 0);
                    return TrackItemsProcessed(1, ref WebApps);
                case Enums.Scope.Farm:
                    UpdateExpectedItems(0, 0, 0, 0, location.ChildCount, 0);
                    return TrackItemsProcessed(1, ref Farm);
                case Enums.Scope.ScopeInvalid:
                default:
                    // do not track non valid scopes
                    return false;
            }
        }

        public bool TrackLocationsProcessed([NotNull] IEnumerable<Location> locations)
        {
            bool finished = false;
            foreach (Location l in locations)
            {
                var finishedThis = TrackLocationProcessed(l);

                finished = finishedThis ? true : finished;
            }

            return finished;
        }

        /// <summary>
        /// increments preparation steps processed
        /// </summary>
        /// <param name="preparationSteps">number of processed preparation steps</param>
        /// <returns>true, if all preparation steps have been processed</returns>
        public bool TrackPreparationsProcessed(int preparationSteps)
        {
            return TrackItemsProcessed(preparationSteps, ref Preparations);

        }

        private void FarmFeatureDefinitionsLoaded(FarmFeatureDefinitionsLoaded message)
        {

            var stepReady = TrackFeatureDefinitionsProcessed(message.FarmFeatureDefinitions.Count());
            if (stepReady)
            {
                DefinitionLoadReady = true;
                SendProgress();

                if (PreparationReady)
                {
                    eventAggregator.PublishOnUIThread(message);

                    // locations already loaded? 
                    if (DefinitionLoadReady && tempLocationStore.Count() > 0)
                    {
                        foreach (LocationsLoaded loadedMsg in tempLocationStore)
                        {
                            eventAggregator.BeginPublishOnUIThread(loadedMsg);
                        }

                        tempLocationStore.Clear();
                    }
                }
                else
                {
                    tempFeatureDefinitionStore = message;
                }


            }
        }

        private void HandleLocationsLoaded([NotNull] LocationsLoaded message)
        {
            var stepReady = TrackLocationsProcessed(message.Locations);

            if (stepReady)
            {
                SendProgress();
                var dbgMsg = new FeatureAdmin.Messages.LogMessage(Core.Models.Enums.LogLevel.Debug,
                    string.Format("Debug Load progress: {0}", StatusReport)
                    );
                eventAggregator.PublishOnUIThread(dbgMsg);

                if (PreparationReady && DefinitionLoadReady)
                {
                    // publish locations to wpf
                    eventAggregator.PublishOnUIThread(message);
                }
                else
                {
                    tempLocationStore.Add(message);
                }
            }
            else
            {
                //// for web applications, load children
            }

            SendProgress();
        }

        private void InitiateLoadTask(Location startLocation)
        {
            // initiate clean all feature definition and location collections
            var clearMessage = new ClearItems(Id);
            eventAggregator.PublishOnUIThread(clearMessage);

            // initiate read of all feature definitions
            var fdQuery = new LoadFeatureDefinitionQuery(Id);
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
        /// <summary>
        /// tracks / increments items processed
        /// </summary>
        /// <param name="itemsProcessedNew">processed items to increment</param>
        /// <param name="progModule">a ProgressModule where to get the values</param>
        /// <returns>true, if all items have been processed (--> if processed reached total)</returns>
        private bool TrackItemsProcessed(int itemsProcessedNew, ref ProgressModule progModule)
        {
            progModule.Processed += itemsProcessedNew;

            if (progModule.Processed > progModule.Total)
            {
                IncrementProgress(progModule.Quota, progModule.MaxCumulatedQuota);
            }
            else
            {
                IncrementProgress(
                    progModule.Quota * progModule.Processed / progModule.Total,
                    progModule.MaxCumulatedQuota);
            }

            return progModule.Processed >= progModule.Total;
        }
        private void UpdateExpectedItems(int prepSteps, int features, int webs, int sites, int webApps, int farms)
        {
            Preparations.Total += prepSteps;
            FarmFeatureDefinitions.Total += features;
            Webs.Total += webs;
            Sites.Total += sites;
            WebApps.Total += webApps;
            Farm.Total += farms;
        }


        private void UpdateProcessedItems(int features, int webs, int sites, int webApps, int farms)
        {
            FarmFeatureDefinitions.Processed += features;
            Webs.Processed += webs;
            Sites.Processed += sites;
            WebApps.Processed += webApps;
            Farm.Processed += farms;
        }
    }
}
