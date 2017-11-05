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
        private static IEnumerable<ActivatedFeature> activatedFeatures = SampleData.SampleActivatedFeatures.GetActivatedFeatures(locations);

        private static IEnumerable<FeatureDefinition> featuredefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions();

        private static IEnumerable<Location> locations = SampleData.SampleLocationHierarchy.GetAllLocations();

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            return featuredefinitions;
        }

        private static IEnumerable<ActivatedFeature> loadActivatedFeatures(Location location)
        {
            var features = activatedFeatures.Where(f => f.LocationId == location.Id).AsEnumerable<ActivatedFeature>();
            if (features == null)
            {
                features = new List<ActivatedFeature>();
            }

            return features;
        }

        private static IEnumerable<Location> loadChildLocations(Location location)
        {
            var children = locations.Where(f => f.Parent == location.Id).AsEnumerable<Location>();
           
            return children;
        }

        public IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location)
        {
            var locations = new List<Location>();

            locations.Add(location);

            var children = loadChildLocations(location);

            locations.AddRange(children);

            if (location.Scope == Core.Models.Enums.Scope.WebApplication)
            {
                foreach (Location siteCollection in children)
                {
                    locations.AddRange(loadChildLocations(siteCollection));
                }
            }

            return locations;
        }

        public IEnumerable<Location> LoadFarmAndWebApps()
        {
            var locations = new List<Location>();

            var farm = locations.Where(f => f.Scope == Core.Models.Enums.Scope.Farm).FirstOrDefault();

            locations.Add(farm);

            locations.AddRange(loadChildLocations(farm));

            return locations;
        }
    }
}
