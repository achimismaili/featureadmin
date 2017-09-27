using Caliburn.Micro;
using System.ComponentModel.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.DataServices.Contracts;
using System.Collections.ObjectModel;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Services;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IEventAggregator eventAggregator;

        private IServiceWrapper dataService;

        public AppViewModel(IEventAggregator eventAggregator, IServiceWrapper dataService)
        {
            LocationList = new LocationListViewModel(dataService);

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            Maus = "piep1";
        }

        public LocationListViewModel LocationList { get; }


        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string maus;

        public string Maus
        {
            get { return maus; }
            set
            {
                maus = value;
                NotifyOfPropertyChange(() => Maus);
            }
        }


        public void OpenSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        private bool _isSettingsFlyoutOpen;

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }
    }
}
