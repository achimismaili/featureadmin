using System.Collections.Generic;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using FeatureAdmin.Backends.Sp2013.Common;
using System;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Backends.Sp2013.Services
{
    public class SpDataService : IDataService
    {
        public LoadedDto LoadFarm()
        {
            var loadedFarm = new LoadedDto(null);

            var farm = SpLocationHelper.GetFarm();

            var farmLocation = SpConverter.ToLocation(farm);

            var activatedFeatures = SpConverter.ToActivatedFeatures(farm.Features, farmLocation);

            // no non full trust farm features expected on scope farm
            // (sand boxed and add-in features are only scoped site and web)

            loadedFarm.AddChild(farmLocation, activatedFeatures, null);

            return loadedFarm;

        }

        public LoadedDto LoadWebApps()
        {
            var spFarm = SpLocationHelper.GetFarm();

            var farmLocation = SpConverter.ToLocation(spFarm);

            // this variable will save all loaded information of all web applications
            var loadedElements = new LoadedDto(farmLocation);

            var spWebApps = SpLocationHelper.GetAllWebApplications();

            foreach (var wa in spWebApps)
            {
                var waLocation = wa.ToLocation(farmLocation.Id);

                var activatedFeatures = wa.Features.ToActivatedFeatures(waLocation);
                loadedElements.AddActivatedFeatures(activatedFeatures);

                // no non full trust farm features expected on scope web app
                // (sand boxed and add-in features are only scoped site and web)

                loadedElements.AddChild(waLocation, activatedFeatures, null);
            }

            return loadedElements;
        }

        public LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges)
        {
            // this variable will save all loaded information of all web applications
            var loadedElements = new LoadedDto(location);

            var spWebApp = SpLocationHelper.GetWebApplication(location.Id);

            if (spWebApp == null)
            {
                // The web application might have got deleted!?
                // throw?
                return null;
            }

            var siCos = spWebApp.Sites;

            foreach (SPSite spSite in siCos)
            {
                Location siteLocation;

                if (elevatedPrivileges)
                {
                    siteLocation = SpSiteElevation.SelectAsSystem(spSite, SpConverter.ToLocation, location.Id);
                }
                else
                {
                    siteLocation = SpConverter.ToLocation(spSite, location.Id);
                }


                // meaning that they are not installed in the farm, rather on site or web level
                IEnumerable<FeatureDefinition> nonFarmFeatureDefinitions;

                SPFeatureCollection siteFeatureCollection = SpFeatureHelper.GetFeatureCollection(spSite, elevatedPrivileges);

                var activatedSiteFeatures = siteFeatureCollection.ToActivatedFeatures(siteLocation, out nonFarmFeatureDefinitions);

                loadedElements.AddChild(siteLocation, activatedSiteFeatures, nonFarmFeatureDefinitions);

                if (spSite != null && spSite.AllWebs != null)
                {
                    SPWebCollection allWebs;

                    if (elevatedPrivileges)
                    {
                        allWebs = SpSiteElevation.SelectAsSystem(spSite, SpLocationHelper.GetAllWebs);
                    }
                    else
                    {
                        allWebs = spSite.AllWebs;
                    }

                    foreach (SPWeb spWeb in allWebs)
                    {
                        var webLocation = SpConverter.ToLocation(spWeb, siteLocation.Id);

                        nonFarmFeatureDefinitions = null;

                        SPFeatureCollection webFeatureCollection = SpFeatureHelper.GetFeatureCollection(spWeb, elevatedPrivileges);

                        var activatedWebFeatures = webFeatureCollection.ToActivatedFeatures(webLocation, out nonFarmFeatureDefinitions);

                        loadedElements.AddChild(webLocation, activatedWebFeatures, nonFarmFeatureDefinitions);

                        // https://blogs.technet.microsoft.com/stefan_gossner/2008/12/05/disposing-spweb-and-spsite-objects/
                        spWeb.Dispose();
                    }
                }

                spSite.Dispose();
            }



            return loadedElements;
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var spFeatureDefinitions = SPFarm.Local.FeatureDefinitions;

            List<FeatureDefinition> features = new List<FeatureDefinition>();

            if (spFeatureDefinitions != null && spFeatureDefinitions.Count > 0)
            {
                foreach (SPFeatureDefinition spFd in spFeatureDefinitions)
                {
                    var fd = spFd.ToFeatureDefinition(null);

                    features.Add(fd);
                }
            }

            return features;
        }

        public string DeactivateFarmFeature(FeatureDefinition feature, Location location, bool force)
        {
            // this will always be null
            ActivatedFeature deactivationDummy;
            return FarmFeatureAction(feature, location, FeatureAction.Deactivate, force, out deactivationDummy);
        }

        public string FarmFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature resultingFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return SpFeatureAction.FarmFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.ActivateFeatureInFeatureCollection,
                        force,
                        out resultingFeature);
                case FeatureAction.Deactivate:
                    return SpFeatureAction.FarmFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.DeactivateFeatureInFeatureCollectionReturnsNull,
                        force,
                        out resultingFeature);
                case FeatureAction.Upgrade:
                    return SpFeatureAction.FarmFeatureAction(
                       feature,
                       location,
                       SpFeatureHelper.UpgradeFeatureInFeatureCollection,
                       force,
                       out resultingFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }

        public string DeactivateWebAppFeature(FeatureDefinition feature, Location location, bool force)
        {
            // this will always be null
            ActivatedFeature deactivationDummy;
            return WebAppFeatureAction(feature, location, FeatureAction.Deactivate, force, out deactivationDummy);
        }

        public string WebAppFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature resultingFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return SpFeatureAction.WebAppFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.ActivateFeatureInFeatureCollection,
                        force,
                        out resultingFeature);
                case FeatureAction.Deactivate:
                    return SpFeatureAction.WebAppFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.DeactivateFeatureInFeatureCollectionReturnsNull,
                        force,
                        out resultingFeature);
                case FeatureAction.Upgrade:
                    return SpFeatureAction.WebAppFeatureAction(
                       feature,
                       location,
                       SpFeatureHelper.UpgradeFeatureInFeatureCollection,
                       force,
                       out resultingFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }

        public string DeactivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            // this will always be null
            ActivatedFeature deactivationDummy;
            return SiteFeatureAction(feature, location, FeatureAction.Deactivate, elevatedPrivileges, force, out deactivationDummy);
        }

        public string SiteFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature resultingFeature)
        {
                switch (action)
                {
                    case FeatureAction.Activate:
                        return SpFeatureAction.SiteFeatureAction(
                            feature,
                            location,
                            SpFeatureHelper.ActivateFeatureInFeatureCollection,
                            elevatedPrivileges,
                            force,
                            out resultingFeature);
                    case FeatureAction.Deactivate:
                        return SpFeatureAction.SiteFeatureAction(
                            feature,
                            location,
                            SpFeatureHelper.DeactivateFeatureInFeatureCollectionReturnsNull,
                            elevatedPrivileges,
                            force,
                            out resultingFeature);
                    case FeatureAction.Upgrade:
                        return SpFeatureAction.SiteFeatureAction(
                           feature,
                           location,
                           SpFeatureHelper.UpgradeFeatureInFeatureCollection,
                           elevatedPrivileges,
                           force,
                           out resultingFeature);
                    default:
                        throw new NotImplementedException("This kind of action is not supported!");
                }
            }

        public string DeactivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            // this will always be null
            ActivatedFeature deactivationDummy;
            return WebFeatureAction(feature, location,FeatureAction.Deactivate, elevatedPrivileges, force, out deactivationDummy );
        }

        public string WebFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature resultingFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return SpFeatureAction.WebFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.ActivateFeatureInFeatureCollection,
                        elevatedPrivileges,
                        force,
                        out resultingFeature);
                case FeatureAction.Deactivate:
                    return SpFeatureAction.WebFeatureAction(
                        feature,
                        location,
                        SpFeatureHelper.DeactivateFeatureInFeatureCollectionReturnsNull,
                        elevatedPrivileges,
                        force,
                        out resultingFeature);
                case FeatureAction.Upgrade:
                    return SpFeatureAction.WebFeatureAction(
                       feature,
                       location,
                       SpFeatureHelper.UpgradeFeatureInFeatureCollection,
                       elevatedPrivileges,
                       force,
                       out resultingFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }
    }
}
