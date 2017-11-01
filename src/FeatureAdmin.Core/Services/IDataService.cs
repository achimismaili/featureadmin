using FeatureAdmin.Core.Models;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Services
{
    public interface IDataService
    {
        IEnumerable<ActivatedFeature> LoadActivatedFeatures(SPLocation location);

        IEnumerable<SPLocation> LoadChildLocations(SPLocation parentLocation);

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        SPLocation LoadLocation(Location location);
        //int FeatureToggle(Models.SPObject location, bool add, Guid featureId, bool force);
        //int FeatureUninstall(Guid featureId, bool force);

        //int FeatureUpdate(Models.SPObject location, bool add, Guid featureId, bool force);

    }
}
