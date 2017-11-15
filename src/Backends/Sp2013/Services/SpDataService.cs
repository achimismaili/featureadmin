using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace FeatureAdmin.Backends.Sp2013.Services
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

        public IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location)
        {
            if (location == null)
            {
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
                                var l = w.ToLocation(location.Parent);
                                locations.Add(l);
                            }
                        }
                    });
                    break;
                case Core.Models.Enums.Scope.Site:
                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            using (SPSite sc = new SPSite(location.Id))
                            {
                                var s = sc.ToLocation(location.Parent);
                                locations.Add(s);

                                locations.AddRange(sc.AllWebs.ToLocations(location.Id));
                            }
                        });
                    break;
                case Core.Models.Enums.Scope.WebApplication:

                    var wa = GetWebApplication(location.Id);

                    if (wa != null)
                    {
                        var webApp = wa.ToLocation(location.Parent);
                        locations.Add(webApp);
                    
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

        public IEnumerable<Location> LoadFarmAndWebApps()
        {
            var locations = new List<Location>();

            var farm = GetFarm();
            
            locations.Add(farm.ToLocation());

            var farmId = farm.Id;

            var webApps = GetAllWebApplications(farmId);

            locations.AddRange(webApps);

            return locations;
        }
    }
}
