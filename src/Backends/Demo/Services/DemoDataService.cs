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
            demoFeaturedefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions().ToList();
            demoLocations = SampleData.SampleLocationHierarchy.GetAllLocations().ToList();
            demoActivatedFeatures = SampleData.SampleLocationHierarchy.GetAllActivatedFeatures(demoLocations).ToList();
        }

        private List<FeatureDefinition> demoFeaturedefinitions;

        private List<Location> demoLocations;

        private List<ActivatedFeature> demoActivatedFeatures;

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var farmFeatureDefinitions =  demoFeaturedefinitions.Where(fd => string.IsNullOrEmpty(fd.SandBoxedSolutionLocation)).ToList();

            return farmFeatureDefinitions;
        }



        private LocationsLoaded loadLocations(Location location)
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

            children.AddRange(demoLocations.Where(f => f.Parent == location.Id).ToList());


            foreach (Location l in children)
            {
                var features = demoActivatedFeatures.Where(f => f.LocationId == l.Id).ToList();
                activatedFeatures.AddRange(features);
                var defs = demoFeaturedefinitions.Where(f => f.SandBoxedSolutionLocation == l.Url && f.Scope == l.Scope).ToList();
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

                List<FeatureDefinition> fDefs = new List<FeatureDefinition>(initialLoaded.Definitions);

                List<Location> locations = new List<Location>(initialLoaded.ChildLocations);

                List<ActivatedFeature> features = new List<ActivatedFeature>(initialLoaded.ActivatedFeatures);



                foreach (Location siteCollection in initialLoaded.ChildLocations)
                {
                    var child = loadLocations(siteCollection);

                    fDefs.AddRange(child.Definitions);
                    locations.AddRange(child.ChildLocations);
                    features.AddRange(child.ActivatedFeatures);
                }


                var loadedMessage = new LocationsLoaded(
                    location,
                    locations,
                    features,
                    fDefs
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

        
        
            public string DeactivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            if (location == null || feature == null)
            {
                throw new ArgumentNullException("Location or feature must not be null!");
            }

            var af = demoActivatedFeatures.FirstOrDefault(f => f.FeatureId == feature.Id && f.LocationId == location.Id);
            demoActivatedFeatures.Remove(af);
            return string.Empty;

            //// in sharepoint, first, the containers need to be opened ...

            //switch (location.Scope)
            //{
            //    case Core.Models.Enums.Scope.Web:
            //        // get site and web
            //        break;
            //    case Core.Models.Enums.Scope.Site:
            //        // get site
            //        break;
            //    case Core.Models.Enums.Scope.WebApplication:
            //        // get web app
            //        break;
            //    case Core.Models.Enums.Scope.Farm:
            //        // get farm
            //        break;
            //    case Core.Models.Enums.Scope.ScopeInvalid:
            //        throw new Exception("Invalid scope was not expected!");
            //    default:
            //        throw new Exception("Undefined scope!");
            //}


        }

        public string ActivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force
            , out ActivatedFeature activatedFeature)
        {
            activatedFeature = Core.Factories.ActivatedFeatureFactory.GetActivatedFeature(
                feature.Id
                , location.Id
                , feature
                , false
                ,null
                , DateTime.Now
                , feature.Version
                );

            demoActivatedFeatures.Add(activatedFeature);
            return string.Empty;
        }
    }
}
