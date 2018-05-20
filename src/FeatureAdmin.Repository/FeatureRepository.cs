using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
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

        public FeatureRepository()
        {
            var config = new EngineConfiguration();
            config.PersistenceMode = PersistenceMode.ManualSnapshots;
            store = Db.For<FeatureModel>(config);
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
            store.AddLocations(message.ChildLocations);
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
