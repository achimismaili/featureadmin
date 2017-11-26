using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Backends.Sp2013
{
    public static class SpConverter
    {
        public static string GetDefinitionInstallationScope(bool isFeatureDefinitionScopeEqualFarm, string featureParentUrl)
        {
            string definitionInstallationScope = isFeatureDefinitionScopeEqualFarm ?
                "Farm" : featureParentUrl;

            return definitionInstallationScope;
        }


        public static ActivatedFeature ToActivatedFeature(this SPFeature spFeature, Guid parentId, Scope parentScope, string parentUrl)
        {
            FeatureDefinition definition = null;
            bool faulty = false;

            string definitionInstallationScope = GetDefinitionInstallationScope(spFeature.FeatureDefinitionScope == SPFeatureDefinitionScope.Farm, parentUrl);

            try
            {
                if (spFeature.Definition != null)
                {
                    var fDef = spFeature.Definition;
                    definition = fDef.ToFeatureDefinition(definitionInstallationScope);
                }
                else
                {
                    definition = FeatureDefinitionFactory.GetFaultyDefinition(spFeature.DefinitionId, parentScope, spFeature.Version, definitionInstallationScope);
                    faulty = true;
                }
            }
            catch (Exception)
            {
                faulty = true;
            }


            var feature = ActivatedFeatureFactory.GetActivatedFeature(
                spFeature.DefinitionId,
                parentId,
                definition,
                faulty,
                spFeature.Properties == null ? null :
                spFeature.Properties.ToProperties(),
                spFeature.TimeActivated,
                spFeature.Version,
                definitionInstallationScope
                );

            return feature;
        }


        public static IEnumerable<ActivatedFeature> ToActivatedFeatures(this SPFeatureCollection spFeatures, Location parent)
        {
            return ToActivatedFeatures(spFeatures, parent.Id, parent.Scope, parent.Url);
        }

/// <summary>
/// 
/// </summary>
/// <param name="spFeatures">the features</param>
/// <param name="parentId">id of parent</param>
/// <param name="parentScope">scope of parent</param>
/// <param name="parentUrl">required only for site and web, others can write "Farm"</param>
/// <returns></returns>
        public static IEnumerable<ActivatedFeature> ToActivatedFeatures(this SPFeatureCollection spFeatures, Guid parentId, Scope parentScope, string parentUrl)
        {
            var features = new List<ActivatedFeature>();

            if (spFeatures != null && spFeatures.Count > 0)
            {
                foreach (var f in spFeatures)
                {
                    features.Add(f.ToActivatedFeature(parentId, parentScope, parentUrl));
                }
            }

            return features;
        }

        public static Location ToLocation(this SPWebService farm)
        {
            var id = farm.Id;
            var activatedFeatures = farm.Features.ToActivatedFeatures(id, Scope.Farm, "Farm");

            var location = LocationFactory.GetFarm(id, activatedFeatures);

            return location;
        }

        public static Location ToLocation(this SPWebApplication webApp, Guid parentId)
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


            var activatedFeatures = webApp.Features.ToActivatedFeatures(id, Scope.WebApplication, url);

            
            var location = LocationFactory.GetLocation(
                id,
                webApp.DisplayName,
                parentId,
                Scope.WebApplication,
                url,
                activatedFeatures);

            return location;
        }

        public static Location ToLocation(this SPSite site, Guid parentId)
        {
            var id = site.ID;
            var activatedFeatures = site.Features.ToActivatedFeatures(id, Scope.Site, site.Url);

            string displayName;

            if (site.RootWeb != null)
            {
                displayName = site.RootWeb.Title;
            }
            else
            {
                displayName = "Site has no root web!";
            }

            var location = LocationFactory.GetLocation(
                id,
                displayName,
                parentId,
                Scope.Site,
                site.Url,
                activatedFeatures
                );

            return location;
        }

        public static Location ToLocation(this SPWeb web, Guid parentId)
        {
            var id = web.ID;
            var webUrl = web.Url;
            var activatedFeatures = web.Features.ToActivatedFeatures(id, Scope.Web, webUrl);

            var location = LocationFactory.GetLocation(
                id,
                web.Title,
                parentId,
                Scope.Web,
                webUrl,
                activatedFeatures
                );

            return location;
        }

        public static FeatureDefinition ToFeatureDefinition(this SPFeatureDefinition spFeatureDefinition, string definitionInstallationScope)
        {
            var cultureInfo = new System.Globalization.CultureInfo(1033);

            if (spFeatureDefinition == null)
            {
                return null;
            }

            var fd = FeatureDefinitionFactory.GetFeatureDefinition(
                spFeatureDefinition.Id,
                spFeatureDefinition.CompatibilityLevel,
                spFeatureDefinition.GetDescription(cultureInfo),
                spFeatureDefinition.DisplayName,
                spFeatureDefinition.Hidden,
                spFeatureDefinition.Name,
                spFeatureDefinition.Properties == null ? null :
                spFeatureDefinition.Properties.ToProperties(),
                spFeatureDefinition.Scope.ToScope(),
                spFeatureDefinition.GetTitle(cultureInfo),
                spFeatureDefinition.SolutionId,
                spFeatureDefinition.UIVersion,
                spFeatureDefinition.Version,
                definitionInstallationScope);

            return fd;
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

        public static IEnumerable<Location> ToLocations(this SPWebCollection spLocations, Guid parentId)
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

        public static IEnumerable<Location> ToLocations(this SPSiteCollection spLocations, Guid parentId)
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

                locations.AddRange(spl.AllWebs.ToLocations(l.Id));

                // https://blogs.technet.microsoft.com/stefan_gossner/2008/12/05/disposing-spweb-and-spsite-objects/
                spl.Dispose();
            }

            return locations;
        }


        public static IEnumerable<Location> ToLocations(this SPWebApplicationCollection spLocations, Guid parentId)
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
    }
}
