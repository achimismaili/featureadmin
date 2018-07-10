using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Backends.Demo.Services
{
    public class DemoDataService : IDataService
    {
        private Repository.DemoRepository repository;
        public DemoDataService()
        {
            repository = new Repository.DemoRepository();
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var farmFeatureDefinitions =

                repository.SearchFeatureDefinitions(string.Empty, null, true);

            return farmFeatureDefinitions;
        }

        public LoadedDto LoadFarm(bool elevatedPrivileges)
        {
            var loadedFarm = new LoadedDto(null);

            var location = repository.SearchLocations(string.Empty, Core.Models.Enums.Scope.Farm).FirstOrDefault();

            var activatedFeatures = GetDemoActivatedFeatures(location);
            var definitions = GetDemoFeatureDefinitions(location);

            loadedFarm.AddChild(location, activatedFeatures, definitions);

            return loadedFarm;

        }

        public LoadedDto LoadWebApps(bool elevatedPrivileges)
        {
            return loadChildLocations(Core.Factories.LocationFactory.GetDummyFarmForLoadCommand());
        }

        public LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges)
        {
            var siCos = loadChildLocations(location);

            var allChildren = siCos;

            foreach (var s in siCos.ChildLocations)
            {
                var webs = loadChildLocations(s);

                allChildren.AddChildren(webs.ChildLocations, webs.ActivatedFeatures, webs.Definitions);
            }

            return allChildren;
        }

        private IEnumerable<FeatureDefinition> GetDemoFeatureDefinitions(Location location)
        {
            var defs = repository.SearchFeatureDefinitions(location.Id.ToString(), location.Scope, false);

            return defs;
        }

        private IEnumerable<ActivatedFeature> GetDemoActivatedFeatures(Location location)
        {
            var f = repository.GetActivatedFeatures(location);

            return f;
        }

        private LoadedDto loadChildLocations(Location location)
        {

            var loadedElements = new LoadedDto(location);


            Core.Models.Enums.Scope? childScope;

            switch (location.Scope)
            {
                case Core.Models.Enums.Scope.Web:
                    throw new ArgumentException("FeatureAdmin should never load child locations for a web.");
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
                    throw new ArgumentException("FeatureAdmin should never reach default scope in load child locations.");
            }

            List<Location> children = new List<Location>();

            if (childScope != null)
            {
                children = repository.SearchLocations(location.Id.ToString(), childScope.Value).ToList();

                // add all child locations at once
                loadedElements.AddChildLocations(children);

                foreach (var l in children)
                {
                    var activatedFeatures = GetDemoActivatedFeatures(l);
                    loadedElements.AddActivatedFeatures(activatedFeatures);

                    var definitions = GetDemoFeatureDefinitions(l);
                    loadedElements.AddFeatureDefinitions(definitions);
                }
            }

            return loadedElements;
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
