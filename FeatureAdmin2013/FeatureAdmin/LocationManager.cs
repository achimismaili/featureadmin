using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using FeatureAdmin.Models;

namespace FeatureAdmin
{
    public static class LocationManager
    {
        public static Location GetWebAppLocation(SPWebApplication webapp, bool admin)
        {
            Location loct = new Location();
            loct.Id = webapp.Id;
            // loct.ContentDatabaseId
            loct.Scope = SPFeatureScope.WebApplication;
            loct.FullUrl = SafeGetWebAppUrl(webapp);
            loct.RelativeUrl = loct.FullUrl;
            loct.Name = SafeGetWebAppTitle(webapp, admin);
            loct.Access = SafeGetWebAppAccess(webapp);
            return loct;
        }
        public static Location GetLocation(object obj)
        {
            Location loct = new Location();
            if (obj is SPFarm)
            {
                SPFarm farm = obj as SPFarm;
                loct.Id = farm.Id;
                // loct.ContentDatabaseId
                loct.Scope = SPFeatureScope.Farm;
                loct.FullUrl = "Farm";
                loct.RelativeUrl = loct.FullUrl;
                loct.Name = "Farm";
            }
            else if (obj is SPWebApplication)
            {
                SPWebApplication webapp = obj as SPWebApplication;
                bool admin = false;
                loct = GetWebAppLocation(webapp, admin);
            }
            else if (obj is SPSite)
            {
                SPSite site = obj as SPSite;
                loct.Id = site.ID;
                loct.ContentDatabaseId = site.ContentDatabase.Id;
                loct.Scope = SPFeatureScope.Site;
                loct.FullUrl = SafeGetSiteFullUrl(site);
                loct.RelativeUrl = SafeGetSiteRelativeUrl(site);
                loct.Name = SafeGetSiteTitle(site);
                loct.Access = SafeGetSiteAccess(site);
                PopulateTemplate(loct, site.RootWeb);
                //site.LastContentModifiedDate
                //site.ReadLocked
                //site.ReadOnly
            }
            else if (obj is SPWeb)
            {
                SPWeb web = obj as SPWeb;
                loct.Id = web.ID;
                loct.ContentDatabaseId = web.Site.ContentDatabase.Id;
                loct.Scope = SPFeatureScope.Web;
                loct.FullUrl = SafeGetWebFullUrl(web);
                loct.RelativeUrl = SafeGetWebRelativeUrl(web);
                loct.Name = SafeGetWebTitle(web);
                loct.Access = SafeGetWebAccess(web);
                PopulateTemplate(loct, web);
            }
            else
            {
                loct.Name = "?";
                loct.FullUrl = "?";
                loct.RelativeUrl = loct.FullUrl;
            }
            return loct;
        }
        public static string SafeDescribeObject(object obj)
        {
            try
            {
                Location loct = GetLocation(obj);
                return SafeDescribeLocation(loct);
            }
            catch
            {
                return "Exception describing object";
            }
        }
        public static string SafeDescribeLocation(Location loct)
        {
            if (loct.Scope == SPFeatureScope.Farm)
            {
                return "Farm";
            }
            if (!string.IsNullOrEmpty(loct.FullUrl))
            {
                if (!string.IsNullOrEmpty(loct.Name))
                { // have url and name
                    return string.Format("{0} - {1}", loct.FullUrl, loct.Name);
                }
                else
                {
                    // have url
                    return loct.FullUrl;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(loct.Name))
                { // have name
                    return loct.Name;
                }
                else
                { // have neither url nor name
                    return loct.Id.ToString();
                }
            }
        }
        public static string SafeGetWebAppTitle(SPWebApplication webApp, bool admin)
        {
            string name = "";
            try
            {
                if (webApp.Name != null)
                {
                    name = webApp.Name;
                }
            }
            catch
            {
            }
            if (admin)
            {
                if (name != "")
                {
                    name = " {CentralAdmin}: " + name; ;
                }
                else
                {
                    name = "{CentralAdmin}";
                }
            }
            return name;
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
        public static string SafeGetSiteAbsoluteUrl(SPSite site)
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
        public static string SafeGetSiteFullUrl(SPSite site)
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
        public static string SafeGetSiteRelativeUrl(SPSite site)
        {
            try
            {
                return site.ServerRelativeUrl;
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
        public static string SafeGetWebFullUrl(SPWeb web)
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
        public static string SafeGetWebRelativeUrl(SPWeb web)
        {
            try
            {
                int preflen = web.Site.Url.Length;
                return web.Url.Substring(preflen);
            }
            catch
            {
                return null;
            }
        }
        public static string SafeGetWebAppAccess(SPWebApplication webapp)
        {
            try
            {
                string name = webapp.Name;
                return "";
            }
            catch
            {
                return "?";
            }
        }
        public static string SafeGetSiteAccess(SPSite site)
        {
            try
            {
                return (site.ReadOnly ? "RO" : "");
            }
            catch
            {
                return "?";
            }
        }
        public static string SafeGetWebAccess(SPWeb web)
        {
            try
            {
                string name = web.Name;
                return "";
            }
            catch
            {
                return "?";
            }
        }
        private static void PopulateTemplate(Location loc, SPWeb web)
        {
            loc.Template.Name = "?";
            loc.Template.Title = "?";
            try
            {
                SPWebTemplateCollection templateCollection = web.Site.GetWebTemplates(web.Site.RootWeb.RegionalSettings.LocaleId);
                string templateName = web.WebTemplate + "#" + web.Configuration.ToString();
                loc.Template.Name = templateName;
                SPWebTemplate template = FindWebTemplate(templateCollection, templateName);
                loc.Template.Title = template.Title;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Return the web template description based on the name
        /// </summary>
        private static SPWebTemplate FindWebTemplate(SPWebTemplateCollection templateCollection, string templateName)
        {
            foreach (SPWebTemplate webTemplate in templateCollection)
            {
                // NB: Must use case-insentive here
                if (webTemplate.Name.Equals(templateName, StringComparison.InvariantCultureIgnoreCase))
                    return webTemplate;
            }
            return null;
        }
    }
}
