using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    public class FeatureModel : Model 
    {
        private List<ActivatedFeature> ActivatedFeatures;
        private List<FeatureDefinition> FeatureDefinitions;
        private Dictionary<Guid, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            FeatureDefinitions = new List<FeatureDefinition>();
            Locations = new Dictionary<Guid, Location>();
        }

        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> farmFeatureDefinitions)
        {
            if (farmFeatureDefinitions != null)
            {
                FeatureDefinitions.AddRange(farmFeatureDefinitions);

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
                searchResult = FeatureDefinitions;
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
                        fd => fd.Id == idGuid
                       || fd.ActivatedFeatures.Any(f => f.LocationId == idGuid));
                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult =
                       FeatureDefinitions.Where(fd => fd.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                            fd.Title.ToLower().Contains(lowerCaseSearchInput));
                }

            }

            if (selectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == selectedScopeFilter.Value);
            }

            return searchResult;
        }

        internal IEnumerable<Location> SearchLocations(string searchInput, Scope? selectedScopeFilter)
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
                       || l.Value.ActivatedFeatures.Any(f => f.FeatureId == idGuid)
                       ).Select(l => l.Value);
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

            return searchResult;
        }
    }
}
