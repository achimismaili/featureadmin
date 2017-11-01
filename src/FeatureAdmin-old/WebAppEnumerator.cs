using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public static class WebAppEnumerator
    {
        public struct WebAppInfo
        {
            public SPWebApplication WebApp;
            public bool Admin;
        }
        public static IList<WebAppInfo> GetAllWebApps()
        {
            List<WebAppInfo> webapps = new List<WebAppInfo>();
            // all the content WebApplications 
            foreach (SPWebApplication contentApp in SPWebService.ContentService.WebApplications)
            {
                WebAppInfo webappInfo = new WebAppInfo();
                webappInfo.WebApp = contentApp;
                webappInfo.Admin = false;
                webapps.Add(webappInfo);
            }
            // Central Admin
            foreach (SPWebApplication adminApp in SPWebService.AdministrationService.WebApplications)
            {
                WebAppInfo webappInfo = new WebAppInfo();
                webappInfo.WebApp = adminApp;
                webappInfo.Admin = true;
                webapps.Add(webappInfo);
            }
            return webapps;
        }
    }
}
