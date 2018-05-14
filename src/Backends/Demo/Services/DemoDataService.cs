using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Messages.Tasks;
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
            demoActivatedFeatures = SampleData.SampleLocationHierarchy.GetAllActivatedFeatures(demoLocations);
        }

        private static IEnumerable<FeatureDefinition> featuredefinitions;

        private static IEnumerable<Location> demoLocations;

        private static IEnumerable<ActivatedFeature> demoActivatedFeatures;

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            return featuredefinitions;
        }



        private static LocationsLoaded loadLocations(Location location)
        {
            List<ActivatedFeature> activatedFeatures = new List<ActivatedFeature>();
            List<FeatureDefinition> definitions = new List<FeatureDefinition>();
            if (location == null)
            {
                return null;
            }

            List<Location> children = new List<Location>();

            //if farm is loaded, set loaded farm as parent and add farm as children, too
            if (location.Scope == Core.Models.Enums.Scope.Farm)
            {
                location = demoLocations.Where(f => f.Scope == Core.Models.Enums.Scope.Farm).FirstOrDefault();
                children.Add(location);
            }

            children.AddRange(demoLocations.Where(f => f.Parent == location.Id).AsEnumerable<Location>());


            foreach (Location l in children)
            {
                var features = demoActivatedFeatures.Where(f => f.LocationId == location.Id).AsEnumerable<ActivatedFeature>();
                activatedFeatures.AddRange(features);
                var defs = featuredefinitions.Where(f => f.SandBoxedSolutionLocation == location.Url).AsEnumerable<FeatureDefinition>();
                definitions.AddRange(defs);
            }


            var loadedMessage = new LocationsLoaded(location, children, activatedFeatures, definitions);

            return loadedMessage;
        }

        public LocationsLoaded LoadNonFarmLocationAndChildren(Location location)
        {


            var initialLoaded = loadLocations(location);


            if (location.Scope == Core.Models.Enums.Scope.WebApplication && initialLoaded.ChildLocations.Count() > 0)
            {

                List<FeatureDefinition> featureDefinitions = new List<FeatureDefinition>(initialLoaded.Definitions);

                List<Location> locations = new List<Location>(initialLoaded.ChildLocations);

                List<ActivatedFeature> features = new List<ActivatedFeature>(initialLoaded.ActivatedFeatures);



                foreach (Location siteCollection in initialLoaded.ChildLocations)
                {
                    var child = loadLocations(siteCollection);

                    featureDefinitions.AddRange(child.Definitions);
                    locations.AddRange(child.ChildLocations);
                    features.AddRange(child.ActivatedFeatures);
                }


                var loadedMessage = new LocationsLoaded(
                    location,
                    locations,
                    features,
                    featuredefinitions
                    );

                return loadedMessage;
            }
            else
            {
                return initialLoaded;
            }

        }

        public LocationsLoaded LoadFarmAndWebApps()
        {
            return loadLocations(Core.Factories.LocationFactory.GetDummyFarmForLoadCommand());
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
