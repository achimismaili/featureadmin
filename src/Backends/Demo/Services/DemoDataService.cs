using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Backends.Services
{
    public class DemoDataService : IDataService
    {
        private static IEnumerable<ActivatedFeature> activatedFeatures = SampleData.SampleActivatedFeatures.GetActivatedFeatures(locations);

        private static IEnumerable<FeatureDefinition> featuredefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions();

        private static IEnumerable<Location> locations = SampleData.SampleLocationHierarchy.GetAllLocations();

        public IEnumerable<FeatureDefinition> LoadFeatureDefinitions()
        {
            throw new NotImplementedException();
        }

        public Location ReLoadLocation(Location location)
        {
            if (location == null)
            {
                location = Location.GetLocationUndefined(Guid.Empty, Guid.Empty, "location was null!");
            }

            if(location.Scope == Core.Models.Enums.Scope.Farm)
            {
                location = locations.Where(f => f.Scope == Core.Models.Enums.Scope.Farm).FirstOrDefault();
            }

            var features = activatedFeatures.Where(f => f.LocationId == location.Id).AsEnumerable<ActivatedFeature>();
            if (features == null)
            {
                features = new List<ActivatedFeature>();
            }
            var children = locations.Where(f => f.Parent == location.Id).AsEnumerable<Location>();
            if (children == null)
            {
                children = new List<Location>();
            }

            location.ActivatedFeataures = features;
            location.ChildLocations = children;

            return location;
        }
    }
}
