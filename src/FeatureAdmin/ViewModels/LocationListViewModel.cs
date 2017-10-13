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

            ScopeFilterAll = true;
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
        private bool scopeFilterAll;

        private bool scopeFilterFarm;
        private bool scopeFilterWebApp;
        private bool scopeFilterSite;
        private bool scopeFilterWeb;
        public bool ScopeFilterAll { get {
                return scopeFilterAll;
            } set
            {
                scopeFilterAll = value;
                if (scopeFilterAll)
                {
                    scopeFilter = null;
                    FilterResults();
                }
            } }
        public bool ScopeFilterFarm 
        {
            get
            {
                return scopeFilterFarm;
            }
            set
            {
                scopeFilterFarm = value;
                if (scopeFilterFarm)
                {
                    scopeFilter = Scope.Farm;
                    FilterResults();
                }
            }
        }
        public bool ScopeFilterWebApp
        {
            get
            {
                return scopeFilterWebApp;
            }
            set
            {
                scopeFilterWebApp = value;
                if (scopeFilterWebApp)
                {
                    scopeFilter = Scope.WebApplication;
                    FilterResults();
                }
            }
        }
        public bool ScopeFilterSite
        {
            get
            {
                return scopeFilterSite;
            }
            set
            {
                scopeFilterSite = value;
                if (scopeFilterSite)
                {
                    scopeFilter = Scope.Site;
                    FilterResults();
                }
            }
        }
        public bool ScopeFilterWeb
        {
            get
            {
                return scopeFilterWeb;
            }
            set
            {
                scopeFilterWeb = value;
                if (scopeFilterWeb)
                {
                    scopeFilter = Scope.Web;
                    FilterResults();
                }
            }
        }

        private Scope? scopeFilter;

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
            if (scopeFilter == null)
            {
                searchResult = allLocations; ;
            }
            else
            {
                searchResult = 
                    allLocations.Where(l => l.Scope == scopeFilter.Value);
            }

            if (string.IsNullOrWhiteSpace(searchInput) )
            {
                Locations = new ObservableCollection<Location>(searchResult);
            }
            else
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                Locations = new ObservableCollection<Location>(
                    searchResult.Where(l => l.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                    l.Url.ToLower().Contains(lowerCaseSearchInput)));
            }
            return;
        }

        public void ClearSearchCommand()
        {
            SearchInput = null;
            ScopeFilterAll = true;
        }
    }
}
