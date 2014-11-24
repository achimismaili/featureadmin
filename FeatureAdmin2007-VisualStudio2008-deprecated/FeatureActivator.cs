using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    class FeatureActivator
    {
        /// <summary>
        /// Delegate to handle exception logging
        /// </summary>
        /// <param name="location"></param>
        public delegate void ExceptionLoggerHandler(Exception exc, string msg);
        public event ExceptionLoggerHandler ExceptionLoggingListeners;
        protected void LogException(Exception exc, string msg)
        {
            if (ExceptionLoggingListeners != null)
            {
                ExceptionLoggingListeners(exc, msg);
            }
        }

        public int Activations { get; private set; }

        /// <summary>activate features in the whole farm</summary>
        public void TraverseActivateFeaturesInFarm(List<Feature> features)
        {
            ActivateFeaturesInFarm(features);

            foreach (SPWebApplication webapp in WebAppEnumerator.GetAllWebApps())
            {
                TraverseActivateFeaturesInWebApplication(webapp, features);
            }
        }
        /// <summary>
        /// Activate any farm level features on farm content service
        /// </summary>
        private void ActivateFeaturesInFarm(List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                if (feature.Scope == SPFeatureScope.Farm)
                {
                    try
                    {
                        if (!(SPWebService.ContentService.Features[feature.Id] is SPFeature))
                        {
                            SPWebService.ContentService.Features.Add(feature.Id);
                            ++Activations;
                        }
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, string.Format(
                            "Attempting to activate farm feature {0} on farm",
                            feature.Id
                            ));
                    }
                }
            }
        }
        public void TraverseActivateFeaturesInWebApplication(SPWebApplication webapp, List<Feature> features)
        {
            ActivateFeaturesInWebApp(webapp, features);

            foreach (SPSite site in webapp.Sites)
            {
                try
                {
                    TraverseActivateFeaturesInSiteCollection(site, features);
                }
                finally
                {
                    site.Dispose();
                }
            }
        }
        public void TraverseActivateFeaturesInSiteCollection(SPSite site, List<Feature> features)
        {
            ActivateFeaturesInSiteCollection(site, features);

            foreach (SPWeb web in site.AllWebs)
            {
                try
                {
                    ActivateFeaturesInWeb(web, features);
                }
                finally
                {
                    web.Dispose();
                }
            }
        }
        /// <summary>
        /// Activate any webapp features on specified web application
        /// </summary>
        private void ActivateFeaturesInWebApp(SPWebApplication webapp, List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                if (feature.Scope == SPFeatureScope.WebApplication)
                {
                    try
                    {
                        if (!(SPWebService.ContentService.Features[feature.Id] is SPFeature))
                        {
                            SPWebService.ContentService.Features.Add(feature.Id);
                            ++Activations;
                        }
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, string.Format(
                            "Attempting to activate webapp feature {0} on webapp {1}",
                            feature.Id,
                            LocationManager.SafeDescribeObject(webapp)
                            ));
                    }
                }
            }
        }
        /// <summary>
        /// Activate any site level features on specified site
        /// </summary>
        private void ActivateFeaturesInSiteCollection(SPSite site, List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                if (feature.Scope == SPFeatureScope.Site)
                {
                    try
                    {
                        if (!(site.Features[feature.Id] is SPFeature))
                        {
                            site.Features.Add(feature.Id);
                            ++Activations;
                        }
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, string.Format(
                            "Attempting to activate site feature {0} on site {1}",
                            feature.Id,
                            LocationManager.SafeDescribeObject(site)
                            ));
                    }
                }
            }
        }
        public void ActivateFeaturesInWeb(SPWeb web, List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                if (feature.Scope == SPFeatureScope.Web)
                {
                    try
                    {
                        if (!(web.Features[feature.Id] is SPFeature))
                        {
                            web.Features.Add(feature.Id);
                            ++Activations;
                        }
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, string.Format(
                            "Attempting to activate web feature {0} on web {1}",
                            feature.Id,
                            LocationManager.SafeDescribeObject(web)
                            ));
                    }
                }
            }
        }
    }
}
