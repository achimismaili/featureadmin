using FeatureAdmin.SharePointFarmService.Interfaces;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public class SiteCollection: BaseSharePointContainer, IDisposable
    {
        private SPSite spSite;
        public SiteCollection(SPSite siteCollection)
        {
            spSite = siteCollection;
            featureCollection = siteCollection.Features;
        }

        public void Dispose()
        {
            spSite.Dispose();
        }
    }
}
