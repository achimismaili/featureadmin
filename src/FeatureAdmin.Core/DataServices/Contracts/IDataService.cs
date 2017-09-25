using FeatureAdmin.Core.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.DataServices.Contracts
{
    public interface IDataService
    {
        IEnumerable<IFeatureDefinition> FeatureDefinitions { get; }
        IEnumerable<ILocation> Locations { get; }
        IEnumerable<IActivatedFeature> ActivatedFeatures { get; }

        Task<int> ActivateAllFeaturesWithinSharePointContainerAsync(ILocation sharePointContainer, IEnumerable<Guid> featureDefinitions, bool force, out Exception exception);

        Task<int> DeactivateAllFeaturesWithinSharePointContainerAsync(ILocation sharePointContainer, IEnumerable<Guid> featureDefinitions, bool force, out Exception exception);

        Task<int> DeactivateAllFeaturesAsync(IEnumerable<IActivatedFeature> features, bool force, out Exception exception);

        Task<int> UninstallAsync(Guid featureDefinitionId, int compatibilityLevel, bool force, out Exception exception);
    }
}
