using Caliburn.Micro;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.OrigoDb;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    /// <summary>
    /// The feature repository manages all locations, feautre definitions and activated features
    /// in a in memory database and also talks to SharePoint via the SharePoint Backends
    /// </summary>
    public class FeatureRepository : IFeatureRepository
    {
        public FeatureModel store;

        private readonly IEventAggregator eventAggregator;


        public FeatureRepository(IEventAggregator eventAggregator
)
        {
            var config = new EngineConfiguration();
            config.PersistenceMode = PersistenceMode.ManualSnapshots;
            // see https://github.com/DevrexLabs/OrigoDB/issues/24
            // config.SetCommandStoreFactory(cfg => new OrigoDB.Core.Test.InMemoryCommandStore(cfg));
            config.SetSnapshotStoreFactory(cfg => new OrigoDB.Core.Test.InMemorySnapshotStore(cfg));
            store = Db.For<FeatureModel>(config);

            this.eventAggregator = eventAggregator;
        }

        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions)
        {
            store.AddFeatureDefinitions(featureDefinitions);
        }

        //public void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures)
        //{
        //    store.AddActivatedFeatures(activatedFeatures);
        //}

        public void AddLoadedLocations(LocationsLoaded message)
        {
            var error = store.AddLocations(message.ChildLocations);

            if (!string.IsNullOrEmpty(error))
            {
                var logMsg = new Messages.LogMessage(Core.Models.Enums.LogLevel.Error, error);

                eventAggregator.PublishOnUIThread(logMsg);
            }

            store.AddActivatedFeatures(message.ActivatedFeatures);

            store.AddFeatureDefinitions(message.Definitions);
        }

        /// <summary>
        /// Initial load and also reload of all feature definitions, locations and activated features
        /// </summary>
        public void Clear()
        {
            store.Clear();
        }

        public ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId)
        {
            return store.GetActivatedFeature(featureDefinitionId, locationId);
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location)
        {
            return store.GetActivatedFeatures(location);
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition)
        {
            return store.GetActivatedFeatures(featureDefinition);
        }

        public bool IsFeatureActivated(Guid featureDefinitionId, Guid? locationId = null)
        {
            return store.IsFeatureActivated(featureDefinitionId, locationId);
        }

        public IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures)
        {
            return store.SearchFeatureDefinitions(searchInput, selectedScopeFilter, onlyFarmFeatures);
        }
        public IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter)
        {
            return store.SearchLocations(searchInput, selectedScopeFilter);
        }
    }
}
