using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.Repositories;
using FeatureAdmin.Core.Repositories.Contracts;

namespace FeatureAdmin.Repositories.Demo
{
    public class DemoRepository : IRepository
    {
        public Task<int> ActivateAllFeaturesWithinSharePointContainerAsync(ILocation sharePointContainer, IEnumerable<Guid> featureDefinitions, bool force, out Exception exception)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeactivateAllFeaturesAsync(IEnumerable<IActivatedFeature> features, bool force, out Exception exception)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeactivateAllFeaturesWithinSharePointContainerAsync(ILocation sharePointContainer, IEnumerable<Guid> featureDefinitions, bool force, out Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LoadFarm()
        {
            throw new NotImplementedException();
        }

        public Task<int> UninstallAsync(Guid featureDefinitionId, int compatibilityLevel, bool force, out Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
