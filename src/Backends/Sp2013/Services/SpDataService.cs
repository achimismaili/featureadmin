using System.Collections.Generic;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using FeatureAdmin.Backends.Sp2013.Common;
using System;

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
            try
            {
                var farm = SpLocationHelper.GetFarm();

                SpFeatureHelper.DeactivateFeatureInFeatureCollection(farm.Features, feature.Id, force);

            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

        public string ActivateFarmFeature(FeatureDefinition feature, Location location, bool force, out ActivatedFeature activatedFeature)
        {
            activatedFeature = null;

            try
            {
                var farm = SpLocationHelper.GetFarm();

                var spActivatedFeature = SpFeatureHelper.ActivateFeatureInFeatureCollection(farm.Features, feature.Id, force);

                if (spActivatedFeature != null)
                {
                    FeatureDefinition nffd; // feature definition is not relevant in case of activation
                    activatedFeature = spActivatedFeature.ToActivatedFeature(location, out nffd);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

        public string DeactivateWebAppFeature(FeatureDefinition feature, Location location, bool force)
        {
            try
            {
                var wa = SpLocationHelper.GetWebApplication(location.Id);

                SpFeatureHelper.DeactivateFeatureInFeatureCollection(wa.Features, feature.Id, force);

            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

        public string ActivateWebAppFeature(FeatureDefinition feature, Location location, bool force, out ActivatedFeature activatedFeature)
        {
            activatedFeature = null;

            try
            {
                var wa = SpLocationHelper.GetWebApplication(location.Id);

                var spActivatedFeature = SpFeatureHelper.ActivateFeatureInFeatureCollection(wa.Features, feature.Id, force);

                if (spActivatedFeature != null)
                {
                    FeatureDefinition nffd; // feature definition is not relevant in case of activation
                    activatedFeature = spActivatedFeature.ToActivatedFeature(location, out nffd);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

        public string DeactivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            SPSite spSite = null;

            try
            {
                spSite = SpLocationHelper.GetSite(location);

                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spSite, elevatedPrivileges);

                SpFeatureHelper.DeactivateFeatureInFeatureCollection(featureCollection, feature.Id, force);

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spSite != null)
                {
                    spSite.Dispose();
                }
            }

            return string.Empty;
        }

        public string ActivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            activatedFeature = null;

            SPSite spSite = null;

            try
            {
                spSite = SpLocationHelper.GetSite(location);

                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spSite, elevatedPrivileges);

                var spActivatedFeature = SpFeatureHelper.ActivateFeatureInFeatureCollection(featureCollection, feature.Id, force);

                if (spActivatedFeature != null)
                {
                    FeatureDefinition nffd; // feature definition is not relevant in case of activation
                    activatedFeature = spActivatedFeature.ToActivatedFeature(location, out nffd);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spSite != null)
                {
                    spSite.Dispose();
                }
            }

            return string.Empty;
        }

        public string DeactivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            SPWeb spWeb = null;

            try
            {
                spWeb = SpLocationHelper.GetWeb(location);

                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spWeb, elevatedPrivileges);

                SpFeatureHelper.DeactivateFeatureInFeatureCollection(featureCollection, feature.Id, force);

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spWeb != null)
                {
                    spWeb.Dispose();
                }
            }
            return string.Empty;
        }

        public string ActivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            activatedFeature = null;

            SPWeb spWeb = null;

            try
            {
                spWeb = SpLocationHelper.GetWeb(location);
                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spWeb, elevatedPrivileges);

                var spActivatedFeature = SpFeatureHelper.ActivateFeatureInFeatureCollection(featureCollection, feature.Id, force);

                if (spActivatedFeature != null)
                {
                    FeatureDefinition nffd; // feature definition is not relevant in case of activation
                    activatedFeature = spActivatedFeature.ToActivatedFeature(location, out nffd);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spWeb != null)
                {
                    spWeb.Dispose();
                }
            }

            return string.Empty;
        }

        //private LoadedDto loadLocations(Location location)
        //{
        //    List<ActivatedFeature> activatedFeatures = new List<ActivatedFeature>();
        //    List<FeatureDefinition> definitions = new List<FeatureDefinition>();
        //    List<Location> children = new List<Location>();

        //    if (location == null)
        //    {
        //        return null;
        //    }



        //    //if farm is loaded, set loaded farm as parent and add farm as children, too
        //    if (location.Scope == Core.Models.Enums.Scope.Farm)
        //    {
        //       children.Add(location);
        //    }

        //    Core.Models.Enums.Scope? childScope;

        //    switch (location.Scope)
        //    {
        //        case Core.Models.Enums.Scope.Web:
        //            childScope = null;
        //            break;
        //        case Core.Models.Enums.Scope.Site:
        //            childScope = Core.Models.Enums.Scope.Web;
        //            break;
        //        case Core.Models.Enums.Scope.WebApplication:
        //            childScope = Core.Models.Enums.Scope.Site;
        //            break;
        //        case Core.Models.Enums.Scope.Farm:
        //            var webApps = GetAllWebApplications(location.Id);

        //            children.AddRange(webApps);


        //            break;
        //        case Core.Models.Enums.Scope.ScopeInvalid:
        //            childScope = null;
        //            break;
        //        default:
        //            childScope = null;
        //            break;
        //    }


        //    if (childScope != null)
        //    {
        //        children.AddRange(repository.SearchLocations(location.Id.ToString(), childScope.Value));
        //    }

        //    foreach (Location l in children)
        //    {
        //        var features = repository.GetActivatedFeatures(l);
        //        activatedFeatures.AddRange(features);
        //        var defs = repository.SearchFeatureDefinitions(l.Id.ToString(), l.Scope, false); ;
        //        definitions.AddRange(defs);
        //    }

        //    var loadedElements = new LoadedDto(
        //        location,
        //        children,
        //        activatedFeatures,
        //        definitions
        //        );

        //    var loadedMessage = new LocationsLoaded(loadedElements);

        //    return loadedMessage;
        //}



        ///// <summary>
        ///// Loads farm and web apps
        ///// </summary>
        ///// <returns>farm and web apps as children</returns>
        ///// <remarks>
        ///// adds farm as children, too
        ///// </remarks>
        //public LoadedDto LoadFarmAndWebApps()
        //{
        //    var spFarm = GetFarm();

        //    var farm = spFarm.ToLocation();

        //    return loadLocations(farm);
        //}



    }
}
