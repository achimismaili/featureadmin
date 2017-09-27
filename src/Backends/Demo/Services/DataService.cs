using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.DataServices.Contracts;

namespace FeatureAdmin.Backends.Demo.Services
{
    public class DataService : IDataService
    {
        public IEnumerable<IActivatedFeature> ActivatedFeatures
        {
            get
            {
                return SampleData.SampleActivatedFeatures.GetActivatedFeatures(Locations);
            }
        }

        public IEnumerable<IFeatureDefinition> FeatureDefinitions
        {
            get
            {
                return SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions();
            }
        }

        public IEnumerable<ILocation> Locations
        {
            get
            {
                return SampleData.SampleLocationHierarchy.GetAllLocations();
            }
        }

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
