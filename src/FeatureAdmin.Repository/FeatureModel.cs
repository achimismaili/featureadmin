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
        protected Dictionary<Guid, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            FeatureDefinitions = new Dictionary<string, FeatureDefinition>();
            Locations = new Dictionary<Guid, Location>();
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
                    Locations = Locations.Concat(locations.ToDictionary(l => l.Id)).ToDictionary(l => l.Key, l => l.Value); ;
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
                             join l in Locations on newLocs.Id equals l.Key
                             where newLocs.Id == l.Key
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
                    return string.Format(
                        "Error when trying to add a location. The location with id '{0}' and Url '{1}' was already loaded. This should never happen! Try to reload or restart. Error: '{2}'",
                        doublette.Id, doublette.Url, errInfo);
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

        public IEnumerable<ActivatedFeatureSpecial> GetAsActivatedFeatureSpecial (IEnumerable<ActivatedFeature> activatedFeatures)
        {
            if (activatedFeatures == null || activatedFeatures.Count() < 1)
            {
                return new List<ActivatedFeatureSpecial>();
            }

            var conversionResult = from af in activatedFeatures
                                   join l in Locations on af.LocationId equals l.Key
                               select new ActivatedFeatureSpecial(af, l.Value);

            return conversionResult.ToList();
        }

        /// <summary>
        /// searches for special features with search input and scope-filter
        /// </summary>
        /// <param name="source">collection of special features to search in</param>
        /// <param name="searchInput">search input</param>
        /// <param name="selectedScopeFilter">scope filter</param>
        /// <returns></returns>
        public IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(
            IEnumerable<ActivatedFeatureSpecial> source,
            string searchInput,
            Scope? selectedScopeFilter)
        {
            if (source == null || source.Count() < 1)
            {
                return new List<ActivatedFeatureSpecial>();
            }

            var searchResult = source;
                               
            if (!string.IsNullOrEmpty(searchInput))
            {
                Guid idGuid;
                Guid.TryParse(searchInput, out idGuid);

                // if searchInput is not a guid, seachstring will always be a guid.empty
                // to also catch, if user intentionally wants to search for guid empty, this is checked here, too
                if (searchInput.Equals(Guid.Empty.ToString()) || idGuid != Guid.Empty)
                {
                    searchResult = searchResult.Where(
                        f => f.ActivatedFeature.FeatureId == idGuid
                       || f.ActivatedFeature.LocationId == idGuid
                        ).Select(f => f);
                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult = searchResult.Where(
                       f => f.ActivatedFeature.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                       f.Location.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                       f.Location.Url.ToLower().Contains(lowerCaseSearchInput)
                        );
                }

            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(f => f.Location.Scope == selectedScopeFilter.Value);
            }

            return searchResult.ToArray();
        }

        public ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId)
        {
            return ActivatedFeatures.FirstOrDefault(f => f.FeatureId == featureDefinitionId && f.LocationId == locationId);
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition)
        {
            if (featureDefinition != null)
            {
                if (featureDefinition.Scope == Scope.ScopeInvalid && featureDefinition.SandBoxedSolutionLocation == null)
                {
                    return ActivatedFeatures.Where(af => af.FeatureId == featureDefinition.Id && 
                    ( af.FeatureDefinitionScope == FeatureDefinitionScope.Farm ||
                    af.FeatureDefinitionScope == FeatureDefinitionScope.None))
                    .ToList();
                }

                return ActivatedFeatures.Where(af => af.FeatureDefinitionUniqueIdentifier == featureDefinition.UniqueIdentifier)
                    .ToList();
            }

            return null;
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location)
        {
            return ActivatedFeatures.Where(af => af.LocationId == location.Id)
                .ToList();
        }

        public IEnumerable<Location> GetLocationsCanActivate(FeatureDefinition featureDefinition, Location location)
        {
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location, featureDefinition.SandBoxedSolutionLocation);

            var prefilteredActivatedFeatures = ActivatedFeatures.Where(f => f.FeatureId == featureDefinition.Id).ToList();

            // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
            var locationsCanActivate =
                            (from loc in allLocationsOfFeatureScope
                             join af in prefilteredActivatedFeatures on loc.Id equals af.LocationId into gj
                             from subAf in gj.DefaultIfEmpty()
                                 // following is the only different line compared to ..can deactivate
                             where subAf == null
                             select loc).ToList();


            return locationsCanActivate;

        }

        public IEnumerable<Location> GetLocationsCanDeactivate(FeatureDefinition featureDefinition, Location location)
        {
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location, featureDefinition.SandBoxedSolutionLocation);

            var prefilteredActivatedFeatures = ActivatedFeatures.Where(f => f.FeatureId == featureDefinition.Id).ToList();

            // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
            var locationsCanDeactivate =
                            (from loc in allLocationsOfFeatureScope
                             join af in prefilteredActivatedFeatures on loc.Id equals af.LocationId into gj

                             from subAf in gj.DefaultIfEmpty()
                                 // following is the only different line compared to ..can activate
                             where subAf != null && subAf.FeatureId == featureDefinition.Id
                             select loc).ToList();


            return locationsCanDeactivate;
        }

        public IEnumerable<Location> GetLocationsDirectChildren(Location location)
        {
            return Locations.Where(l => l.Value.Parent == location.Id)
                .Select(l => l.Value)
                .ToList();
        }

        [Command]
        public string RemoveActivatedFeature(Guid featureId, Guid locationId)
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
                if(FeatureDefinitions.Remove(uniqueIdentifier))
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

        public IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchInput, Scope? selectedScopeFilter, bool? onlyFarmFeatures)
        {
            IEnumerable<FeatureDefinition> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = FeatureDefinitions.Values;
            }
            else
            {
                Guid idGuid;
                Guid.TryParse(searchInput, out idGuid);

                // if searchInput is not a guid, seachstring will always be a guid.empty
                // to also catch, if user intentionally wants to search for guid empty, this is checked here, too
                if (searchInput.Equals(Guid.Empty.ToString()) || idGuid != Guid.Empty)
                {
                    searchResult = FeatureDefinitions.Where(
                        fd => fd.Value.Id == idGuid
                        ).Select(fd => fd.Value);

                    // search for feature Ids in activated features, if no location was found with that id
                    if (!searchResult.Any())
                    {
                        // see also https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-inner-joins
                        searchResult =
                            ActivatedFeatures
                            .Where(f => f.LocationId == idGuid)
                            .Join(
                            FeatureDefinitions,
                            f => f.FeatureDefinitionUniqueIdentifier,
                            fd => fd.Value.UniqueIdentifier,
                            (f, fd) => fd.Value);
                    }
                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult =
                       FeatureDefinitions.Where(fd => fd.Value.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                            fd.Value.Title.ToLower().Contains(lowerCaseSearchInput))
                            .Select(fd => fd.Value);
                }

            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == selectedScopeFilter.Value);
            }

            return searchResult.ToArray();
        }
        public IEnumerable<Location> SearchLocations(string searchInput, Scope? selectedScopeFilter)
        {
            IEnumerable<Location> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = Locations.Values;
            }
            else
            {
                Guid idGuid;
                Guid.TryParse(searchInput, out idGuid);

                // if searchInput is not a guid, seachstring will always be a guid.empty
                // to also catch, if user intentionally wants to search for guid empty, this is checked here, too
                if (searchInput.Equals(Guid.Empty.ToString()) || idGuid != Guid.Empty)
                {
                    searchResult = Locations.Where(
                        l => l.Key == idGuid
                       || l.Value.Parent == idGuid
                        ).Select(l => l.Value);

                    // search for feature Ids in activated features, if no location was found with that id
                    if (!searchResult.Any())
                    {
                        // see also https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-inner-joins
                        searchResult =
                            ActivatedFeatures
                            .Where(f => f.FeatureId == idGuid)
                            .Join(
                            Locations,
                            f => f.LocationId,
                            l => l.Key,
                            (f, l) => l.Value);

                        //from af in ActivatedFeatures
                        //join l in Locations 
                        //on af.LocationId equals l.Key
                        //where af != null && af.FeatureId == idGuid
                        //select l.Value;
                    }

                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult = Locations.Values.Where(
                       l => l.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                            l.Url.ToLower().Contains(lowerCaseSearchInput)
                        );
                }

            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == selectedScopeFilter.Value);
            }

            return searchResult.ToArray();
        }

        /// <summary>
        /// Gets all locations that are available for feature de- and activation
        /// </summary>
        /// <param name="childrenScope">scope of locations (webs, sicos, web apps, farm)</param>
        /// <param name="parentLocation">parent location, all must be below</param>
        /// <param name="featureDefinitionLocation">if feature definition is sandbox or add in, definition is not farm wide, but in site or web</param>
        /// <returns></returns>
        private IEnumerable<Location> GetChildLocationsOfScope(Scope childrenScope, Location parentLocation, Guid? featureDefinitionLocation)
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
            if (featureDefinitionLocation.HasValue)
            {
                var DefinitionScopedChildLocations =
                    childLocations
                    .Where(l => l.Id == featureDefinitionLocation.Value ||
                                l.Parent == featureDefinitionLocation.Value)
                    .ToList();

                return DefinitionScopedChildLocations;
            }
            else
            {
                return childLocations;
            }
        }

        private IEnumerable<Location> GetLocationsChildrensChildren(Location location)
        {
            return Locations.Where(l => l.Value.Parent == location.Id)
                .Join(Locations,
                            parent => parent.Key,
                            child => child.Value.Parent,
                            (parent, child) => child.Value)
                .ToList();
        }
    }
}
