using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using FeatureAdmin.Models;

namespace FeatureAdmin
{
    public class ActivationFinder
    {
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
        private Dictionary<Guid, int> faultyFeatures = new Dictionary<Guid, int>();

        public List<Guid> GetFaultyFeatureIdList() { return new List<Guid>(faultyFeatures.Keys); }

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
            foreach (WebAppEnumerator.WebAppInfo webappInfo in WebAppEnumerator.GetAllWebApps())
            {
                SPWebApplication webApp = webappInfo.WebApp;
                try
                {
                    CheckWebApp(webApp);
                    if (stopAtFirstHit && activationsFound > 0) { return; }
                }
                catch (Exception exc)
                {
                    OnException(exc,
                        "Exception checking webapp: " + LocationManager.SafeGetWebAppUrl(webApp)
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
                        "Exception enumerating sites of webapp: " + LocationManager.SafeGetWebAppUrl(webApp)
                        );
                }
            }
            return;
        }
        private void CheckFarm()
        {
            if (desiredFeature != Guid.Empty)
            {
                if ((SPWebService.ContentService.Features[desiredFeature] is SPFeature))
                {
                    bool faulty = false; // ?
                    ReportFarmFeature(desiredFeature, faulty);
                }
            }
            else
            {
                foreach (SPFeature feature in SPWebService.ContentService.Features)
                {
                    bool faulty = IsFeatureFaulty(feature);
                    ReportFarmFeature(feature.DefinitionId, faulty);
                }
            }
        }
        private void ReportFarmFeature(Guid featureId, bool faulty)
        {
            ++activationsFound;
            ReportFeature(SPFarm.Local, faulty, SPFeatureScope.Farm, featureId, "farm", "farm");
        }
        private void CheckWebApp(SPWebApplication webApp)
        {
            if (desiredFeature != Guid.Empty)
            {
                if (webApp.Features[desiredFeature] is SPFeature)
                {
                    bool faulty = false; // ?
                    ReportWebAppFeature(desiredFeature, faulty, webApp);
                    return;
                }
            }
            else
            {
                foreach (SPFeature feature in webApp.Features)
                {
                    bool faulty = IsFeatureFaulty(feature);
                    Guid featureId = faulty ? Guid.Empty : feature.DefinitionId;
                    ReportWebAppFeature(featureId, faulty, webApp);
                }
            }
        }
        private bool IsFeatureFaulty(SPFeature feature)
        {
            if (feature.Definition == null)
            {
                return true;
            }
            FeatureChecker checker = new FeatureChecker();
            if (checker.CheckFeature(feature).Faulty)
            {
                return true;
            }
            return false;
        }
        private void ReportWebAppFeature(Guid featureId, bool faulty, SPWebApplication webApp)
        {
            ++activationsFound;
            ReportFeature(webApp, faulty, SPFeatureScope.WebApplication, featureId, LocationManager.GetWebAppUrl(webApp), webApp.Name);
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
                            "Exception checking site: " + LocationManager.SafeGetSiteAbsoluteUrl(site)
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
                            "Exception enumerating webs of site: " + LocationManager.SafeGetSiteAbsoluteUrl(site)
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
                    bool faulty = false; // ?
                    ReportSiteFeature(desiredFeature, faulty, site);
                }
            }
            else
            {
                foreach (SPFeature feature in site.Features)
                {
                    bool faulty = IsFeatureFaulty(feature);
                    ReportSiteFeature(feature.DefinitionId, faulty, site);
                }
            }
        }
        private void ReportSiteFeature(Guid featureId, bool faulty, SPSite site)
        {
            ++activationsFound;
            ReportFeature(site, faulty, SPFeatureScope.Site, featureId, site.Url, site.RootWeb.Title);
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
                            "Exception checking web: " + LocationManager.SafeGetWebFullUrl(web)
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
                    bool faulty = false; // ?
                    ReportWebFeature(desiredFeature, faulty, web);
                }
            }
            else
            {
                foreach (SPFeature feature in web.Features)
                {
                    bool faulty = IsFeatureFaulty(feature);
                    ReportWebFeature(feature.DefinitionId, faulty, web);
                }
            }
        }
        private void ReportWebFeature(Guid featureId, bool faulty, SPWeb web)
        {
            ++activationsFound;
            ReportFeature(web, faulty, SPFeatureScope.Web, featureId, web.Url, web.Title);
        }
        private void ReportFeature(object obj, bool faulty, SPFeatureScope scope, Guid featureId, string url, string name)
        {
            OnFoundFeature(featureId, url, name);
            Location location = LocationManager.GetLocation(obj);
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
            if (faulty)
            {
                int faults = 0;
                if (faultyFeatures.ContainsKey(featureId))
                {
                    faults = faultyFeatures[featureId];
                }
                ++faults;
                faultyFeatures[featureId] = faults;
            }
        }
    }
}
