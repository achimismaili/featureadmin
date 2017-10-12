using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

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
        }

        private string _SearchInput;
        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                _SearchInput = value;
                FilterResults(_SearchInput);
            }
        }

        protected void FilterResults(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Locations = new ObservableCollection<Location>(allLocations);
                return;
            }
            else
            {
                var lowerCaseSearchInput = searchInput.ToLower();
                Locations = new ObservableCollection<Location>(
                    allLocations.Where(l => l.DisplayName.ToLower().Contains(lowerCaseSearchInput) ||
                    l.Url.ToLower().Contains(lowerCaseSearchInput) ));
            }
        }

        public void ClearSearchCommand()
        {
            SearchInput = null;
        }
    }
}
