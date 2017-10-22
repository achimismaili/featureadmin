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
    public class LocationViewModel : BaseDetailViewModel, IHandle<ItemSelected<Location>>
    {
        public Location Location { get; set; }

        

        public LocationViewModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
            
        }

        public void Handle(ItemSelected<Location> message)
        {
            Location = message.Item;
            ItemSelected = message.Item != null;
        }

        public void CopyTitle()
        {
            copyToClipBoard(Location.DisplayName);
        }

        public void CopyUrl()
        {
            copyToClipBoard(Location.Url);
        }

        public void CopyId()
        {
            copyToClipBoard(Location.Id.ToString());
        }
    }
}
