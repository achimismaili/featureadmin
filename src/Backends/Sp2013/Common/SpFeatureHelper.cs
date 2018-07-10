using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    public static class SpFeatureHelper
    {

        private static string FeatureToggleFarm(Guid featureId, Scope featureScope, bool add, bool force, bool elevatedPrivileges, out ActivatedFeature feature)
        {
            feature = null;
            SPFeature spFeature = null;
            string errorMsg = null;
            FeatureDefinition definitionCanBeIgnoredHere;

            var farm = Common.SpLocationHelper.GetFarm(elevatedPrivileges);

            if (featureScope == Scope.Farm)
            {
                errorMsg += ToggleFeatureInFeatureCollection(farm.Features, featureId, add, force, out spFeature);

                if (spFeature != null)
                {
                    var farmLocation = farm.ToLocation();

                    

                    feature = SpConverter.ToActivatedFeature(
                        spFeature,
                        farmLocation,
                        out definitionCanBeIgnoredHere
                        );
                }
            }

            else
            {
                var webApps = Common.SpLocationHelper.GetAllWebApplications(elevatedPrivileges);

                foreach (SPWebApplication webApp in webApps)
                {
                    errorMsg += FeatureToggleWebApp(webApp, farm.Id, featureId, featureScope, add, force, elevatedPrivileges, out feature);
                }
            }

            return errorMsg;
        }

        private static string FeatureToggleWebApp(SPWebApplication webApp, Guid parentId, Guid featureId, Scope featureScope, bool add, bool force, bool elevatedPrivileges, out ActivatedFeature feature)
        {
            feature = null;
            SPFeature spFeature = null;
            string errorMsg = null;
            FeatureDefinition definitionCanBeIgnoredHere;

            if (featureScope == Scope.WebApplication)
            {
                errorMsg += ToggleFeatureInFeatureCollection(webApp.Features, featureId, add, force, out spFeature);
                if (spFeature != null)
                {
                    

                    var featureParentLocation = SpConverter.ToLocation(webApp, parentId);

                    feature = SpConverter.ToActivatedFeature(
                        spFeature,
                        featureParentLocation,
                        out definitionCanBeIgnoredHere
                        );
                }
            }

            else if (featureScope == Scope.Site || featureScope == Scope.Web)
            {
                foreach (SPSite site in webApp.Sites)
                {
                    errorMsg += FeatureToggleSite(site, featureId, featureScope, add, force, out feature);
                }
            }
            return errorMsg;
        }



        public static string FeatureToggleSite(SPSite site, Guid featureId, Scope featureScope, bool activate, bool force,
            out ActivatedFeature feature)
        {
            feature = null;
            SPFeature spFeature = null;
            string errorMsg = null;
            FeatureDefinition definitionCanBeIgnoredHere;

            if (featureScope == Scope.Site)
            {
                errorMsg += ToggleFeatureInFeatureCollection(site.Features, featureId, activate, force, out spFeature);

                if (spFeature != null)
                {
                    var featureParentLocation = SpConverter.ToLocation(site, site.WebApplication.Id);

                    feature = SpConverter.ToActivatedFeature(
                        spFeature,
                        featureParentLocation,
                        out definitionCanBeIgnoredHere
                        );
                }
            }
            else if (featureScope == Scope.Web)
            {
                foreach (SPWeb web in site.AllWebs)
                {
                    errorMsg += ToggleFeatureInFeatureCollection(web.Features, featureId, activate, force, out spFeature);

                    if (spFeature != null)
                    {
                        var featureParentLocation = SpConverter.ToLocation(web, web.Site.ID);

                        feature = SpConverter.ToActivatedFeature(
                            spFeature,
                            featureParentLocation,
                            out definitionCanBeIgnoredHere
                            );
                    }
                }
            }

           

            return errorMsg;
        }
        public static string FeatureToggleWeb(SPWeb web, Guid featureId, bool add, bool force, out ActivatedFeature feature)
        {
            feature = null;
            SPFeature spFeature = null;
            FeatureDefinition definitionCanBeIgnoredHere;

            var errorMsg = ToggleFeatureInFeatureCollection(web.Features, featureId, add, force, out spFeature);

            if (spFeature != null)
            {
                var featureParentLocation = SpConverter.ToLocation(web, web.Site.ID);

                feature = SpConverter.ToActivatedFeature(
                    spFeature,
                    featureParentLocation,
                    out definitionCanBeIgnoredHere
                    );
            }


            return errorMsg;
        }
        private static string ToggleFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool activate, bool force, out SPFeature spFeature)
        {
            spFeature = null;
            string errorMsg = null;


            if (activate)
            {
                try
                {

                    // activate feature
                    spFeature = features.Add(featureId, force);

                    if (spFeature == null)
                    {
                        errorMsg = "Feature activation failed.";
                    }
                }
                catch (Exception ex)
                {

                    errorMsg = string.Format(
                        "Error when trying to activate feature. Message: {0}",
                        ex.Message);
                }
            }
            else
            {
                // deactivate feature
                var featuresActiveBefore = features.Count();
                try
                {
                    features.Remove(featureId, force);
                }
                catch (Exception ex)
                {

                    errorMsg = string.Format(
                        "Error when trying to remove feature. Message: {0}",
                        ex.Message);
                }

                if (!(featuresActiveBefore > features.Count))
                {
                    errorMsg = "Feature was not removed.";
                }
            }

            return errorMsg;
        }

        public static string FeatureToggle(FeatureDefinition feature, Location location, bool add, bool force, bool elevatedPrivileges, out ActivatedFeature activatedFeature)
        {
            activatedFeature = null;
            string errorMsg = null;

            if (location == null || feature == null)
            {
                errorMsg = "Location or feature must not be null!";
                throw new ArgumentNullException(errorMsg);
            }

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    if (feature.Scope == Core.Models.Enums.Scope.Web)
                    {
                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            using (var site = new SPSite(location.Url))
                            {
                                using (var web = site.OpenWeb())
                                {
                                    FeatureToggleWeb(web, feature.Id, add, force, out activatedFeature);
                                }
                            };
                        });
                    }
                    break;
                case Core.Models.Enums.Scope.Site:

                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (var site = new SPSite(location.Url))
                        {
                            FeatureToggleSite(site, feature.Id, feature.Scope, add, force, out activatedFeature);
                        };
                    });

                    break;
                case Core.Models.Enums.Scope.WebApplication:

                    var wa = Common.SpLocationHelper.GetWebApplication(location.Id, elevatedPrivileges);

                    if (wa != null)
                    {
                        FeatureToggleWebApp(wa, location.Parent, feature.Id, feature.Scope, add, force, elevatedPrivileges, out activatedFeature);
                    }
                    break;
                case Core.Models.Enums.Scope.Farm:
                    FeatureToggleFarm(feature.Id, feature.Scope, add, force, elevatedPrivileges, out activatedFeature);
                    break;
                case Core.Models.Enums.Scope.ScopeInvalid:
                    var errInvalid = "Invalid scope was not expected!";
                    errorMsg += errInvalid;
                    throw new Exception(errInvalid);
                default:
                    var errUndef = "Undefined scope!";
                    errorMsg += errUndef;
                    throw new Exception(errUndef);
            }

            return errorMsg;
        }
    }
}
