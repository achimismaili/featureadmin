using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Backends.Demo.Services
{
    public class DemoDataService : IDataService
    {
        private Repository.DemoRepository demoRepository;

        public string ServiceMode
        {
            get
            {
                return "SharePoint - DEMO MODE";
            }
        }

        public DemoDataService()
        {
            demoRepository = new Repository.DemoRepository();
        }

        public string FarmFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return ActivateFeature(feature, location, false, force, out activatedFeature);
                case FeatureAction.Deactivate:
                    throw new NotImplementedException("This kind of action is not supported!");
                case FeatureAction.Upgrade:
                    return UpgradeFeature(feature, location, false, force, out activatedFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }

        public string SiteFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return ActivateFeature(feature, location, elevatedPrivileges, force, out activatedFeature);
                case FeatureAction.Deactivate:
                    throw new NotImplementedException("This kind of action is not supported!");
                case FeatureAction.Upgrade:
                    return UpgradeFeature(feature, location, elevatedPrivileges, force, out activatedFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }

        }

        public string WebAppFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return ActivateFeature(feature, location, false, force, out activatedFeature);
                case FeatureAction.Deactivate:
                    throw new NotImplementedException("This kind of action is not supported!");
                case FeatureAction.Upgrade:
                    return UpgradeFeature(feature, location, false, force, out activatedFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }

        public string WebFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            switch (action)
            {
                case FeatureAction.Activate:
                    return ActivateFeature(feature, location, elevatedPrivileges, force, out activatedFeature);
                case FeatureAction.Deactivate:
                    throw new NotImplementedException("This kind of action is not supported!");
                case FeatureAction.Upgrade:
                    return UpgradeFeature(feature, location, elevatedPrivileges, force, out activatedFeature);
                default:
                    throw new NotImplementedException("This kind of action is not supported!");
            }
        }

        public string DeactivateFarmFeature(FeatureDefinition feature, Location location, bool force)
        {
            return DeactivateFeature(feature, location, false, force);
        }

        public string DeactivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            return DeactivateFeature(feature, location, elevatedPrivileges, force);
        }

        public string DeactivateWebAppFeature(FeatureDefinition feature, Location location, bool force)
        {
            return DeactivateFeature(feature, location, false, force);
        }

        public string DeactivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            return DeactivateFeature(feature, location, elevatedPrivileges, force);
        }

        public LoadedDto LoadFarm()
        {
            var loadedFarm = new LoadedDto(null);

            var location = demoRepository.SearchLocations(string.Empty, Scope.Farm, null).FirstOrDefault();

            if (location!= null)
            {
                var activatedFeatures = GetDemoActivatedFeatures(location.Item);
            var definitions = GetDemoFeatureDefinitions(location.Item);

            loadedFarm.AddChild(location.Item, activatedFeatures, definitions);

            return loadedFarm;
            }

            return null;
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            var farmFeatureDefinitions =

                demoRepository.SearchFeatureDefinitions(string.Empty, null, true, null).Select(i => i.Item).ToList();

            return farmFeatureDefinitions;
        }
        public LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges)
        {
            var siCos = loadChildLocations(location);

            var allChildren = new LoadedDto(location);

            foreach (var s in siCos.ChildLocations)
            {
                var webs = loadChildLocations(s);

                allChildren.AddChildren(webs.ChildLocations, webs.ActivatedFeatures, webs.Definitions);
            }

            allChildren.AddChildren(siCos.ChildLocations, siCos.ActivatedFeatures, siCos.Definitions);

            return allChildren;
        }

        public LoadedDto LoadWebApps()
        {
            return loadChildLocations(Core.Factories.LocationFactory.GetDummyFarmForLoadCommand());
        }

        private string ActivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force
            , out ActivatedFeature activatedFeature)
        {
            var props = new Dictionary<string, string>() {
                { "demo activation 'elevatedPrivileges' setting", elevatedPrivileges.ToString() },
                 { "demo activation 'force' setting", force.ToString() }

            };

            activatedFeature = Core.Factories.ActivatedFeatureFactory.GetActivatedFeature(
                feature.UniqueIdentifier
                , location.UniqueId
                , feature.DisplayName
                , false
                , props
                , DateTime.Now
                , feature.Version,
                feature.Version
                );



            demoRepository.AddActivatedFeature(activatedFeature);

            // wait 1 second in the demo
            System.Threading.Thread.Sleep(1000);

            return string.Empty;
        }

        private string DeactivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            if (location == null || feature == null)
            {
                throw new ArgumentNullException("Location or feature must not be null!");
            }

            var returnMsg = demoRepository.RemoveActivatedFeature(feature.UniqueIdentifier, location.UniqueId);

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

        private IEnumerable<ActivatedFeature> GetDemoActivatedFeatures(Location location)
        {
            var f = demoRepository.GetActivatedFeatures(location);

            return f;
        }

        private IEnumerable<FeatureDefinition> GetDemoFeatureDefinitions(Location location)
        {
            var defs = demoRepository.SearchFeatureDefinitions(location.Id.ToString(), location.Scope, false, null).Select(i => i.Item).ToList();

            return defs;
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
                if (childScope.Value == Core.Models.Enums.Scope.WebApplication)
                {
                    children = demoRepository.SearchLocations(null, childScope.Value, null).Select(i => i.Item).ToList();
                }
                else
                {
                    children = demoRepository.SearchLocations(location.Id.ToString(), childScope.Value, null).Select(i => i.Item).ToList();
                }

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

        private string UpgradeFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force
                                            , out ActivatedFeature upgradedFeature)
        {

            DeactivateFeature(feature, location, elevatedPrivileges, force);
            return ActivateFeature(feature, location, elevatedPrivileges, force, out upgradedFeature);

        }

        public string Uninstall(FeatureDefinition definition)
        {
           return demoRepository.RemoveFeatureDefinition(definition.UniqueIdentifier);
        }
    }
}
