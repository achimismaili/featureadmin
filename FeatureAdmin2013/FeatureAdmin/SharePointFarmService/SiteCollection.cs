using FeatureAdmin.SharePointFarmService.Interfaces;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public class SiteCollection: IActivatedFeatureService
    {
        private SPFeatureCollection featureCollection;

        public SPFeature GetActivatedFeature(Guid Id)
        {
            if (featureCollection != null || Id == Guid.Empty)
            {
                return null;
            }
            return featureCollection.FirstOrDefault(f => f.DefinitionId == Id);
        }

        public SPFeatureCollection GetAllActivatedFeatures()
        {
            if (featureCollection != null)
            {
                return null;
            }

            return featureCollection;
        }

        public Web(SPWeb web)
        {
            featureCollection = web.Features;
        }

        public SiteCollection(SPSite siteCollection)
        {
            siCo = siteCollection;
        }
}
}
