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
using FeatureAdmin.Service;

namespace FeatureAdmin.ViewModels
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IEventAggregator _eventAggregator;

        public IWindowManager WM;

        [ImportingConstructor]
        public AppViewModel(IWindowManager wm)
        {
            WM = wm;
            LocationList = new LocationListViewModel();

        //_eventAggregator = eventAggregator;
        //    _eventAggregator.Subscribe(this);
        }

    public LocationListViewModel LocationList { get; }

    
    public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
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
