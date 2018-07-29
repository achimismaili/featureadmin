using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Repository;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Repository
{
    /// <summary>
    /// The feature repository manages all locations, feautre definitions and activated features
    /// in a in memory database and also talks to SharePoint via the SharePoint Backends
    /// </summary>
    public class FeatureRepository : IFeatureRepository
    {
        public OrigoDb.FeatureModel store;

        private readonly IEventAggregator eventAggregator;


        public FeatureRepository(IEventAggregator eventAggregator
)
        {
            var config = new EngineConfiguration();
            config.PersistenceMode = PersistenceMode.ManualSnapshots;
            // see https://github.com/DevrexLabs/OrigoDB/issues/24
            config.SetCommandStoreFactory(cfg => new OrigoDB.Core.Test.InMemoryCommandStore(cfg));
            config.SetSnapshotStoreFactory(cfg => new OrigoDB.Core.Test.InMemorySnapshotStore(cfg));
            store = Db.For<OrigoDb.FeatureModel>(config);

            this.eventAggregator = eventAggregator;
        }

        public string AddActivatedFeature([NotNull] ActivatedFeature feature)
        {
            return store.AddActivatedFeature(feature);
        }

        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions)
        {
            store.AddFeatureDefinitions(featureDefinitions);
        }

        //public void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures)
        //{
        //    store.AddActivatedFeatures(activatedFeatures);
        //}

        public void AddLoadedLocations(LoadedDto message)
        {
             var error = store.AddLoadedLocations(message);

            if (!string.IsNullOrEmpty(error))
            {
                var logMsg = new Messages.LogMessage(Core.Models.Enums.LogLevel.Error, error);

                eventAggregator.PublishOnUIThread(logMsg);
            }
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

        public IEnumerable<Location> GetLocationsCanActivate([NotNull] FeatureDefinition featureDefinition, [NotNull] Location location)
        {
            return store.GetLocationsCanActivate(featureDefinition, location);
        }

        public IEnumerable<Location> GetLocationsCanDeactivate([NotNull] FeatureDefinition featureDefinition, [NotNull] Location location)
        {
            return store.GetLocationsCanDeactivate(featureDefinition, location);
        }
        public string RemoveActivatedFeature([NotNull] Guid featureId, [NotNull] Guid locationId)
        {
            return store.RemoveActivatedFeature(featureId, locationId);
        }

        public IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures)
        {
            return store.SearchFeatureDefinitions(searchInput, selectedScopeFilter, onlyFarmFeatures);
        }

        public IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(
            IEnumerable<ActivatedFeatureSpecial> source,
            string searchInput, 
            Scope? selectedScopeFilter)
        {
            return store.SearchSpecialFeatures(source, searchInput, selectedScopeFilter);
        }

        public IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToUpgrade()
        {
            return store.GetAllFeaturesToUpgrade();
        }

        public IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToCleanUp()
        {
            return store.GetAllFeaturesToCleanUp();
        }

        public IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter)
        {
            return store.SearchLocations(searchInput, selectedScopeFilter);
        }

        public IEnumerable<ActivatedFeatureSpecial> GetAsActivatedFeatureSpecial(IEnumerable<ActivatedFeature> activatedFeatures)
        {
            return store.GetAsActivatedFeatureSpecial(activatedFeatures);
        }

        public string RemoveFeatureDefinition(string uniqueIdentifier)
        {
            return store.RemoveFeatureDefinition(uniqueIdentifier);
        }
    }
}
