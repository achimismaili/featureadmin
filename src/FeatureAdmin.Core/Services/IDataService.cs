using FeatureAdmin.Core.Models;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Services
{
    public interface IDataService
    {
       

        IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location);

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        IEnumerable<Location> LoadFarmAndWebApps();
        //int FeatureToggle(Models.SPObject location, bool add, Guid featureId, bool force);
        //int FeatureUninstall(Guid featureId, bool force);

        //int FeatureUpdate(Models.SPObject location, bool add, Guid featureId, bool force);

    }
}
