using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Repository
{
    public interface IFeatureRepository 
    {
        string AddActivatedFeature(ActivatedFeature feature);

        void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions);

        void AddLoadedLocations(LoadedDto message);
        
        void Clear();

        ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location);

        IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location);

        IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location);
        string RemoveActivatedFeature(Guid featureId, Guid locationId);

        IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures);
        IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);

        IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToCleanUp();

        IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(IEnumerable<ActivatedFeatureSpecial> source , string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);

        IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToUpgrade();
    }
}
