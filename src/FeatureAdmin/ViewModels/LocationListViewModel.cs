using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using FeatureAdmin.Core.Models.Enums;
namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen, IHandle<LocationUpdated>, IHandle<SetSearchFilter<Location>>
    {
        private ObservableCollection<Location> allLocations { get; set; }
        public ObservableCollection<Location> Locations { get; private set; }

        private IEventAggregator eventAggregator;

        public LocationListViewModel(IEventAggregator eventAggregator)
        {

            allLocations = new ObservableCollection<Location>();
            Locations = allLocations;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            ScopeFilters = new ObservableCollection<Scope>(Common.Constants.Search.ScopeFilterList);
        }

        private Location selectedLocation;
        public Location SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                eventAggregator.BeginPublishOnUIThread(new ItemSelected<Location>(selectedLocation));
            }
        }

        public void Handle(LocationUpdated message)
        {
            if (message == null || message.Location == null)
            {
                //TODO log
                return;
            }

            var locationToAdd = message.Location;
            if (allLocations.Any(l => l.Id == locationToAdd.Id))
            {
                var existingLocation = allLocations.FirstOrDefault(l => l.Id == locationToAdd.Id);
                allLocations.Remove(existingLocation);
            }

            allLocations.Add(locationToAdd);
            FilterResults();
        }

        public ObservableCollection<Scope> ScopeFilters { get; private set; }

        private Scope? selectedScopeFilter;
        public Scope? SelectedScopeFilter
        {
            get { return selectedScopeFilter; }
            set
            {
                selectedScopeFilter = value;
                FilterResults();
            }
        }

        private string searchInput;
        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                FilterResults();
            }
        }

        protected void FilterResults()
        {
            IEnumerable<Location> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = allLocations;
            }
            else 
            {
                Guid idGuid;
                Guid.TryParse(searchInput, out idGuid);

                // if searchInput is not a guid, seachstring will always be a guid.empty
                // to also catch, if user intentionally wants to search for guid empty, this is checked here, too
                if (searchInput.Equals(Guid.Empty.ToString()) || idGuid != Guid.Empty)
                {
                   searchResult = allLocations.Where(l => l.Id == idGuid
                   || l.Parent == idGuid
                   || l.ActivatedFeatures.Contains(idGuid));
                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult =
                        allLocations.Where(l => l.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                        l.Url.ToLower().Contains(lowerCaseSearchInput));
                }
                   
            }

            if (SelectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == SelectedScopeFilter.Value);
            }

            Locations = new ObservableCollection<Location>(searchResult);
        }

        public void ClearSearchCommand()
        {
            SearchInput = null;
            //ScopeFilter = ScopeFilter.All;
        }

        public void Handle(SetSearchFilter<Location> message)
        {
            if (message == null)
            {
                return;
            };

            if (message.SetQuery)
            {
                SearchInput = message.SearchQuery;
            }

            if (message.SetScope)
            {
                SelectedScopeFilter = message.SearchScope;
            }

        }
    }
}
