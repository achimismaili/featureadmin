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
        private Dictionary<KeyValuePair<Guid, Guid>, ActivatedFeature> ActivatedFeatures;
        private Dictionary<string, FeatureDefinition> FeatureDefinitions;
        private Dictionary<Guid, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new Dictionary<KeyValuePair<Guid, Guid>, ActivatedFeature>();
            FeatureDefinitions = new Dictionary<string, FeatureDefinition>();
            Locations = new Dictionary<Guid, Location>();
        }

        public string AddActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures)
        {
            // required only for debugging with foreach code in try section
            // KeyValuePair<Guid, Guid>? key = null;

            try
            {
                if (activatedFeatures != null)
                {

                    // this is for debugging only, it is not performing well with bigger sharepoint farms ...

                    //foreach (var item in activatedFeatures)
                    //{
                    //    key = new KeyValuePair<Guid, Guid>(item.FeatureId, item.LocationId);

                    //    if (ActivatedFeatures.ContainsKey(key.Value))
                    //    {
                    //        throw new Exception( "Existing key: feature id: " + key.Value.Key.ToString() + " location id:" + key.Value.Value.ToString() + "!");
                    //    }
                    //    else
                    //    {
                    //        ActivatedFeatures.Add(key.Value, item);
                    //    }
                    //}

                    ActivatedFeatures = ActivatedFeatures.Concat(activatedFeatures.ToDictionary(
                        f => new KeyValuePair<Guid, Guid>(f.FeatureId, f.LocationId)))
                        .ToDictionary(f => f.Key, f => f.Value); ;
                }
            }
            catch (Exception ex)
            {
                string additionalInfo = string.Empty;

                ActivatedFeature doublette = null;

                // try to find activated feature that was tried to be added twice
                try
                {
                    doublette =
                            (from newFeatures in activatedFeatures
                             join f in ActivatedFeatures.Values on new { newFeatures.FeatureId, newFeatures.LocationId } equals new { f.FeatureId, f.LocationId }
                             where newFeatures.FeatureId == f.FeatureId && newFeatures.LocationId == f.LocationId
                             select f).FirstOrDefault();

                    if (doublette == null)
                    {
                        doublette =
                            activatedFeatures.GroupBy(f => new { f.FeatureId, f.LocationId })
                            .SelectMany(grp => grp.Skip(1)).FirstOrDefault();
                    }

                }
                catch (Exception)
                {

                    throw;
                }

                if (doublette == null)
                    // required only for debugging with foreach code in try section
                   // if (key == null)
                {
                    return ex.Message;
                }
                else
                {
                    return string.Format(
                        "Error when trying to add an activated feature. The following combination of location id '{0}' and feature id '{1}' was already loaded. This should never happen! Try to reload or restart. Error: '{2}'",
                         doublette.LocationId, doublette.FeatureId, ex.Message);

                    // required only for debugging with foreach code in try section
                    // key.Value.Value, key.Value.Key, ex.Message);
                }
            }

            return string.Empty;
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
        public void Clear()
        {
            ActivatedFeatures.Clear();
            FeatureDefinitions.Clear();
            Locations.Clear();
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
                            from af in ActivatedFeatures.Values
                            where af.LocationId == idGuid
                            join fd in FeatureDefinitions on af.FeatureDefinitionUniqueIdentifier equals fd.Key
                            select fd.Value;
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

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(FeatureDefinition featureDefinition)
        {
            if (featureDefinition != null)
            {
                return ActivatedFeatures.Where(af => af.Value.FeatureDefinitionUniqueIdentifier == featureDefinition.UniqueIdentifier)
                    .Select(af => af.Value)
                    .ToList();
            }

            return null;
        }

        public IEnumerable<ActivatedFeature> GetActivatedFeatures(Location location)
        {
            if (location != null)
            {
                return ActivatedFeatures.Where(af => af.Value.LocationId == location.Id)
                    .Select(af => af.Value)
                    .ToList(); ;
            }

            return null;
        }


        public ActivatedFeature GetActivatedFeature(Guid featureDefinitionId, Guid locationId)
        {
            var key = new KeyValuePair<Guid, Guid>(featureDefinitionId, locationId);

            if (ActivatedFeatures.ContainsKey(key))
            {
                return ActivatedFeatures[key];
            }
            else
            {
                return null;
            }

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
                // the key of the key is the featureId
                return ActivatedFeatures.Keys.Any(f => f.Key == featureDefinitionId);
            }
            else
            {
                var key = new KeyValuePair<Guid, Guid>(featureDefinitionId, locationId.Value);

                return ActivatedFeatures.ContainsKey(key);
            }
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
                            ActivatedFeatures.Keys
                            .Where(f => f.Key == idGuid)
                            .Join(
                            Locations,
                            f => f.Value,
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
    }
}
