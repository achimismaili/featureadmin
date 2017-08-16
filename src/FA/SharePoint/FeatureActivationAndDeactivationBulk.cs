using FA.Models;
using FA.Models.Interfaces;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.SharePoint
{
    public static class FeatureActivationAndDeactivationBulk
    {

        #region public multiple scope methods called with no real SharePoint objects, only FeatureParent and/or ActivatedFeature and/or FeatureDefinition
        /// <summary>
        /// Activates a collection of feature(definition)s in a defined sharepoint container and below
        /// </summary>
        /// <param name="sharePointContainer">a web, site collection, web app or farm, where to activate features</param>
        /// <param name="featureDefinitions">the features to be activated</param>
        /// <param name="force">should the features forecefully be activated?</param>
        /// <param name="exception">returns an exception message (e.g. for logging purposes)</param>
        /// <returns>number of activated features</returns>
        /// <remarks>
        /// e.g. when providing a Web App as container and a set of site and web features, 
        /// then everywhere within the web application, those features will be activated in all sites and webs
        /// 
        /// The parameter sharePointContainer (feature parent) defines the activation level and the url for getting the container from SharePoint
        /// 
        /// The parameter featureDefinition is required for feature id and scope of the feature
        /// </remarks>
        public static int ActivateAllFeaturesWithinSharePointContainer(IFeatureParent sharePointContainer, IEnumerable<IFeatureIdentifier> featureDefinitions, bool force, out Exception exception)
        {
            return ProcessAllFeaturesWithinSharePointContainer(sharePointContainer, featureDefinitions, true, force, out exception);
        }
        public static int DeactivateAllFeaturesWithinSharePointContainer(IFeatureParent sharePointContainer, IEnumerable<IFeatureIdentifier> featureDefinitions, bool force, out Exception exception)
        {
            return ProcessAllFeaturesWithinSharePointContainer(sharePointContainer, featureDefinitions, false, force, out exception);
        }
        private static int ProcessAllFeaturesWithinSharePointContainer(IFeatureParent sharePointContainer, IEnumerable<IFeatureIdentifier> featureDefinitions, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (sharePointContainer == null)
            {
                exception = new ArgumentNullException("sharePointContainer (FeatureParent) is null");
                return 0;
            }
            if (featureDefinitions == null || featureDefinitions.Count() == 0)
            {
                exception = new ArgumentNullException("No features to activate!");
                return 0;
            }

            var farmFeatureIds = featureDefinitions.Where(fd => fd.Scope == SPFeatureScope.Farm).Select(fd => fd.Id);
            var webAppFeatureIds = featureDefinitions.Where(fd => fd.Scope == SPFeatureScope.WebApplication).Select(fd => fd.Id);
            var siteFeatureIds = featureDefinitions.Where(fd => fd.Scope == SPFeatureScope.Site).Select(fd => fd.Id);
            var webFeatureIds = featureDefinitions.Where(fd => fd.Scope == SPFeatureScope.Web).Select(fd => fd.Id);
            var invalidFeatureIds = featureDefinitions.Where(fd => fd.Scope == SPFeatureScope.ScopeInvalid).Select(fd => fd.Id);

            var activationLevel = sharePointContainer.Scope;
            
            switch (activationLevel)
            {
                case SPFeatureScope.Farm:
                    processingCounter += ProcessAllFeaturesInFarm(farmFeatureIds, SPFeatureScope.Farm, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInFarm(webAppFeatureIds, SPFeatureScope.WebApplication, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInFarm(siteFeatureIds, SPFeatureScope.Site, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInFarm(webFeatureIds, SPFeatureScope.Web, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInFarm(invalidFeatureIds, SPFeatureScope.ScopeInvalid, activate, force, out exception);
                    break;
                case SPFeatureScope.WebApplication:
                    processingCounter += ProcessAllFeaturesInWebApp(sharePointContainer.Url , webAppFeatureIds, SPFeatureScope.WebApplication, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInWebApp(sharePointContainer.Url, siteFeatureIds, SPFeatureScope.Site, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInWebApp(sharePointContainer.Url, webFeatureIds, SPFeatureScope.Web, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInWebApp(sharePointContainer.Url, invalidFeatureIds, SPFeatureScope.ScopeInvalid, activate, force, out exception);
                    break;
                case SPFeatureScope.Site:
                    processingCounter += ProcessAllFeaturesInSite(sharePointContainer.Url, siteFeatureIds, SPFeatureScope.Site, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInSite(sharePointContainer.Url, webFeatureIds, SPFeatureScope.Web, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInSite(sharePointContainer.Url, invalidFeatureIds, SPFeatureScope.ScopeInvalid, activate, force, out exception);
                    break;
                case SPFeatureScope.Web:
                    processingCounter += ProcessAllFeaturesInWeb(sharePointContainer.Url, webFeatureIds, activate, force, out exception);
                    processingCounter += ProcessAllFeaturesInWeb(sharePointContainer.Url, invalidFeatureIds, activate, force, out exception);

                    break;
                default:
                    exception = new ArgumentException("sharepoint container scope was invalid");
                    break;
            }

            return processingCounter;
        }

        public static int DeactivateAllFeatures(IEnumerable<IActivatedFeature> features, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (features == null || features.Count() == 0)
            {
                exception = new ArgumentNullException("No features to deactivate!");
                return 0;
            }

            
            var farmFeatureIds = features.Where(fd => fd.Scope == SPFeatureScope.Farm).Select(fd => fd.Id);
            var webAppFeatures = features.Where(fd => fd.Scope == SPFeatureScope.WebApplication);
            var siteFeatures = features.Where(fd => fd.Scope == SPFeatureScope.Site);
            var webFeatures = features.Where(fd => fd.Scope == SPFeatureScope.Web);
            var invalidFeatures = features.Where(fd => fd.Scope == SPFeatureScope.ScopeInvalid);

            // deactivate farm features
            if (farmFeatureIds != null && farmFeatureIds.Count() > 0)
            {
                processingCounter += ProcessAllFeaturesInFarm(farmFeatureIds, SPFeatureScope.Farm, false, force, out exception);
            }

            // deactivate WebApp features
            if (webAppFeatures != null && webAppFeatures.Count() > 0)
            {
                var groupedFeatures = webAppFeatures.GroupBy(f => f.Parent.Url); 
                foreach (var featureGroup in groupedFeatures)
                {
                    IFeatureParent sharePointContainer = featureGroup.First().Parent;

                    processingCounter += ProcessAllFeaturesInWebApp(sharePointContainer.Url, featureGroup.Select(f => f.Id), SPFeatureScope.WebApplication, false, force, out exception);
                }
            }

            // deactivate SiteCollection features
            if (siteFeatures != null && siteFeatures.Count() > 0)
            {
                var groupedFeatures = siteFeatures.GroupBy(f => f.Parent.Url);
                foreach (var featureGroup in groupedFeatures)
                {
                    IFeatureParent sharePointContainer = featureGroup.First().Parent;

                    processingCounter += ProcessAllFeaturesInSite(sharePointContainer.Url, featureGroup.Select(f => f.Id), SPFeatureScope.Site, false, force, out exception);
                }
            }

            // deactivate Web features
            if (webFeatures != null && webFeatures.Count() > 0)
            {
                var groupedFeatures = webFeatures.GroupBy(f => f.Parent.Url);
                foreach (var featureGroup in groupedFeatures)
                {
                    IFeatureParent sharePointContainer = featureGroup.First().Parent;

                    processingCounter += ProcessAllFeaturesInWeb(sharePointContainer.Url, featureGroup.Select(f => f.Id), false, force, out exception);
                }
            }
            return processingCounter;
        }

        #endregion public multiple scope methods called with no real SharePoint objects, only FeatureParent and/or ActivatedFeature and/or FeatureDefinition

        #region private, generic processing methods, called with no real SharePoint objects

        private static int ProcessAllFeaturesInFarm(IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            
            try
            {
                var farm = FarmRead.GetFarm();
                if (farm == null)
                {
                    exception = new ArgumentNullException("farm is null !? not enough rights?");
                    return processingCounter;
                }

                processingCounter += ProcessAllFeaturesInFarm(farm, featureIds, scope, activate, force, out exception);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        public static int ProcessAllFeaturesInWebApp(string webAppUrl, IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (string.IsNullOrEmpty(webAppUrl))
            {
                exception = new ArgumentNullException("webAppUrl is null");
                return processingCounter;
            }
            try
            {
                var webApp = FarmRead.GetWebAppByUrl(webAppUrl);
                if (webApp == null)
                {
                    exception = new ArgumentNullException("webApp not found by Url " + webAppUrl);
                    return processingCounter;
                }

                processingCounter += ProcessAllFeaturesInWebApp(webApp, featureIds, scope, activate, force, out exception);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        public static int ProcessAllFeaturesInSite(string siteUrl, IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            Exception reEx = null;
            exception = null;

            if (string.IsNullOrEmpty(siteUrl))
            {
                exception = new ArgumentNullException("siteUrl is null");
                return processingCounter;
            }
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (var site = new SPSite(siteUrl))
                    {
                        processingCounter += ProcessAllFeaturesInSite(site, featureIds, scope, activate, force, out reEx);
                    }
                });
                exception = reEx;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }


        public static int ProcessAllFeaturesInWeb(string webUrl, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            Exception reEx = null;
            exception = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (var site = new SPSite(webUrl))
                    {
                        using (var web = site.OpenWeb())
                        {
                            processingCounter += ProcessAllFeaturesInWeb(web, featureIds, activate, force, out reEx);
                        }
                    };
                });
                exception = reEx;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        #endregion private, generic processing methods, called with no real SharePoint objects

        #region multiple scope processing with real SharePoint containers

        private static int ProcessAllFeaturesInFarm(SPWebService farm, IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (farm == null)
            {
                exception = new ArgumentNullException("farm is null");
                return processingCounter;
            }
            try
            {
                if(scope == SPFeatureScope.Farm || scope == SPFeatureScope.ScopeInvalid)
                {
                    processingCounter += ProcessAllFarmFeaturesInFarm(farm, featureIds, activate, force, out exception);
                }

                if (scope != SPFeatureScope.Farm)
                {
                    SPWebApplicationCollection webApps = FarmRead.GetWebApplicationsContent();

                    if (webApps != null && webApps.Count > 0)
                    {
                        foreach (SPWebApplication wa in webApps)
                        {
                            processingCounter += ProcessAllFeaturesInWebApp(wa, featureIds, scope, activate, force, out exception);
                        }
                    }

                    webApps = FarmRead.GetWebApplicationsAdmin();
                    if (webApps != null && webApps.Count > 0)
                    {
                        foreach (SPWebApplication wa in webApps)
                        {
                            processingCounter += ProcessAllFeaturesInWebApp(wa, featureIds, scope, activate, force, out exception);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        private static int ProcessAllFeaturesInWebApp(SPWebApplication webApp, IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (webApp == null)
            {
                exception = new ArgumentNullException("webApp is null");
                return processingCounter;
            }
            try
            {
                if (scope == SPFeatureScope.WebApplication || scope == SPFeatureScope.ScopeInvalid)
                {
                    processingCounter += ProcessAllWebAppFeaturesInWebApp(webApp, featureIds, activate, force, out exception);
                }

                if (scope == SPFeatureScope.Site || scope == SPFeatureScope.Web || scope == SPFeatureScope.ScopeInvalid)
                {
                    foreach (SPSite site in webApp.Sites)
                    {
                        processingCounter += ProcessAllFeaturesInSite(site, featureIds, scope, activate, force, out exception);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        private static int ProcessAllFeaturesInSite(SPSite site, IEnumerable<Guid> featureIds, SPFeatureScope scope, bool activate, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            if (site == null)
            {
                exception = new ArgumentNullException("site was null");
                return processingCounter;
            }

            try
            {
                if (scope == SPFeatureScope.Site || scope == SPFeatureScope.ScopeInvalid)
                {
                    processingCounter += ProcessAllSiteFeaturesInSite(site, featureIds, activate, force, out exception);
                }

                if (scope == SPFeatureScope.Web || scope == SPFeatureScope.ScopeInvalid)
                {
                    foreach (SPWeb web in site.AllWebs)
                    {
                        processingCounter += ProcessAllWebFeaturesInWeb(web, featureIds, activate, force, out exception);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }

        private static int ProcessAllFeaturesInWeb(SPWeb web, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            return ProcessAllWebFeaturesInWeb(web, featureIds, activate, force, out exception);
        }

        #endregion multiple scope processing with real SharePoint containers

        #region single scope processing with real SharePoint containers

        private static int ProcessAllFarmFeaturesInFarm(SPWebService farm, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var processingCounter = 0;

            if (featureIds != null && featureIds.Count() > 0)
            {
                foreach (Guid id in featureIds)
                {
                    // overwrite the exception, it is just for logging, in case at least one happens ...
                    processingCounter += FeatureActivationAndDeactivationCore.ProcessFarmFeatureInFarm(farm, id, activate, force, out exception);
                }
            }

            return processingCounter;
        }

        private static int ProcessAllWebAppFeaturesInWebApp(SPWebApplication webApp, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var processingCounter = 0;

            if (featureIds != null && featureIds.Count() > 0)
            {
                foreach (Guid id in featureIds)
                {
                    // overwrite the exception, it is just for logging, in case at least one happens ...
                    processingCounter += FeatureActivationAndDeactivationCore.ProcessWebAppFeatureInWebApp(webApp, id, activate, force, out exception);
                }
            }

            return processingCounter;
        }

        private static int ProcessAllSiteFeaturesInSite(SPSite site, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var processingCounter = 0;

            if (featureIds != null && featureIds.Count() > 0)
            {
                foreach (Guid id in featureIds)
                {
                    // overwrite the exception, it is just for logging, in case at least one happens ...
                    processingCounter += FeatureActivationAndDeactivationCore.ProcessSiteFeatureInSite(site, id, activate, force, out exception);
                }
            }

            return processingCounter;
        }
        private static int ProcessAllWebFeaturesInWeb(SPWeb web, IEnumerable<Guid> featureIds, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var processingCounter = 0;

            if (featureIds != null && featureIds.Count() > 0)
            {
                foreach (Guid id in featureIds)
                {
                    // overwrite the exception, it is just for logging, in case at least one happens ...
                    processingCounter += FeatureActivationAndDeactivationCore.ProcessWebFeatureInWeb(web, id, activate, force, out exception);
                }
            }

            return processingCounter;
        }
        #endregion single scope processing with real SharePoint containers
    }
}
