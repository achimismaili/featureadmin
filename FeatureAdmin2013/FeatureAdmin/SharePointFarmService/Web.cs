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
        public Web(SPWeb web)
        {
            featureCollection = web.Features;
        }
    }
}
