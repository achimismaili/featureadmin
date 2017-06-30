using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Services.SharePointFarmService
{
    public static class ReadViaSharePointApi
    {
        /// <summary>
        /// Get the farm FeatureParent to retrieve SPFeatureCollection
        /// </summary>
        /// <returns>farm FeatureParent to retrieve SPFeatureCollection</returns>
        public static SPWebService GetFarm()
        {
            return SPWebService.ContentService;
        }

        public static SPFeatureDefinitionCollection GetFeatureDefinitionCollection()
        {
            return SPFarm.Local.FeatureDefinitions;
        }

        public static SPWebApplicationCollection GetWebApplicationsContent()
        {
            return SPWebService.ContentService.WebApplications;
        }

        public static SPWebApplication GetWebAppByUrl(string webAppUrl)
        {
            if (string.IsNullOrEmpty(webAppUrl))
            {
                return null;
            }

            SPWebApplication webApp = null;

            // first try content web apps:
            var webApps = ReadViaSharePointApi.GetWebApplicationsContent();

            if (webApps != null && webApps.Count > 0)
            {
                webApp = webApps.FirstOrDefault(wa => wa.GetResponseUri(SPUrlZone.Default).ToString().Equals(webAppUrl, StringComparison.InvariantCultureIgnoreCase));
            }

            // else try admin web apps:
            if (webApp == null)
            {
                webApps = ReadViaSharePointApi.GetWebApplicationsAdmin();

                if (webApps != null && webApps.Count > 0)
                {
                    webApp = webApps.FirstOrDefault(wa => wa.GetResponseUri(SPUrlZone.Default).ToString().Equals(webAppUrl, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            return webApp;
        }

        public static SPWebApplicationCollection GetWebApplicationsAdmin()
        {
            return SPWebService.AdministrationService.WebApplications;
        }
    }
}
