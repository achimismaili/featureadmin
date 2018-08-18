using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.OrigoDb
{
    [Serializable]
    public class FeatureModel : Model
    {
        /// <summary>
        /// KeyValuePair Guid, Guid = featureId, locationId
        /// </summary>
        protected List<ActivatedFeature> ActivatedFeatures;
        protected Dictionary<string, FeatureDefinition> FeatureDefinitions;
        protected Dictionary<string, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            FeatureDefinitions = new Dictionary<string, FeatureDefinition>();
            Locations = new Dictionary<string, Location>();
        }

        [Command]
        public string AddActivatedFeature(ActivatedFeature feature)
        {
            ActivatedFeature featureToAdd;

            featureToAdd = ActivatedFeatures.FirstOrDefault(f => f == feature);

            if (featureToAdd == null)
            {
                ActivatedFeatures.Add(feature);
                return null;
            }
            else
            {
                return string.Format("Problem in Repository. Could not add. Activated Feature already existed in repository with id '{0}' in location '{1}' - Please 'Reload'", feature.FeatureId, feature.LocationId);
            }
        }

        [Command]
        public void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures)
        {
            if (activatedFeatures != null)
            {
                ActivatedFeatures.AddRange(activatedFeatures);
            }
        }

        [Command]
        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> farmFeatureDefinitions)
        {
            if (farmFeatureDefinitions != null)
            {
                foreach (FeatureDefinition fd in farmFeatureDefinitions)
                {
                    if (!FeatureDefinitions.ContainsKey(fd.UniqueIdentifier))
                    {
                        FeatureDefinitions.Add(fd.UniqueIdentifier, fd);
                    }
                }
            }
        }
        [Command]
        public string AddLoadedLocations(LoadedDto loadedElements)
        {
            var error = AddLocations(loadedElements.ChildLocations);

            AddActivatedFeatures(loadedElements.ActivatedFeatures);

            AddFeatureDefinitions(loadedElements.Definitions);

            return error;
        }

        [Command]
        public string AddLocations(IEnumerable<Location> locations)
        {
            try
            {
                if (locations != null)
                {
                    Locations = Locations.Concat(locations.ToDictionary(l => l.UniqueId)).ToDictionary(l => l.Key, l => l.Value); ;
                }
            }
            catch (Exception ex)
            {
                string errInfo = ex.Message;

                Location doublette = null;

                // try to find location that is tried to be added twice
                try
                {
                    doublette =
                            (from newLocs in locations
                             join l in Locations on newLocs.UniqueId equals l.Key
                             where newLocs.UniqueId == l.Key
                             select newLocs).FirstOrDefault();
                }
                catch (Exception ex2)
                {

                    errInfo += ex2.Message;
                }

                if (doublette == null)
                {
                    return errInfo;
                }
                else
                {
                    // TODO switch ID to scope-Url, as restored locations can have same id (e.g. SPWeb of a restored SPSite)

                    //return string.Format(
                    //    "Error when trying to add a location. The location with id '{0}' and Url '{1}' was already loaded. This should never happen! Try to reload or restart. Error: '{2}'",
                    //    doublette.Id, doublette.Url, errInfo);
                }
            }

            return string.Empty;

        }

        public IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToCleanUp()
        {
            // get all faulty activated features 
            var activatedFaultyFeaturesInFarm = ActivatedFeatures.Where(f => f.Faulty);

            return GetAsActivatedFeatureSpecial(activatedFaultyFeaturesInFarm);
        }

        public void Clear()
        {
            ActivatedFeatures.Clear();
            FeatureDefinitions.Clear();
            Locations.Clear();
        }

        public IEnumerable<ActivatedFeatureSpecial> GetAllFeaturesToUpgrade()
        {
            // get all activated features to upgrade
            var activatedUpgradeableFeaturesInFarm = ActivatedFeatures.Where(f => f.CanUpgrade);

            return GetAsActivatedFeatureSpecial(activatedUpgradeableFeaturesInFarm);
        }

        public IEnumerable<ActivatedFeatureSpecial> GetAsActivatedFeatureSpecial(IEnumerable<ActivatedFeature> activatedFeatures)
        {
            if (activatedFeatures == null || activatedFeatures.Count() < 1)
            {
                return new List<ActivatedFeatureSpecial>();
            }

            var conversionResult = from af in activatedFeatures
                                   join d in FeatureDefinitions on af.FeatureId equals d.Key
                                   into featuresAndDefinitions
                                   from someDefinitionsEmpty in featuresAndDefinitions.DefaultIfEmpty()
                                   join l in Locations on af.LocationId equals l.Key
                                   select new ActivatedFeatureSpecial(af, someDefinitionsEmpty.Value, l.Value);

            return conversionResult.ToList();
        }

        /// <summary>
        /// searches for special features with search input and scope-filter
        /// </summary>
        /// <param name="source">collection of special features to search in</param>
        /// <param name="searchInput">search input</param>
        /// <param name="selectedScopeFilter">scope filter</param>
        /// <returns></returns>
        public IEnumerable<ActiveIndicator<ActivatedFeatureSpecial>> SearchSpecialFeatures(
            IEnumerable<ActivatedFeatureSpecial> source,
            string searchInput,
            Scope? selectedScopeFilter,
            FeatureDefinition selectedFeatureDefinition)
        {
            if (source == null || source.Count() < 1)
            {
                return new ActiveIndicator<ActivatedFeatureSpecial>[0];
            }

            var searchResult = source;

            if (!string.IsNullOrEmpty(searchInput))
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                searchResult = searchResult.Where(
                   f => f.ActivatedFeature.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                   f.Location.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                   f.Location.Url.ToLower().Contains(lowerCaseSearchInput) ||
                    f.ActivatedFeature.FeatureId.Contains(lowerCaseSearchInput) ||
                    f.ActivatedFeature.LocationId.Contains(lowerCaseSearchInput)
                    );
            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(f => f.Location.Scope == selectedScopeFilter.Value);
            }

            if (selectedFeatureDefinition == null || !searchResult.Any())
            {
                return searchResult.Select(afs => new ActiveIndicator<ActivatedFeatureSpecial>(afs, false)).ToArray();
            }
            else
            {
                return searchResult.Select(afs =>
                new ActiveIndicator<ActivatedFeatureSpecial>(
                    afs,
                    afs.ActivatedFeature.FeatureId == selectedFeatureDefinition.UniqueIdentifier)
                    )
                .ToArray();
            }

        }

        public ActivatedFeature GetActivatedFeature(string featureId, string locationId)
        {
            return ActivatedFeatures.FirstOrDefault(f => f.FeatureId == featureId && f.LocationId == locationId);
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition)
        {
            if (featureDefinition != null)
            {
                var result = ActivatedFeatures.Where(af => af.FeatureId == featureDefinition.UniqueIdentifier)
                    .ToList();

                // try to find some activated features, if scope is invalid (e.g. definition not available in local hive)
                if (!result.Any() && featureDefinition.Scope == Scope.ScopeInvalid && featureDefinition.SandBoxedSolutionLocation == null)
                {
                    return ActivatedFeatures.Where(af => af.FeatureId.Contains(featureDefinition.Id.ToString()) &&
                    (af.FeatureDefinitionScope == FeatureDefinitionScope.Farm ||
                    af.FeatureDefinitionScope == FeatureDefinitionScope.None))
                    .ToList();
                }

                return result;
            }

            return null;
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location)
        {
            return ActivatedFeatures.Where(af => af.LocationId == location.UniqueId)
                .ToList();
        }

        public IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location)
        {
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location, featureDefinition.SandBoxedSolutionLocation);

            var prefilteredActivatedFeatures = ActivatedFeatures.Where(f => f.FeatureId == featureDefinition.UniqueIdentifier).ToList();

            // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
            var locationsCanActivate =
                            (from loc in allLocationsOfFeatureScope
                             join af in prefilteredActivatedFeatures on loc.UniqueId equals af.LocationId into gj
                             from subAf in gj.DefaultIfEmpty()
                                 // following is the only different line compared to ..can deactivate
                             where subAf == null
                             select loc).ToList();


            return locationsCanActivate;

        }

        public IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location)
        {
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location, featureDefinition.SandBoxedSolutionLocation);

            var prefilteredActivatedFeatures = ActivatedFeatures.Where(f => f.FeatureId == featureDefinition.UniqueIdentifier).ToList();

            // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
            var locationsCanDeactivate =
                            (from loc in allLocationsOfFeatureScope
                             join af in prefilteredActivatedFeatures on loc.UniqueId equals af.LocationId into gj

                             from subAf in gj.DefaultIfEmpty()
                                 // following is the only different line compared to ..can activate
                             where subAf != null && subAf.FeatureId == featureDefinition.UniqueIdentifier
                             select loc).ToList();


            return locationsCanDeactivate;
        }

        public IEnumerable<Location> GetLocationsDirectChildren(Location location)
        {
            return Locations.Where(l => l.Value.ParentId == location.UniqueId)
                .Select(l => l.Value)
                .ToList();
        }

        [Command]
        public string RemoveActivatedFeature(string featureId, string locationId)
        {
            var featureToRemove = ActivatedFeatures.FirstOrDefault(f => f.FeatureId == featureId && f.LocationId == locationId);

            if (featureToRemove != null)
            {
                if (ActivatedFeatures.Remove(featureToRemove))
                {
                    return null;
                }
                else
                {
                    return string.Format("Repository problem when removing feature with id '{0}' in location '{1}' - Please 'Reload'", featureId, locationId);
                }
            }
            else
            {
                return string.Format("Activated Feature not found with id '{0}' in location '{1}' - Please 'Reload'", featureId, locationId);
            }
        }

        [Command]
        public string RemoveFeatureDefinition(string uniqueIdentifier)
        {
            var definitionToRemove = FeatureDefinitions[uniqueIdentifier];

            if (definitionToRemove != null)
            {
                if (FeatureDefinitions.Remove(uniqueIdentifier))
                {
                    return null;
                }
                else
                {
                    return string.Format("Repository problem when removing feature definition with unique id '{0}' - Please 'Reload'", uniqueIdentifier);
                }
            }
            else
            {
                return string.Format("Feature definition not found with unique id '{0}' - Please 'Reload'", uniqueIdentifier);
            }
        }

        public IEnumerable<ActiveIndicator<FeatureDefinition>> SearchFeatureDefinitions(
            string searchInput,
            Scope? selectedScopeFilter,
            bool? onlyFarmFeatures,
            Location selectedLocation)
        {
            IEnumerable<FeatureDefinition> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = FeatureDefinitions.Values;
            }
            else
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                searchResult =
                   FeatureDefinitions.Where(fd => fd.Value.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                        fd.Value.Title.ToLower().Contains(lowerCaseSearchInput) ||
                        fd.Key.Contains(searchInput))
                        .Select(fd => fd.Value);


                // search for features in activated features, if no definition was found with that id
                if (!searchResult.Any())
                {
                    // see also https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-inner-joins
                    searchResult =
                        ActivatedFeatures
                        .Where(f => f.LocationId.Contains(searchInput))
                        .Join(
                        FeatureDefinitions,
                        f => f.FeatureId,
                        fd => fd.Key,
                        (f, fd) => fd.Value);
                }


            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == selectedScopeFilter.Value);
            }

            if (selectedLocation == null || !searchResult.Any())
            {
                return searchResult.Select(f => new ActiveIndicator<FeatureDefinition>(f, false)).ToArray();
            }
            else
            {
                var activeFeaturesWithSelectedId = ActivatedFeatures.Where(af => af.LocationId == selectedLocation.UniqueId);

                // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
                var resultWithActiveIndicator =
                                (from fd in searchResult
                                 join af in activeFeaturesWithSelectedId on fd.UniqueIdentifier equals af.FeatureId into gj
                                 from subAf in gj.DefaultIfEmpty()
                                 select new ActiveIndicator<FeatureDefinition>(fd, subAf != null )).ToArray();

                return resultWithActiveIndicator;
            }


        }
        public IEnumerable<ActiveIndicator<Location>> SearchLocations(
            string searchInput,
            Scope? selectedScopeFilter,
            FeatureDefinition selectedFeatureDefinition)
        {
            IEnumerable<Location> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = Locations.Values;
            }
            else
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                searchResult = Locations.Where(
                   l => l.Value.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                        l.Value.Url.ToLower().Contains(lowerCaseSearchInput) ||
                        l.Key.Contains(searchInput) ||
                        l.Value.ParentId.Contains(searchInput)
                        ).Select(l => l.Value);

                // search for feature Ids in activated features, if no location was found with that id
                if (!searchResult.Any())
                {
                    // see also https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-inner-joins
                    searchResult =
                        ActivatedFeatures
                        .Where(f => f.FeatureId.Contains(searchInput))
                        .Join(
                        Locations,
                        f => f.LocationId,
                        l => l.Key,
                        (f, l) => l.Value);
                }
            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == selectedScopeFilter.Value);
            }

            if (selectedFeatureDefinition == null || !searchResult.Any())
            {
                return searchResult.Select(l => new ActiveIndicator<Location>(l, false)).ToArray();
            }
            else
            {
                var activeFeaturesWithSelectedId = ActivatedFeatures.Where(af => selectedFeatureDefinition.UniqueIdentifier == af.FeatureId);

                // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
                var resultWithActiveIndicator =
                                (from l in searchResult
                                 join af in activeFeaturesWithSelectedId on l.UniqueId equals af.LocationId into gj
                                 from subAf in gj.DefaultIfEmpty()
                                 select new ActiveIndicator<Location>(l, subAf != null)).ToArray();

                return resultWithActiveIndicator;
            }
        }

        /// <summary>
        /// Gets all locations that are available for feature de- and activation
        /// </summary>
        /// <param name="childrenScope">scope of locations (webs, sicos, web apps, farm)</param>
        /// <param name="parentLocation">parent location, all must be below</param>
        /// <param name="featureDefinitionLocationId">if feature definition is sandbox or add in, definition is not farm wide, but in site or web</param>
        /// <returns></returns>
        private IEnumerable<Location> GetChildLocationsOfScope(Scope childrenScope, Location parentLocation, string featureDefinitionLocationId)
        {
            var childLocations = new List<Location>();

            switch (parentLocation.Scope)
            {
                case Scope.Web:
                    if (childrenScope == Scope.Web)
                    {
                        childLocations.Add(parentLocation);
                    }
                    break;
                case Scope.Site:
                    if (childrenScope == Scope.Site)
                    {
                        childLocations.Add(parentLocation);
                    }
                    else if (childrenScope == Scope.Web)
                    {
                        childLocations = GetLocationsDirectChildren(parentLocation).ToList();
                    }
                    break;
                case Scope.WebApplication:
                    switch (childrenScope)
                    {
                        case Scope.Web:
                            childLocations = GetLocationsChildrensChildren(parentLocation).ToList();
                            break;
                        case Scope.Site:
                            childLocations = GetLocationsDirectChildren(parentLocation).ToList();
                            break;
                        case Scope.WebApplication:
                            childLocations.Add(parentLocation);
                            break;
                        //case Scope.Farm:
                        //    break;
                        //case Scope.ScopeInvalid:
                        //    break;
                        default:
                            break;
                    }
                    break;
                case Scope.Farm:
                    // if parent location is farm, 
                    // simply get all locations of the wanted scope
                    childLocations = Locations.Values.Where(l => l.Scope == childrenScope)
                        .ToList();
                    break;
                //case Scope.ScopeInvalid:
                //    break;
                default:
                    break;
            }

            // now filter for featureDefinitionLocation ids in the location id or parent id of all childlocations
            if (!string.IsNullOrEmpty(featureDefinitionLocationId))
            {
                var DefinitionScopedChildLocations =
                    childLocations
                    .Where(l => l.UniqueId.Equals(featureDefinitionLocationId) ||
                                l.ParentId.Equals(featureDefinitionLocationId)
                    ).ToList();

                return DefinitionScopedChildLocations;
            }
            else
            {
                return childLocations;
            }
        }

        private IEnumerable<Location> GetLocationsChildrensChildren(Location location)
        {
            return Locations.Where(l => l.Value.ParentId == location.UniqueId)
                .Join(Locations,
                            parent => parent.Key,
                            child => child.Value.ParentId,
                            (parent, child) => child.Value)
                .ToList();
        }
    }
}
