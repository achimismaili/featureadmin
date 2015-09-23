using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public static class LocationInfo
    {
        private class LocationDetails
        {
            public string Url = null;
            public string Name = null;
            public Guid Id = Guid.Empty;
        }
        private static LocationDetails GetLocation(object obj)
        {
            LocationDetails details = new LocationDetails();
            if (obj is SPWebApplication)
            {
                SPWebApplication webapp = obj as SPWebApplication;
                details.Url = SafeGetWebAppUrl(webapp);
                details.Name = SafeGetWebAppTitle(webapp);
                details.Id = webapp.Id;
            }
            else if (obj is SPSite)
            {
                SPSite site = obj as SPSite;
                details.Url = SafeGetSiteUrl(site);
                details.Name = SafeGetSiteTitle(site);
                details.Id = site.ID;
            }
            else if (obj is SPWeb)
            {
                SPWeb web = obj as SPWeb;
                details.Url = SafeGetWebUrl(web);
                details.Name = SafeGetWebTitle(web);
                details.Id = web.ID;
            }
            return details;
        }
        public static string SafeDescribeObject(object obj)
        {
            if (obj is SPFarm || obj is SPWebService)
            {
                return "Farm";
            }
            LocationDetails details = GetLocation(obj);
            if (!string.IsNullOrEmpty(details.Url))
            {
                if (!string.IsNullOrEmpty(details.Name))
                { // have url and name
                    return string.Format("{0} - {1}", details.Url, details.Name);
                }
                else
                {
                    // have url
                    return details.Url;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(details.Name))
                { // have name
                    return details.Name;
                }
                else
                { // have neither url nor name
                    return details.Id.ToString();
                }
            }
        }
        public static string SafeGetWebAppTitle(SPWebApplication webApp)
        {
            try
            {
                return webApp.Name;
            }
            catch
            {
                return null;
            }
        }
        public static string SafeGetWebAppUrl(SPWebApplication webApp)
        {
            try
            {
                return GetWebAppUrl(webApp);
            }
            catch
            {
                return null;
            }
        }
        public static string GetWebAppUrl(SPWebApplication webApp)
        {
            return webApp.GetResponseUri(SPUrlZone.Default).AbsoluteUri;
        }
        public static string SafeGetSiteTitle(SPSite site)
        {
            try
            {
                return site.RootWeb.Title;
            }
            catch
            {
                return null;
            }
        }
        public static string SafeGetSiteUrl(SPSite site)
        {
            try
            {
                return site.Url;
            }
            catch
            {
                return null;
            }
        }
        public static string SafeGetWebTitle(SPWeb web)
        {
            try
            {
                return web.Title;
            }
            catch
            {
                return null;
            }
        }
        public static string SafeGetWebUrl(SPWeb web)
        {
            try
            {
                return web.Url;
            }
            catch
            {
                return null;
            }
        }
    }
}
