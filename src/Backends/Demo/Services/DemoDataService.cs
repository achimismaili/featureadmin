using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using FeatureAdmin.Core.Messages.Completed;

namespace FeatureAdmin.Backends.Demo.Services
{
    public class DemoDataService : IDataService
    {
        private Repository.DemoRepository repository;
        public DemoDataService()
        {
            repository = new Repository.DemoRepository();

            //var demoLocations = SampleData.SampleLocationHierarchy.GetAllLocations().ToList();
            //var demoActivatedFeatures = SampleData.SampleLocationHierarchy.GetAllActivatedFeatures(demoLocations).ToList();
            //var demoFeaturedefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions().ToList();

            //var loadedElements = new LoadedDto(
            //    SampleData.Locations.TestFarm.Location,
            //    demoLocations,
            //    demoActivatedFeatures,
            //    demoFeaturedefinitions
            //    );

            //var initialContent = new LocationsLoaded(loadedElements);
                

            //repository.AddLoadedLocations(initialContent);
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var farmFeatureDefinitions =

                repository.SearchFeatureDefinitions(string.Empty, null, true);

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
                location = repository.SearchLocations(string.Empty, Core.Models.Enums.Scope.Farm).FirstOrDefault();
                children.Add(location);
            }

            Core.Models.Enums.Scope? childScope;

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    childScope = null;
                    break;
                case Core.Models.Enums.Scope.Site:
                    childScope = Core.Models.Enums.Scope.Web;
                    break;
                case Core.Models.Enums.Scope.WebApplication:
                    childScope = Core.Models.Enums.Scope.Site;
                    break;
                case Core.Models.Enums.Scope.Farm:
                    childScope = Core.Models.Enums.Scope.WebApplication;
                    break;
                case Core.Models.Enums.Scope.ScopeInvalid:
                    childScope = null;
                    break;
                default:
                    childScope = null;
                    break;
            }


            if (childScope != null)
            {
                children.AddRange(repository.SearchLocations(location.Id.ToString(), childScope.Value));
            }

            foreach (Location l in children)
            {
                var features = repository.GetActivatedFeatures(l);
                activatedFeatures.AddRange(features);
                var defs = repository.SearchFeatureDefinitions(l.Id.ToString(), l.Scope, false); ;
                definitions.AddRange(defs);
            }

            var loadedElements = new LoadedDto(
                location,
                children,
                activatedFeatures,
                definitions
                );

            var loadedMessage = new LocationsLoaded(loadedElements);

            return loadedMessage;
        }

        public LocationsLoaded LoadNonFarmLocationAndChildren(Location location)
        {


            var loadedPackage = loadLocations(location);


            if (location.Scope == Core.Models.Enums.Scope.WebApplication && loadedPackage.LoadedElements.ChildLocations.Count() > 0)
            {

                List<FeatureDefinition> fDefs = new List<FeatureDefinition>(loadedPackage.LoadedElements.Definitions);

                List<Location> locations = new List<Location>(loadedPackage.LoadedElements.ChildLocations);

                List<ActivatedFeature> features = new List<ActivatedFeature>(loadedPackage.LoadedElements.ActivatedFeatures);



                foreach (Location siteCollection in loadedPackage.LoadedElements.ChildLocations)
                {
                    var child = loadLocations(siteCollection);

                    fDefs.AddRange(child.LoadedElements.Definitions);
                    locations.AddRange(child.LoadedElements.ChildLocations);
                    features.AddRange(child.LoadedElements.ActivatedFeatures);
                }

                var loadedElements = new LoadedDto(
                    location,
                    locations,
                    features,
                    fDefs
                    );

                var loadedMessage = new LocationsLoaded(loadedElements);

                return loadedMessage;
            }
            else
            {
                return loadedPackage;
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

            var returnMsg = repository.RemoveActivatedFeature(feature.Id, location.Id);

            // wait 1 second in the demo
            System.Threading.Thread.Sleep(1000);

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
            var props = new Dictionary<string, string>() {
                { "demo activation 'elevatedPrivileges' setting", elevatedPrivileges.ToString() },
                 { "demo activation 'force' setting", force.ToString() }

            };

            activatedFeature = Core.Factories.ActivatedFeatureFactory.GetActivatedFeature(
                feature.Id
                , location.Id
                , feature
                , false
                , props
                , DateTime.Now
                , feature.Version
                );



            repository.AddActivatedFeature(activatedFeature);

            // wait 1 second in the demo
            System.Threading.Thread.Sleep(1000);

            return string.Empty;
        }
    }
}
