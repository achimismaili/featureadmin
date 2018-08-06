using FeatureAdmin.Core.Models;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Repository
{
    public interface IFeatureRepository 
    {
        string AddActivatedFeature(ActivatedFeature feature);

        void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions);

        void AddLoadedLocations(LoadedDto message);
        
        void Clear();

        ActivatedFeature GetActivatedFeature(string featureDefinitionId, string locationId);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location);

        IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToCleanUp();

        IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToUpgrade();

        IEnumerable<ActivatedFeatureSpecial> GetAsActivatedFeatureSpecial(IEnumerable<ActivatedFeature> activatedFeatures);
        IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location);

        IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location);
        string RemoveActivatedFeature(string featureId, string locationId);
        string RemoveFeatureDefinition(string uniqueIdentifier);
        IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures);
        IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);
        IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(IEnumerable<ActivatedFeatureSpecial> source , string searchInput, Models.Enums.Scope? selectedScopeFilter);
    }
}
