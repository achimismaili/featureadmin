using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Repository
{
    public interface IFeatureRepository 
    {
        string AddActivatedFeature(ActivatedFeature feature);

        void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions);

        void AddLoadedLocations(Core.Messages.Completed.LocationsLoaded message);

        void Clear();

        ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition);

        IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location);

        IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location);

        IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location);

        // void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures);
        bool IsFeatureActivated(Guid featureDefinitionId, Guid? locationId = null);

        string RemoveActivatedFeature(Guid featureId, Guid locationId);

        //IEnumerable<ActivatedFeature> SearchActivatedFeatures();
        IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter, bool? onlyFarmFeatures);
        IEnumerable<Location> SearchLocations(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);
    }
}
