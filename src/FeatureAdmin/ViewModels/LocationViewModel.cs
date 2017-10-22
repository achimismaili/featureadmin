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
    public class LocationViewModel : BaseDetailViewModel<Location>
    {
        public LocationViewModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
            
        }

        public void CopyTitle()
        {
            copyToClipBoard(Item.DisplayName);
        }

        public void CopyUrl()
        {
            copyToClipBoard(Item.Url);
        }

        public void CopyId()
        {
            copyToClipBoard(Item.Id.ToString());
        }
    }
}
