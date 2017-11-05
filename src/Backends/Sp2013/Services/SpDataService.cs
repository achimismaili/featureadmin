using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using Microsoft.SharePoint.Administration;
using FeatureAdmin.Backends.Sp2013.Services;
using Microsoft.SharePoint;

namespace FeatureAdmin.Backends.Services
{
    public class SpDataService : IDataService
    {
        private SPWebService GetFarm()
        {
            return SPWebService.ContentService;
        }

        /// <summary>
        /// get sitecollection based on location id
        /// </summary>
        /// <param name="location">location of sharepoint object to return as SPSite</param>
        /// <returns>undisposed SPSite object</returns>
        /// <remarks>please dispose, if not needed anymore</remarks>
        private SPSite GetSite(Location location)
        {
            if (location == null)
            {
                return null;
            }
            return new SPSite(location.Id);
        }

        /// <summary>
        /// get web based on location id
        /// </summary>
        /// <param name="location">location of sharepoint object to return as SPWeb</param>
        /// <returns>undisposed SPWeb object</returns>
        /// <remarks>please dispose, if not needed anymore</remarks>
        private SPWeb GetWeb(Location location)
        {
            if (location == null)
            {
                return null;
            }

            SPSite oSPsite = new SPSite(location.Parent);
            return oSPsite.OpenWeb(location.Id);
        }

        /// <summary>
        /// returns web applications of type content or admin
        /// </summary>
        /// <param name="content">true, if content is requested, false for admin</param>
        /// <returns>content or admin web applications</returns>
        private SPWebApplicationCollection GetWebApplications(bool content)
        {
            if (content)
            {
                return SPWebService.ContentService.WebApplications;
            }
            else
            {
                return SPWebService.AdministrationService.WebApplications;
            }
        }

        /// <summary>
        /// returns a web application of type content or admin
        /// </summary>
        /// <param name="content">true, if content is requested, false for admin</param>
        /// <returns>content or admin web application</returns>
        private SPWebApplication GetWebApplication(Guid id, bool content)
        {
            var webApps = GetWebApplications(content);
            if (webApps == null)
            {
                return null;
            }
            return webApps.FirstOrDefault(wa => wa.Id == id);
        }

        private SPWebApplication GetWebApplication(Guid id)
        {
            var wa = GetWebApplication(id, true);

            if (wa != null)
            {
                return wa;
            }

            return GetWebApplication(id, false);
        }

        public IEnumerable<ActivatedFeature> LoadActivatedFeatures(SPLocation location)
        {
            SPFeatureCollection features;
            Guid parentId;

            if (location == null)
            {
                return null;
            };

            if (location.SPLocationObject == null)
            {
                location = LoadLocation(location);
            }

            if (location == null || location.SPLocationObject == null)
            {
                return null;
            }

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    var web = location.SPLocationObject as SPWeb;
                    if (web == null)
                    {
                        return null;
                    }
                    features = web.Features;
                    parentId = web.ID;
                    break;
                case Core.Models.Enums.Scope.Site:
                    var site = location.SPLocationObject as SPSite;
                    if (site == null)
                    {
                        return null;
                    }
                    features = site.Features;
                    parentId = site.ID;

                    features = ((SPSite)location.SPLocationObject).Features;
                    break;
                case Core.Models.Enums.Scope.WebApplication:
                    var wa = location.SPLocationObject as SPWebApplication;
                    if (wa == null)
                    {
                        return null;
                    }
                    features = wa.Features;
                    parentId = wa.Id;
                    break;
                case Core.Models.Enums.Scope.Farm:
                    var farm = location.SPLocationObject as SPWebService;
                    if (farm == null)
                    {
                        return null;
                    }
                    features = farm.Features;
                    parentId = farm.Id;
                    break;
                //case Core.Models.Enums.Scope.ScopeInvalid:
                //    break;
                default:
                    //TODO log that scope is invalid
                    return null;
            }

            if (features == null)
            {
                return null;
            }

            return features.ToActivatedFeatures(parentId);

        }

        public IEnumerable<SPLocation> LoadChildLocations(SPLocation parentLocation)
        {
            if (parentLocation == null)
            {
                return null;
            };

            IEnumerable<SPLocation> locations = null;

            if (parentLocation.Scope != Core.Models.Enums.Scope.Farm && parentLocation.RequiresUpdate == true)
            {
                parentLocation = LoadLocation(parentLocation);
            }

            switch (parentLocation.Scope)
            {
                //case Core.Models.Enums.Scope.Web:
                //    break;
                case Core.Models.Enums.Scope.Site:

                    if (parentLocation.SPLocationObject != null && parentLocation.SPLocationObject is SPSite)
                    {
                        var site = parentLocation.SPLocationObject as SPSite;

                        locations = SpConverter.ToSPLocations(site.AllWebs, site.ID);
                    }
                    break;
                case Core.Models.Enums.Scope.WebApplication:

                    if (parentLocation.SPLocationObject != null && parentLocation.SPLocationObject is SPWebApplication)
                    {
                        var webApp = parentLocation.SPLocationObject as SPWebApplication;

                        locations = SpConverter.ToSPLocations(webApp.Sites, webApp.Id);
                    }
                    break;

                case Core.Models.Enums.Scope.Farm:
                    locations = GetWebApplications(parentLocation.Id);
                    break;
                //case Core.Models.Enums.Scope.ScopeInvalid:
                //    break;
                default:
                    // TODO Log error: parent scope was not farm, web app or site!
                    break;
            }

            return locations;

        }

        private IEnumerable<SPLocation> GetWebApplications(Guid farmId)
        {
            List<SPLocation> webApps;

            var spWebAppsContent = GetWebApplications(true);

            var webAppsContent = SpConverter.ToSPLocations(spWebAppsContent, farmId);

            webApps = new List<SPLocation>(webAppsContent);

            var spWebAppsAdmin = GetWebApplications(false);

            var webAppsAdmin = SpConverter.ToSPLocations(spWebAppsAdmin, farmId);

            webApps.AddRange(webAppsAdmin);

            return webApps;
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var spFeatureDefinitions = SPFarm.Local.FeatureDefinitions;

            List<FeatureDefinition> features = new List<FeatureDefinition>();

            if (spFeatureDefinitions != null && spFeatureDefinitions.Count > 0)
            {
                foreach (SPFeatureDefinition spFd in spFeatureDefinitions)
                {
                    var fd = spFd.ToFeatureDefinition();

                    features.Add(fd);
                }
            }

            return features;

        }

        public SPLocation LoadLocation(Location location)
        {
            Location updatedLocation;

            if (location == null)
            {
                updatedLocation = Location.GetLocationUndefined(Guid.Empty, Guid.Empty, "ERROR - location was null!");
                return SPLocation.GetSPLocation(updatedLocation, "ERROR - location was null!", false);
            }

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    var web = GetWeb(location);
                    updatedLocation = web.ToLocation(web.Site.ID);
                    return SPLocation.GetSPLocation(updatedLocation, web, false);
                case Core.Models.Enums.Scope.Site:
                    var site = GetSite(location);
                    updatedLocation = site.ToLocation(site.WebApplication.Id);
                    return SPLocation.GetSPLocation(updatedLocation, site, false);
                case Core.Models.Enums.Scope.WebApplication:
                    var wa = GetWebApplication(location.Id);
                    updatedLocation = wa.ToLocation(wa.Farm.Id);
                    return SPLocation.GetSPLocation(updatedLocation, wa, false);
                case Core.Models.Enums.Scope.Farm:
                    var spFarm = GetFarm();
                    updatedLocation = spFarm.ToLocation();
                    return SPLocation.GetSPLocation(updatedLocation, spFarm, false);
                //case Core.Models.Enums.Scope.ScopeInvalid:
                //    break;
                default:
                    updatedLocation = Location.GetLocationUndefined(Guid.Empty, Guid.Empty, "ERROR - location scope was undefined!");
                    return SPLocation.GetSPLocation(updatedLocation, "ERROR - location scope was undefined!", false);
            }
        }
    }
}
