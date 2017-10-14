using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public class LocationViewModel : Screen, IHandle<LocationSelected>
    {
        private IEventAggregator eventAggregator;
        public Location Location { get; set; }
        public LocationViewModel(IEventAggregator eventAggregator) // , Location location)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public string Name { get
            {
                return Location == null ? "Please select a location" : Location.DisplayName;
            }
        }

        public void Handle(LocationSelected message)
        {
            Location = message.Location;
            // Selected = Selected == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
