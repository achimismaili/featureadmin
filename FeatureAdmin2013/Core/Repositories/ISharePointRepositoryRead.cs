using FeatureAdmin.Core.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Contracts.Repositories
{
    public interface ISharePointRepositoryRead
    {
        Task<ILocation> GetFarmAsync();

        Task<ICollection<ILocation>> GetWebAppsAsync();

        Task<ICollection<ILocation>> GetSitesAsync(ILocation webApp);

        Task<ICollection<ILocation>> GetWebsAsync(ILocation site);

        Task<IActivatedFeature> GetActivatedFeaturesAsync();
        Task<IFeatureDefinition> GetFeatureDefinitionsAsync();
    }
}
