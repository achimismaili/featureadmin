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

        public IEnumerable<ActivatedFeature> LoadActivatedFeatures(SPLocation location)
        {
            return loadActivatedFeatures(location);
        }

        public IEnumerable<SPLocation> LoadChildLocations(SPLocation parentLocation)
        {
            return loadChildLocations(parentLocation);
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            return featuredefinitions;
        }

        public SPLocation LoadLocation(Location location)
        {
            if (location == null)
            {
                location = Location.GetLocationUndefined(Guid.Empty, Guid.Empty, "location was null!");
            }

            if (location.Scope == Core.Models.Enums.Scope.Farm)
            {
                location = locations.Where(f => f.Scope == Core.Models.Enums.Scope.Farm).FirstOrDefault();
            }

            // IEnumerable<ActivatedFeature> features = loadActivatedFeatures(location);

            // IEnumerable<SPLocation> children = loadChildLocations(location);

            var spLocation = SPLocation.GetSPLocation(location, "demoDummy", false);

            return spLocation;
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

        private static IEnumerable<SPLocation> loadChildLocations(Location location)
        {
            var spChildren = new List<SPLocation>();

            var children = locations.Where(f => f.Parent == location.Id).AsEnumerable<Location>();
            if (children != null)
            {
                foreach (Location l in children)
                {
                    spChildren.Add(SPLocation.ToSPLocation(l));
                }
            }
            return spChildren;
        }
    }
}
