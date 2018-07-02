using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Messages.Completed;

namespace FeatureAdmin.Backends.Sp2013.Services
{
    public class SpDataService : IDataService
    {
        private static SPWebService GetFarm()
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

        public static IEnumerable<SPWebApplication> GetAllWebApplications()
        {
            var webApps = GetWebApplications(true);

            return webApps.Concat(GetWebApplications(false).AsEnumerable());
        }

        /// <summary>
        /// returns web applications of type content or admin
        /// </summary>
        /// <param name="content">true, if content is requested, false for admin</param>
        /// <returns>content or admin web applications</returns>
        private static SPWebApplicationCollection GetWebApplications(bool content)
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

        public IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location, out Location parent)
        {
            Location updatedLocation = null;

            if (location == null)
            {
                parent = updatedLocation;
                return null;
            };

            var locations = new List<Location>();

            if (location.Scope == Core.Models.Enums.Scope.Farm)
            {
                throw new Exception("This method must not be called with farm location");
            }

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite sc = new SPSite(location.Parent))
                        {
                            using (SPWeb w = sc.OpenWeb(location.Id))
                            {
                                updatedLocation = w.ToLocation(location.Parent);

                                locations.Add(updatedLocation);
                            }
                        }
                    });
                    break;
                case Core.Models.Enums.Scope.Site:
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite sc = new SPSite(location.Id))
                        {
                            updatedLocation = sc.ToLocation(location.Parent);

                            locations.Add(updatedLocation);

                            locations.AddRange(sc.AllWebs.ToLocations(location.Id));
                        }
                    });
                    break;
                case Core.Models.Enums.Scope.WebApplication:

                    var wa = GetWebApplication(location.Id);

                    if (wa != null)
                    {
                        updatedLocation = wa.ToLocation(location.Parent);
                        locations.Add(updatedLocation);

                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            locations.AddRange(wa.Sites.ToLocations(location.Id));
                        });
                    }
                    break;
                //case Core.Models.Enums.Scope.Farm:
                //    // invalid
                //    break;
                //case Core.Models.Enums.Scope.ScopeInvalid:
                //    break;
                default:
                    // TODO Log error: scope was not web, web app or site!
                    break;
            }

            parent = updatedLocation;

            return locations;

        }

        private IEnumerable<Location> GetAllWebApplications(Guid farmId)
        {
            var spWebAppsContent = GetWebApplications(true);

            var webApps = new List<Location>(spWebAppsContent.ToLocations(farmId));

            var spWebAppsAdmin = GetWebApplications(false);

            webApps.AddRange(spWebAppsAdmin.ToLocations(farmId));

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
                    var fd = spFd.ToFeatureDefinition("Farm");

                    features.Add(fd);
                }
            }

            return features;
        }

        public IEnumerable<Location> LoadFarmAndWebApps(out Location farm)
        {
            var locations = new List<Location>();

            var spFarm = GetFarm();

            farm = spFarm.ToLocation();

            locations.Add(farm);

            var farmId = farm.Id;

            var webApps = GetAllWebApplications(farmId);

            locations.AddRange(webApps);

            return locations;
        }

        public int FeatureToggle(Location location, FeatureDefinition feature, bool add, bool force)
        {
            if (location == null || feature == null)
            {
                throw new ArgumentNullException("Location or feature must not be null!");
            }

            var counter = 0;

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
                                    FeatureToggleWeb(web, feature.Id, add, force);
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
                            FeatureToggleSite(site, feature.Id, feature.Scope, add, force);
                        };
                    });

                    break;
                case Core.Models.Enums.Scope.WebApplication:

                    var wa = GetWebApplication(location.Id);

                    if (wa != null)
                    {
                        FeatureToggleWebApp(wa, feature.Id, feature.Scope, add, force);
                    }
                        break;
                case Core.Models.Enums.Scope.Farm:
                    FeatureToggleFarm(feature.Id, feature.Scope, add, force);
                    break;
                case Core.Models.Enums.Scope.ScopeInvalid:
                    throw new Exception("Invalid scope was not expected!");
                default:
                    throw new Exception("Undefined scope!");
            }

            return counter;
        }

        private static int FeatureToggleFarm(Guid featureId, Scope featureScope, bool add, bool force)
        {
            var processingCounter = 0;

            var farm = GetFarm();

            if (featureScope == Scope.Farm)
            {
                processingCounter += ToggleFeatureInFeatureCollection(farm.Features, featureId, add, force);
            }

            else
            {
                var webApps = GetAllWebApplications();

                foreach (SPWebApplication webApp in webApps)
                {
                    FeatureToggleWebApp(webApp, featureId, featureScope, add, force);
                }
            }

            return processingCounter;
        }

        private static int FeatureToggleWebApp(SPWebApplication webApp, Guid featureId, Scope featureScope, bool add, bool force)
        {
            var processingCounter = 0;

            if (featureScope == Scope.WebApplication)
            {
                processingCounter += ToggleFeatureInFeatureCollection(webApp.Features, featureId, add, force);
            }

            else if (featureScope == Scope.Site || featureScope == Scope.Web)
            {
                foreach (SPSite site in webApp.Sites)
                {
                    processingCounter += FeatureToggleSite(site, featureId, featureScope, add, force);
                }
            }
            return processingCounter;
        }



        public static int FeatureToggleSite(SPSite site, Guid featureId, Scope featureScope, bool activate, bool force)
        {
            var processingCounter = 0;


            if (featureScope == Scope.Site)
            {
                processingCounter += ToggleFeatureInFeatureCollection(site.Features, featureId, activate, force);
            }
            else if (featureScope == Scope.Web)
            {
                foreach (SPWeb web in site.AllWebs)
                {
                    processingCounter += ToggleFeatureInFeatureCollection(web.Features, featureId, activate, force);
                }
            }
            return processingCounter;
        }
        public static int FeatureToggleWeb(SPWeb web, Guid featureId, bool add, bool force)
        {
            var processingCounter = 0;


            processingCounter += ToggleFeatureInFeatureCollection(web.Features, featureId, add, force);

            return processingCounter;
        }
        private static int ToggleFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool activate, bool force)
        {

            var featuresModifiedCounter = 0;


            if (activate)
            {
                // activate feature
                var feature = features.Add(featureId, force);
                if (feature != null)
                {
                    featuresModifiedCounter++;
                }
            }
            else
            {
                // deactivate feature
                var featuresActiveBefore = features.Count();

                features.Remove(featureId, force);
                if (featuresActiveBefore > features.Count)
                {
                    featuresModifiedCounter++;
                }
            }

            return featuresModifiedCounter;
        }

        public LocationsLoaded LoadNonFarmLocationAndChildren(Location location)
        {
            throw new NotImplementedException();
        }

        public LocationsLoaded LoadFarmAndWebApps()
        {
            throw new NotImplementedException();
        }

        public string DeactivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            throw new NotImplementedException();
        }

        public string ActivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            throw new NotImplementedException();
        }
    }
}
