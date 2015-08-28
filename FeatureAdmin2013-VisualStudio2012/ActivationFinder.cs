using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    class ActivationFinder
    {
        public class Location
        {
            public Guid FeatureId;
            public SPFeatureScope Scope;
            public string Url;
            public string Name;
        }

        /// <summary>
        /// Delegate to report when feature found
        /// </summary>
        /// <param name="location"></param>
        public delegate void FeatureFoundHandler(Guid featureId, string url, string name);
        public event FeatureFoundHandler FoundListeners;
        protected void OnFoundFeature(Guid featureId, string url, string name)
        {
            if (FoundListeners != null)
            {
                FoundListeners(featureId, url, name);
            }
        }

        /// <summary>
        /// Delegate to report exception
        /// </summary>
        /// <param name="msg"></param>
        public delegate void ExceptionHandler(Exception exc, string msg);
        public event ExceptionHandler ExceptionListeners;
        protected void OnException(Exception exc, string msg) { if (ExceptionListeners != null) ExceptionListeners(exc, msg); }

        private Guid desiredFeature; //Empty if enumerating all
        private bool stopAtFirstHit;
        private int activationsFound = 0;
        private Dictionary<Guid, List<Location>> featureLocations = new Dictionary<Guid, List<Location>>();

        /// <summary>
        /// Find and report first place where feature activated, or return false if none found
        /// </summary>
        /// <returns>true if a location found</returns>
        public bool FindFirstFeatureActivation(Guid featureId)
        {
            desiredFeature = featureId;
            stopAtFirstHit = true;
            FindFeatureActivations();
            return (activationsFound > 0);
        }
        /// <summary>
        /// Find and return all activated locations of specified feature
        /// </summary>
        /// <returns>list of locations (dictionary only lists specified feature)</returns>
        public Dictionary<Guid, List<Location>> FindAllActivations(Guid featureId)
        {
            desiredFeature = featureId;
            stopAtFirstHit = false;
            FindFeatureActivations();
            return featureLocations;
        }
        /// <summary>
        /// Find and return all activated locations of all features
        /// </summary>
        /// <returns>dictionary of all locations of all features</returns>
        public Dictionary<Guid, List<Location>> FindAllActivationsOfAllFeatures()
        {
            desiredFeature = Guid.Empty;
            stopAtFirstHit = false;
            FindFeatureActivations();
            return featureLocations;
        }
        private void FindFeatureActivations()
        {
            //first, Look in Farm
            try
            {
                CheckFarm();
            }
            catch (Exception exc)
            {
                OnException(exc,
                    "Exception checking farm"
                    );
            }

            // check all web apps and everything under
            foreach (SPWebApplication webApp in GetAllWebApps())
            {
                try
                {
                    CheckWebApp(webApp);
                    if (stopAtFirstHit && activationsFound > 0) { return; }
                }
                catch (Exception exc)
                {
                    OnException(exc,
                        "Exception checking webapp: " + LocationInfo.SafeGetWebAppUrl(webApp)
                        );
                }

                try
                {
                    // check all sites and everything under
                    EnumerateWebAppSites(webApp);
                    if (stopAtFirstHit && activationsFound > 0) { return; }
                }
                catch (Exception exc)
                {
                    OnException(exc,
                        "Exception enumerating sites of webapp: " + LocationInfo.SafeGetWebAppUrl(webApp)
                        );
                }
            }
            return;
        }
        private SPWebApplicationCollection GetAllWebApps()
        {
            SPWebApplicationCollection webapps = SPWebService.ContentService.WebApplications;
            foreach (SPWebApplication adminApp in SPWebService.AdministrationService.WebApplications)
            {
                webapps.Add(adminApp);
            }
            return webapps;
        }
        private void CheckFarm()
        {
            if (desiredFeature != Guid.Empty)
            {
                if ((SPWebService.ContentService.Features[desiredFeature] is SPFeature))
                {
                    ReportFarmFeature(desiredFeature);
                }
            }
            else
            {
                foreach (SPFeature feature in SPWebService.ContentService.Features)
                {
                    ReportFarmFeature(feature.DefinitionId);
                }
            }
        }
        private void ReportFarmFeature(Guid featureId)
        {
            ++activationsFound;
            ReportFeature(SPFeatureScope.Farm, featureId, "farm", "farm");
        }
        private void CheckWebApp(SPWebApplication webApp)
        {
            if (desiredFeature != Guid.Empty)
            {
                if (webApp.Features[desiredFeature] is SPFeature)
                {
                    ReportWebAppFeature(desiredFeature, webApp);
                    return;
                }
            }
            else
            {
                foreach (SPFeature feature in webApp.Features)
                {
                    ReportWebAppFeature(desiredFeature, webApp);
                }
            }
        }
        private void ReportWebAppFeature(Guid featureId, SPWebApplication webApp)
        {
            ++activationsFound;
            ReportFeature(SPFeatureScope.WebApplication, featureId, LocationInfo.GetWebAppUrl(webApp), webApp.Name);
        }
        private void EnumerateWebAppSites(SPWebApplication webApp)
        {
            foreach (SPSite site in webApp.Sites)
            {
                using (site)
                {
                    // check site
                    try
                    {
                        CheckSite(site);
                        if (stopAtFirstHit && activationsFound > 0) { return; }
                    }
                    catch (Exception exc)
                    {
                        OnException(exc,
                            "Exception checking site: " + LocationInfo.SafeGetSiteUrl(site)
                            );
                    }
                    // check subwebs
                    try
                    {
                        EnumerateSiteWebs(site);
                        if (stopAtFirstHit && activationsFound > 0) { return; }
                    }
                    catch (Exception exc)
                    {
                        OnException(exc,
                            "Exception enumerating webs of site: " + LocationInfo.SafeGetSiteUrl(site)
                            );
                    }
                }
            }
        }
        private void CheckSite(SPSite site)
        {
            if (desiredFeature != Guid.Empty)
            {
                if (site.Features[desiredFeature] is SPFeature)
                {
                    ReportSiteFeature(desiredFeature, site);
                }
            }
            else
            {
                foreach (SPFeature feature in site.Features)
                {
                    ReportSiteFeature(feature.DefinitionId, site);
                }
            }
        }
        private void ReportSiteFeature(Guid featureId, SPSite site)
        {
            ++activationsFound;
            ReportFeature(SPFeatureScope.Site, featureId, site.Url, site.RootWeb.Title);
        }
        private void EnumerateSiteWebs(SPSite site)
        {
            foreach (SPWeb web in site.AllWebs)
            {
                using (web)
                {
                    try
                    {
                        // check web
                        CheckWeb(web);
                        if (stopAtFirstHit && activationsFound > 0) { return; }
                    }
                    catch (Exception exc)
                    {
                        OnException(exc,
                            "Exception checking web: " + LocationInfo.SafeGetWebUrl(web)
                            );
                    }
                }
            }
        }
        private void CheckWeb(SPWeb web)
        {
            if (desiredFeature != Guid.Empty)
            {
                if (web.Features[desiredFeature] is SPFeature)
                {
                    ReportWebFeature(desiredFeature, web);
                }
            }
            else
            {
                foreach (SPFeature feature in web.Features)
                {
                    ReportWebFeature(feature.DefinitionId, web);
                }
            }
        }
        private void ReportWebFeature(Guid featureId, SPWeb web)
        {
            ++activationsFound;
            ReportFeature(SPFeatureScope.Web, featureId, web.Url, web.Title);
        }
        private void ReportFeature(SPFeatureScope scope, Guid featureId, string url, string name)
        {
            OnFoundFeature(featureId, url, name);
            Location location = new Location();
            location.Scope = scope;
            location.FeatureId = featureId;
            location.Url = url;
            location.Name = name;
            List<Location> locs = null;
            if (featureLocations.ContainsKey(featureId))
            {
                locs = featureLocations[featureId];
            }
            else
            {
                locs = new List<Location>();
            }
            locs.Add(location);
            featureLocations[featureId] = locs;
        }
    }
}
