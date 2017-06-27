using FeatureAdmin.SharePointFarmService.Interfaces;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace FeatureAdmin.SharePointFarmService
{
    public class WebApplication : BaseSharePointContainer
    {
        public WebApplication(SPWebApplication webApp)
        {
            featureCollection = webApp.Features;
        }
    }
}
