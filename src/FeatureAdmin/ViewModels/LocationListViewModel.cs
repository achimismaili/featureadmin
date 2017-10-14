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
    public class LocationListViewModel : Screen, IHandle<LocationUpdated>
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
                eventAggregator.BeginPublishOnUIThread(new LocationSelected(selectedLocation));
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

        private string idFilter;
        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                idFilter = value;
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

            Guid idGuid;
            Guid.TryParse(idFilter, out idGuid);
            if (string.IsNullOrEmpty(idFilter))
            {
                searchResult = allLocations;
            }
            else
            {
                searchResult =
                    allLocations.Where(l => l.Id == idGuid);
            }

            if (SelectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == SelectedScopeFilter.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                searchResult = 
                    searchResult.Where(l => l.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                    l.Url.ToLower().Contains(lowerCaseSearchInput));
            }
            Locations = new ObservableCollection<Location>(searchResult);
        }

        public void ClearSearchCommand()
        {
            SearchInput = null;
            //ScopeFilter = ScopeFilter.All;
        }
    }
}
