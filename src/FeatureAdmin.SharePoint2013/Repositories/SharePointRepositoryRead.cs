using FeatureAdmin.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Contracts;

namespace FeatureAdmin.SharePoint2013.Repositories
{
    public class SharePointRepositoryRead : ISharePointRepositoryRead
    {
        public Task<IActivatedFeature> GetActivatedFeaturesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ILocation> GetFarmAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IFeatureDefinition> GetFeatureDefinitionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ILocation>> GetSitesAsync(ILocation webApp)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ILocation>> GetWebAppsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ILocation>> GetWebsAsync(ILocation site)
        {
            throw new NotImplementedException();
        }
    }
}
