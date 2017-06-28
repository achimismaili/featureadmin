using FeatureAdmin.SharePointFarmService.Interfaces;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public class Web : BaseSharePointContainer
    {
        private SPWeb spWeb;
        public Web(SPWeb web)
        {
            spWeb = web;
            featureCollection = web.Features;
        }

        public void Dispose()
        {
            spWeb.Dispose();
        }
    }
}
