using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    public static class SpConverter
    {
        public static ActivatedFeature ToActivatedFeature(
            this SPFeature spFeature,
            Location location)
        {
            FeatureDefinition definition = null;
            bool faulty = false;

            string featureUniqueId;
            string displayName;
            Version definitionVersion;

            try
            {
                var fDef = spFeature.Definition;

                if (fDef != null)
                {


                    if (spFeature.FeatureDefinitionScope == SPFeatureDefinitionScope.Web)
                    {
                        definition = fDef.ToFeatureDefinition(location.UniqueId);
                    }
                    else if (spFeature.FeatureDefinitionScope == SPFeatureDefinitionScope.Site)
                    {
                        if (location.Scope == Scope.Web)
                        {
                            definition = fDef.ToFeatureDefinition(location.ParentId);
                        }
                        else
                        {
                            // only other location with featuredefinitionscope site can be site collection
                            // therefore, location id for sandboxed solution is current location (site)
                            definition = fDef.ToFeatureDefinition(location.UniqueId);
                        }
                    }
                    else
                    {
                        // Featuredefinitionscope must be farm or none now, in both cases, no location will be assigned to feature definition ...
                        definition = fDef.ToFeatureDefinition(null);
                    }

                    featureUniqueId = definition.UniqueIdentifier;
                    displayName = definition.DisplayName;
                    definitionVersion = definition.Version;
                }
                else
                {
                    faulty = true;

                    featureUniqueId = Core.Common.StringHelper.GenerateUniqueId(
                                        spFeature.DefinitionId,
                                        Core.Common.Constants.Labels.FaultyFeatureCompatibilityLevel
                                        );
                    displayName = Core.Common.Constants.Labels.FaultyFeatureName;
                    definitionVersion = null;
                }
            }
            catch (Exception)
            {
                faulty = true;

                featureUniqueId = Core.Common.StringHelper.GenerateUniqueId(
                                        spFeature.DefinitionId,
                                        Core.Common.Constants.Labels.FaultyFeatureCompatibilityLevel
                                        );
                displayName = Core.Common.Constants.Labels.FaultyFeatureName;
                definitionVersion = null;
            }

            var feature = ActivatedFeatureFactory.GetActivatedFeature(
                featureUniqueId,
                location.UniqueId,
                displayName,
                faulty,
                spFeature.Properties == null ? null :
                spFeature.Properties.ToProperties(),
                spFeature.TimeActivated,
                spFeature.Version,
                definitionVersion,
                spFeature.FeatureDefinitionScope.ToFeatureDefinitionScope()
                );

            return feature;
        }

        /// <summary>
        /// Converts SPFeatures to ActivatedFeatures
        /// </summary>
        /// <param name="spFeatures">the features</param>
        /// <param name="location">parent location container</param>
        /// <returns>SPFeatures converted to ActivatedFeatures</returns>
        public static IEnumerable<ActivatedFeature> ToActivatedFeatures(this SPFeatureCollection spFeatures, Location location)
        {
            IEnumerable<FeatureDefinition> nonFarmFeatureDefinitions;

            return ToActivatedFeatures(spFeatures,
                location,
                out nonFarmFeatureDefinitions);

        }

        /// <summary>
        /// Converts SPFeatures to ActivatedFeatures
        /// </summary>
        /// <param name="spFeatures">the features</param>
        /// <param name="location">parent location container</param>
        /// <param name="nonFarmFeatureDefinitions">collection of feature definitions that
        /// are not installed in the farm, rather on site or web level</param>
        /// <returns>SPFeatures converted to ActivatedFeatures</returns>
        /// <remarks>
        /// Only following features need to get in there:
        /// feature definitions from sandboxed solutions
        /// feature definitions from add ins
        /// definitions from faulty features
        /// </remarks>
        public static IEnumerable<ActivatedFeature> ToActivatedFeatures(
            this SPFeatureCollection spFeatures,
            Location location,
            out IEnumerable<FeatureDefinition> nonFarmFeatureDefinitions)
        {
            var features = new List<ActivatedFeature>();

            List<FeatureDefinition> nonFarmFeatureDefinitionsTracker = null;

            if (spFeatures != null && spFeatures.Count > 0)
            {
                foreach (var f in spFeatures)
                {
                    features.Add(f.ToActivatedFeature(location));
                }
            }

            nonFarmFeatureDefinitions = nonFarmFeatureDefinitionsTracker;
            return features;
        }

        public static FeatureDefinition ToFeatureDefinition(this SPFeatureDefinition spFeatureDefinition, string sandboxedSolutionLocationId)
        {
            var cultureInfo = new System.Globalization.CultureInfo(1033);

            if (spFeatureDefinition == null)
            {
                return null;
            }

            // If a feature definition is removed or orphaned, scope is set to undefined
            // therefore, scope is checked first and set in all exceptions later
            Scope defScope;
            try
            {
                defScope = spFeatureDefinition.Scope.ToScope();
            }
            catch (Exception)
            {
                defScope = Scope.ScopeInvalid;
            }

            Guid defId;

            try
            {
                defId = spFeatureDefinition.Id;
            }
            catch (Exception)
            {
                defId = Guid.Empty;
                defScope = Scope.ScopeInvalid;
            }

            int defCompatibilityLevel;

            try
            {
                defCompatibilityLevel = spFeatureDefinition.CompatibilityLevel;
            }
            catch (Exception)
            {
                defCompatibilityLevel = 0;
                defScope = Scope.ScopeInvalid;
            }



            string defDescription;
            try
            {
                defDescription = spFeatureDefinition.GetDescription(cultureInfo);
            }
            catch (Exception ex)
            {
                defDescription = ex.Message;
                defScope = Scope.ScopeInvalid;
            }

            string defDisplayName;
            try
            {
                defDisplayName = spFeatureDefinition.DisplayName;
            }
            catch (Exception ex)
            {
                defDisplayName = ex.Message;
                defScope = Scope.ScopeInvalid;
            }


            bool defHidden;
            try
            {
                defHidden = spFeatureDefinition.Hidden;
            }
            catch (Exception)
            {
                defHidden = false;
                defScope = Scope.ScopeInvalid;
            }

            string defName;
            try
            {
                defName = spFeatureDefinition.Name;
            }
            catch (Exception ex)
            {
                defName = ex.Message;
                defScope = Scope.ScopeInvalid;
            }

            Dictionary<string, string> defProperties;
            try
            {
                defProperties = spFeatureDefinition.Properties == null ? null :
                    spFeatureDefinition.Properties.ToProperties();
            }
            catch (Exception)
            {
                defProperties = null;
                defScope = Scope.ScopeInvalid;
            }

            string defTitle;

            try
            {
                defTitle = spFeatureDefinition.GetTitle(cultureInfo);
            }
            catch (Exception ex)
            {
                defTitle = ex.Message;
                defScope = Scope.ScopeInvalid;
            }

            Guid defSolutionId;

            try
            {
                defSolutionId = spFeatureDefinition.SolutionId;
            }
            catch (Exception)
            {
                defSolutionId = Guid.Empty;
                defScope = Scope.ScopeInvalid;
            }

            string defUIVersion;
            try
            {
                defUIVersion = spFeatureDefinition.UIVersion;
            }
            catch (Exception)
            {
                defUIVersion = string.Empty;
                defScope = Scope.ScopeInvalid;
            }

            Version defVersion;
            try
            {
                defVersion = spFeatureDefinition.Version;
            }
            catch (Exception)
            {
                defVersion = new Version("0.0.0.0");
                defScope = Scope.ScopeInvalid;
            }

            var fd = FeatureDefinitionFactory.GetFeatureDefinition(
            defId,
            defCompatibilityLevel,
            defDescription,
            defDisplayName,
            defHidden,
            defName,
            defProperties,
            defScope,
            defTitle,
            defSolutionId,
            defUIVersion,
            defVersion,
            sandboxedSolutionLocationId);

            return fd;


        }

        public static FeatureDefinitionScope ToFeatureDefinitionScope(this SPFeatureDefinitionScope scope)
        {
            switch (scope)
            {
                case SPFeatureDefinitionScope.None:
                    return FeatureDefinitionScope.None;
                case SPFeatureDefinitionScope.Farm:
                    return FeatureDefinitionScope.Farm;
                case SPFeatureDefinitionScope.Site:
                    return FeatureDefinitionScope.Site;
                case SPFeatureDefinitionScope.Web:
                    return FeatureDefinitionScope.Web;
                default:
                    throw new ArgumentException(
                        string.Format("Unexpected, undefined SPFeatureDefinitionScope '{0}' found", scope.ToString()));
            }
        }
        public static Location ToLocation(this SPWebService farm)
        {
            var id = farm.Id;

            var webAppsCount = SpLocationHelper.GetAllWebApplications().Count();

            var location = LocationFactory.GetFarm(id, webAppsCount);

            return location;
        }

        public static Location ToLocation(this SPWebApplication webApp, string parentId)
        {
            var id = webApp.Id;

            var uri = webApp.GetResponseUri(SPUrlZone.Default);

            string url;

            if (uri != null)
            {
                url = uri.ToString();
            }
            else
            {
                url = "No ResponseUri in default zone found.";
            }

            var location = LocationFactory.GetLocation(
                            id,
                            webApp.DisplayName,
                            parentId,
                            Scope.WebApplication,
                            url,
                            webApp.Sites.Count);

            return location;
        }

        public static Location ToLocation(this SPSite site, string parentId)
        {
            LockState lockState = LockState.NotLocked;

            var id = site.ID;

            string displayName;
            int childCount;
            Guid? databaseId;

            if (site.IsReadLocked)
            {
                lockState = LockState.NoAccess;

                displayName = string.Format(
                    "Site is Locked! {0}",
                    site.Url
                    );
                childCount = 0;

                databaseId = null; //TODO: find out, if dbid can be read when site collection locked

            }
            else
            {
                if (site.RootWeb != null)
                {
                    // TODO: check lock status https://technet.microsoft.com/en-us/library/ff631148(v=office.14).aspx
                    // site.IsReadLocked --> No access
                    displayName = site.RootWeb.Title;
                }
                else
                {
                    displayName = "Site has no root web!"; // well, that's normally impossible, but you never know ...
                }

                childCount = site.AllWebs.Count;

                databaseId = site.ContentDatabase.Id;
            }

            var location = LocationFactory.GetLocation(
                id,
                displayName,
                parentId,
                Scope.Site,
                site.Url,
                childCount,
                databaseId,
                lockState
                );

            return location;
        }

        public static Location ToLocation(this SPWeb web, string parentId)
        {
            var id = web.ID;
            var webUrl = web.Url;

            var location = LocationFactory.GetLocation(
                id,
                web.Title,
                parentId,
                Scope.Web,
                webUrl,
                0,
                web.Site.ContentDatabase.Id
                );

            return location;
        }
        public static IEnumerable<Location> ToLocations(this SPWebCollection spLocations, string parentId)
        {


            if (spLocations == null)
            {
                // todo log error
                return null;
            }

            var locations = new List<Location>();

            foreach (SPWeb spl in spLocations)
            {
                var l = spl.ToLocation(parentId);
                locations.Add(l);

                // https://blogs.technet.microsoft.com/stefan_gossner/2008/12/05/disposing-spweb-and-spsite-objects/
                spl.Dispose();
            }

            return locations;
        }

        public static IEnumerable<Location> ToLocations(this SPSiteCollection spLocations, string parentId)
        {


            if (spLocations == null)
            {
                // todo log error
                return null;
            }

            var locations = new List<Location>();

            foreach (SPSite spl in spLocations)
            {
                var l = spl.ToLocation(parentId);
                locations.Add(l);

                locations.AddRange(spl.AllWebs.ToLocations(l.UniqueId));

                // https://blogs.technet.microsoft.com/stefan_gossner/2008/12/05/disposing-spweb-and-spsite-objects/
                spl.Dispose();
            }

            return locations;
        }

        public static IEnumerable<Location> ToLocations(this SPWebApplicationCollection spLocations, string parentId)
        {
            if (spLocations == null)
            {
                // todo log error
                return null;
            }

            var locations = new List<Location>();

            foreach (SPWebApplication spl in spLocations)
            {
                var l = spl.ToLocation(parentId);
                locations.Add(l);
            }

            return locations;
        }

        public static Dictionary<string, string> ToProperties(this SPFeaturePropertyCollection featureProperties)
        {
            var properties = new Dictionary<string, string>();

            foreach (SPFeatureProperty p in featureProperties)
            {
                properties.Add(p.Name, p.Value);
            }

            return properties;
        }


        public static Scope ToScope(this SPFeatureScope spFeatureScope)
        {
            switch (spFeatureScope)
            {
                //case SPFeatureScope.ScopeInvalid:
                //    return Scope.ScopeInvalid;
                case SPFeatureScope.Farm:
                    return Scope.Farm;
                case SPFeatureScope.WebApplication:
                    return Scope.WebApplication;
                case SPFeatureScope.Site:
                    return Scope.Site;
                case SPFeatureScope.Web:
                    return Scope.Web;
                default:
                    return Scope.ScopeInvalid;
            }
        }
    }
}
