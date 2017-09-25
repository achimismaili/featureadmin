using Caliburn.Micro;
using System.ComponentModel.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Repositories.Contracts;
using System.Collections.ObjectModel;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.ViewModels
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IEventAggregator _eventAggregator;

        private ObservableCollection<ActivatedFeature> activatedFeatures;
        private ObservableCollection<FeatureDefinition> featureDefinitions;
        private ObservableCollection<Location> locations;

        public ObservableCollection<ActivatedFeature> ActivatedFeatures
        {
            get { return activatedFeatures; }
            set
            {
                activatedFeatures = value;
                NotifyOfPropertyChange(() => ActivatedFeatures);

            }
        }
        public ObservableCollection<FeatureDefinition> FeatureDefinitions
        {
            get { return featureDefinitions; }
            set
            {
                featureDefinitions = value;
                NotifyOfPropertyChange(() => FeatureDefinitions);

            }
        }
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                NotifyOfPropertyChange(() => Locations);

            }
        }

        [ImportingConstructor]
        public AppViewModel()
        {
            
            //_eventAggregator = eventAggregator;
            //    _eventAggregator.Subscribe(this);
            FeatureDefinitions = new ObservableCollection<FeatureDefinition>(SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions());
            Locations = new ObservableCollection<Location>(SampleData.SampleLocationHierarchy.GetAllLocations());
            ActivatedFeatures = new ObservableCollection<ActivatedFeature>(SampleData.SampleActivatedFeatures.GetActivatedFeatures(Locations));

        }

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
