using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Backends.Demo.Services
{
    public class DemoDataService : IDataService
    {
        public DemoDataService()
        {
            featuredefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions();
            demoLocations = SampleData.SampleLocationHierarchy.GetAllLocations();

        }

        private static IEnumerable<FeatureDefinition> featuredefinitions;

        private static IEnumerable<Location> demoLocations;

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            return featuredefinitions;
        }

        

        private static IEnumerable<Location> loadChildLocations(Location location)
        {
            if (location == null)
            {
                return null;
            }
            var children = demoLocations.Where(f => f.Parent == location.Id).AsEnumerable<Location>();

            return children;
        }

        public IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location, out Location parent)
        {
            var locations = new List<Location>();

            parent = location;

            var children = loadChildLocations(location);

            if (children != null)
            {
                locations.AddRange(children);

                if (location.Scope == Core.Models.Enums.Scope.WebApplication)
                {
                    foreach (Location siteCollection in children)
                    {
                        locations.AddRange(loadChildLocations(siteCollection));
                    }
                }
            }
            return locations;
        }

        public IEnumerable<Location> LoadFarmAndWebApps(out Location farm)
        {
            var locations = new List<Location>();

            farm = demoLocations.Where(f => f.Scope == Core.Models.Enums.Scope.Farm).FirstOrDefault();

            // in this set, parent is included in result
            locations.Add(farm);

            locations.AddRange(loadChildLocations(farm));

            return locations;
        }

        public int FeatureToggle(Location location, FeatureDefinition feature, bool add, bool force)
        {
            if (location == null || feature == null)
            {
                throw new ArgumentNullException("Location must not be null!");
            }

            var counter = 0;

            switch (feature.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    break;
                case Core.Models.Enums.Scope.Site:
                    break;
                case Core.Models.Enums.Scope.WebApplication:
                    break;
                case Core.Models.Enums.Scope.Farm:
                    break;
                case Core.Models.Enums.Scope.ScopeInvalid:
                    throw new Exception("Invalid scope was not expected!");
                default:
                    throw new Exception("Undefined scope!");
            }

            return counter;
        }
    }
}
