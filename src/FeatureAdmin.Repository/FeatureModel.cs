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
        private List<ActivatedFeature> ActivatedFeatures;
        private Dictionary<string, FeatureDefinition> FeatureDefinitions;
        private Dictionary<Guid, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            FeatureDefinitions = new Dictionary<string, FeatureDefinition>();
            Locations = new Dictionary<Guid, Location>();
        }

        public void AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures)
        {
            if (activatedFeatures != null)
            {
                ActivatedFeatures.AddRange(activatedFeatures);
            }
        }

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
        public string AddLoadedLocations(LocationsLoaded message)
        {
            var error = AddLocations(message.ChildLocations);

            AddActivatedFeatures(message.ActivatedFeatures);

            AddFeatureDefinitions(message.Definitions);

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
                string additionalInfo = string.Empty;

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
                catch (Exception)
                {

                    throw;
                }

                if (doublette == null)
                {
                    return ex.Message;
                }
                else
                {
                    return string.Format(
                        "Error when trying to add a location. The location with id '{0}' and Url '{1}' was already loaded. This should never happen! Try to reload or restart. Error: '{2}'",
                        doublette.Id, doublette.Url, ex.Message);
                }
            }

            return string.Empty;

        }

        public void Clear()
        {
            ActivatedFeatures.Clear();
            FeatureDefinitions.Clear();
            Locations.Clear();
        }
        public ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId)
        {
            return ActivatedFeatures.FirstOrDefault(f => f.FeatureId == featureDefinitionId && f.LocationId == locationId);
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition)
        {
            if (featureDefinition != null)
            {
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
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location);

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
            var allLocationsOfFeatureScope = GetChildLocationsOfScope(featureDefinition.Scope, location);

            var prefilteredActivatedFeatures = ActivatedFeatures.Where(f => f.FeatureId == featureDefinition.Id).ToList();

            // see https://docs.microsoft.com/en-us/dotnet/csharp/linq/perform-left-outer-joins
            var locationsCanDeactivate =
                            (from loc in allLocationsOfFeatureScope
                             join af in prefilteredActivatedFeatures on loc.Id equals af.LocationId into gj

                             from subAf in gj.DefaultIfEmpty()
                             // following is the only different line compared to ..can activate
                             where subAf.FeatureId == featureDefinition.Id
                             select loc).ToList();


            return locationsCanDeactivate;
        }

        public IEnumerable<Location> GetLocationsDirectChildren(Location location)
        {
            return Locations.Where(l => l.Value.Parent == location.Id)
                .Select(l => l.Value)
                .ToList();
        }

        /// <summary>
        /// checks, if feature is activated in the specified location or at all
        /// </summary>
        /// <param name="featureDefinitionId">feature id of the feature to check</param>
        /// <param name="locationId">location id, where to check, if null, checks for everywhere</param>
        /// <returns>true, if feature is found</returns>
        public bool IsFeatureActivated(Guid featureDefinitionId, Guid? locationId)
        {
            if (locationId.HasValue)
            {
                return ActivatedFeatures.Any(f => f.FeatureId == featureDefinitionId && f.LocationId == locationId);
            }
            else
            {
                return ActivatedFeatures.Any(f => f.FeatureId == featureDefinitionId);
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
                        //where af.FeatureId == idGuid
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

        private IEnumerable<Location> GetChildLocationsOfScope(Scope childrenScope, Location parentLocation)
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

            return childLocations;
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
