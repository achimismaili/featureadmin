using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    /// <summary>
    /// Can activate or deactivate a set of features across any branch of the farm
    /// </summary>
    class FeatureActivator
    {
        public enum Action  { None, Activating, Deactivating };
        public enum Forcefulness { None, Regular, Forcible };

        #region Logging & exception logging
        public delegate void InfoLoggerHandler(Location location, string msg);
        public event InfoLoggerHandler InfoLoggingListeners;
        protected void LogInfo(Location location, string msg)
        {
            if (InfoLoggingListeners != null)
            {
                InfoLoggingListeners(location, msg);
            }
        }
        public delegate void ExceptionLoggerHandler(Exception exc, Location location, string msg);
        public event ExceptionLoggerHandler ExceptionLoggingListeners;
        protected void LogException(Exception exc, Location location, string msg)
        {
            if (ExceptionLoggingListeners != null)
            {
                ExceptionLoggingListeners(exc, location, msg);
            }
        }
        #endregion

        public int Activations { get; private set; }
        public int ActivationAttempts { get; private set; }
        private FeatureDatabase _featureDb;
        private Action _action;
        private Forcefulness _forcefulness;
        private FeatureSet _featureset = null;

        public FeatureActivator(FeatureDatabase featureDb, Action action, FeatureSet featureSet)
        {
            _featureDb = featureDb;
            _action = action;
            _featureset = featureSet;
        }

        /// <summary>activate features in the whole farm</summary>
        public void TraverseActivateFeaturesInFarm(Forcefulness forcefulness)
        {
            _forcefulness = forcefulness;
            ActivateFeaturesInFarm();

            foreach (SPWebApplication webapp in WebAppEnumerator.GetAllWebApps())
            {
                TraverseActivateFeaturesInWebApplication(webapp, forcefulness);
            }
        }
        /// <summary>
        /// Activate any farm level features on farm content service
        /// </summary>
        private void ActivateFeaturesInFarm()
        {
            Location location = LocationManager.GetLocation(SPFarm.Local);
            foreach (Feature feature in _featureset.FarmFeatures)
            {
                PerformAction(location, SPWebService.ContentService.Features, feature);
            }
        }
        public void TraverseActivateFeaturesInWebApplication(SPWebApplication webapp, Forcefulness forcefulness)
        {
            _forcefulness = forcefulness;
            ActivateFeaturesInWebApp(webapp);

            if (_featureset.SiteCollectionFeatureCount == 0
                && _featureset.WebFeatureCount == 0)
            {
                return;
            }

            foreach (SPSite site in webapp.Sites)
            {
                try
                {
                    TraverseActivateFeaturesInSiteCollection(site, forcefulness);
                }
                finally
                {
                    site.Dispose();
                }
            }
        }
        public void TraverseActivateFeaturesInSiteCollection(SPSite site, Forcefulness forcefulness)
        {
            _forcefulness = forcefulness;
            ActivateFeaturesInSiteCollection(site);

            if (_featureset.WebFeatureCount == 0)
            {
                return;
            }
            foreach (SPWeb web in site.AllWebs)
            {
                try
                {
                    ActivateFeaturesInWeb(web, forcefulness);
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
        private void ActivateFeaturesInWebApp(SPWebApplication webapp)
        {
            Location webappLoc = LocationManager.GetLocation(webapp);
            foreach (Feature feature in _featureset.WebAppFeatures)
            {
                PerformAction(webappLoc, SPWebService.ContentService.Features, feature);
            }
        }
        /// <summary>
        /// Activate any site level features on specified site
        /// </summary>
        private void ActivateFeaturesInSiteCollection(SPSite site)
        {
            Location siteLoc = LocationManager.GetLocation(site);
            foreach (Feature feature in _featureset.SiteCollectionFeatures)
            {
                PerformAction(siteLoc, site.Features, feature);
            }
        }
        public void ActivateFeaturesInWeb(SPWeb web, Forcefulness forcefulness)
        {
            _forcefulness = forcefulness;
            Location webLoc = LocationManager.GetLocation(web);
            foreach (Feature feature in _featureset.WebFeatures)
            {
                PerformAction(webLoc, web.Features, feature);
            }
        }
        private void PerformAction(Location location, SPFeatureCollection spFeatureCollection, Feature feature)
        {
            try
            {
                if (_action == Action.Activating)
                {
                    if (!(spFeatureCollection[feature.Id] is SPFeature))
                    {
                        ++ActivationAttempts;
                        try
                        {
                            spFeatureCollection.Add(feature.Id);
                            if ((spFeatureCollection[feature.Id] is SPFeature))
                            {
                                _featureDb.RecordFeatureActivationAtLocation(location, feature.Id);
                                ++Activations;
                                string msg = string.Format("Activated feature {0} ({1}:{2}) from location",
                                    feature.Id, feature.Scope, feature.Name);
                                LogInfo(location, msg);
                            }
                            else
                            {
                                string msg = string.Format("Failure activating feature {0} ({1}:{2}) from location",
                                    feature.Id, feature.Scope, feature.Name);
                                LogInfo(location, msg);
                            }
                        }
                        catch (Exception exc)
                        {
                            string msg = string.Format("Exception activating feature {0} ({1}:{2}) from location",
                                feature.Id, feature.Scope, feature.Name);
                            LogException(exc, location, msg);
                        }
                    }
                }
                else
                {
                    if ((spFeatureCollection[feature.Id] is SPFeature))
                    {
                        ++ActivationAttempts;
                        try
                        {
                            spFeatureCollection.Remove(feature.Id);
                            if (!(spFeatureCollection[feature.Id] is SPFeature))
                            {
                                _featureDb.RecordFeatureDeactivationAtLocation(location, feature.Id);
                                ++Activations;
                                string msg = string.Format("Removed feature {0} ({1}:{2}) from location",
                                    feature.Id, feature.Scope, feature.Name);
                                LogInfo(location, msg);

                            }
                            else
                            {
                                string msg = string.Format("Failure removing feature {0} ({1}:{2}) from location",
                                    feature.Id, feature.Scope, feature.Name);
                                LogInfo(location, msg);
                            }
                        }
                        catch (Exception exc)
                        {
                            string msg = string.Format("Exception removing feature {0} ({1}:{2}) from location",
                                feature.Id, feature.Scope, feature.Name);
                            LogException(exc, location, msg);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                string msg = string.Format("Exception {0} feature {1} ({2}:{3}) at location",
                    _action, feature.Id, feature.Scope, feature.Name);
                LogException(exc, location, msg);
            }
        }
    }
}
