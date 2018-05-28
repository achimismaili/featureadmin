using FeatureAdmin.Core.Models;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    public interface IFeatureRepository 
    {
        //IEnumerable<ActivatedFeature> SearchActivatedFeatures();
        IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures);
        IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);

        void AddLoadedLocations(Core.Messages.Completed.LocationsLoaded message);
        void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions);
        // void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures);

        void Clear();
        bool IsFeatureActivated(Guid featureDefinitionId, Guid? locationId = null);
        ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId);
        IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition);
        IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location);
        IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location);
        IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location);
    }
}
