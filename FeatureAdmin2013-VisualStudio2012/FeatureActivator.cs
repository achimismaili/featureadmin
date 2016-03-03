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
        public enum Action  { Activating, Deactivating };

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
        public int ActivationAttempts { get; private set; }
        private FeatureDatabase _featureDb;
        private Action _action;
        private FeatureSet _featureset = null;

        public FeatureActivator(FeatureDatabase featureDb, Action action, FeatureSet featureSet)
        {
            _featureDb = featureDb;
            _action = action;
            _featureset = featureSet;
        }

        /// <summary>activate features in the whole farm</summary>
        public void TraverseActivateFeaturesInFarm()
        {
            ActivateFeaturesInFarm();

            foreach (SPWebApplication webapp in WebAppEnumerator.GetAllWebApps())
            {
                TraverseActivateFeaturesInWebApplication(webapp);
            }
        }
        /// <summary>
        /// Activate any farm level features on farm content service
        /// </summary>
        private void ActivateFeaturesInFarm()
        {
            foreach (Feature feature in _featureset.FarmFeatures)
            {
                PerformAction(SPFarm.Local, SPWebService.ContentService.Features, feature);
            }
        }
        public void TraverseActivateFeaturesInWebApplication(SPWebApplication webapp)
        {
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
                    TraverseActivateFeaturesInSiteCollection(site);
                }
                finally
                {
                    site.Dispose();
                }
            }
        }
        public void TraverseActivateFeaturesInSiteCollection(SPSite site)
        {
            ActivateFeaturesInSiteCollection(site);

            if (_featureset.WebFeatureCount == 0)
            {
                return;
            }
            foreach (SPWeb web in site.AllWebs)
            {
                try
                {
                    ActivateFeaturesInWeb(web);
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
            foreach (Feature feature in _featureset.WebAppFeatures)
            {
                PerformAction(webapp, SPWebService.ContentService.Features, feature);
            }
        }
        /// <summary>
        /// Activate any site level features on specified site
        /// </summary>
        private void ActivateFeaturesInSiteCollection(SPSite site)
        {
            foreach (Feature feature in _featureset.SiteCollectionFeatures)
            {
                PerformAction(site, site.Features, feature);
            }
        }
        public void ActivateFeaturesInWeb(SPWeb web)
        {
            foreach (Feature feature in _featureset.WebFeatures)
            {
                PerformAction(web, web.Features, feature);
            }
        }
        private void PerformAction(object locobj, SPFeatureCollection spFeatureCollection, Feature feature)
        {
            try
            {
                if (_action == Action.Activating)
                {
                    if (!(spFeatureCollection[feature.Id] is SPFeature))
                    {
                        ++ActivationAttempts;
                        spFeatureCollection.Add(feature.Id);
                        if ((spFeatureCollection[feature.Id] is SPFeature))
                        {
                            _featureDb.RecordFeatureActivation(locobj, feature.Id);
                            ++Activations;
                        }
                    }
                }
                else
                {
                    if ((spFeatureCollection[feature.Id] is SPFeature))
                    {
                        ++ActivationAttempts;
                        spFeatureCollection.Remove(feature.Id);
                        if (!(spFeatureCollection[feature.Id] is SPFeature))
                        {
                            _featureDb.RecordFeatureDeactivation(locobj, feature.Id);
                            ++Activations;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                LogException(exc, string.Format(
                    "{0} feature {1} on {2}: {3}",
                    _action,
                    feature.Id,
                    feature.Scope,
                    LocationManager.SafeDescribeObject(locobj)
                    ));
            }
        }
    }
}
