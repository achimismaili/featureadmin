using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public static class WebAppEnumerator
    {
        public static SPWebApplicationCollection GetAllWebApps()
        {
            // all the content WebApplications 
            SPWebApplicationCollection webapps = SPWebService.ContentService.WebApplications;
            SPAdministrationWebApplication centralAdmin = SPAdministrationWebApplication.Local;
            webapps.Add(centralAdmin);
            return webapps;
        }
    }
}
