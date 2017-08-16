using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models;
using FA.SharePoint;
using FA.UI.Features;
using FA.UI.Locations;
using Prism.Events;
using FA.UI.Events;
using System.Windows.Input;
using Prism.Commands;
using System.Windows;

namespace FA.UI
{
    public class MainViewModel : FA.UI.BaseClasses.ViewModelBase
    {
        #region Fields

        private int iterations = 50;
        private int progressPercentage = 0;
        private string status;
        private bool reloadButtonEnabled = false;
        private Visibility progressBarVisibility;

        //private IFeatureViewModel _selectedFeatureDefinition;

        //private ILocationViewModel _selectedLocation;

        #endregion

        #region Bindable Properties

        public IFeaturesListViewModel FeaturesListViewModel { get; private set; }
        public ILocationsListViewModel LocationsListViewModel { get; private set; }

        public ICommand ReloadCommand { get; private set; }
        public int Iterations
        {
            get { return iterations; }
            set
            {
                if (iterations != value)
                {
                    iterations = value;
                    OnPropertyChanged("Iterations");
                }
            }
        }

        public int ProgressPercentage
        {
            get { return progressPercentage; }
            set
            {
                if (progressPercentage != value)
                {
                    // set loading busy to true, if percentage is not 100%
                    ReloadButtonEnabled = (value >=100);
                    ProgressBarVisibility = (value >= 100) ? Visibility .Hidden : Visibility.Visible;
                    progressPercentage = value;
                    OnPropertyChanged("ProgressPercentage");
                }
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public bool ReloadButtonEnabled
        {
            get { return reloadButtonEnabled; }
            set
            {
                if (reloadButtonEnabled != value)
                {
                    reloadButtonEnabled = value;
                    OnPropertyChanged("ReloadButtonEnabled");
                }
            }
        }

        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                if (progressBarVisibility != value)
                {
                    progressBarVisibility = value;
                    OnPropertyChanged("ProgressBarVisibility");
                }
            }
        }

        #endregion Bindable Properties

        public MainViewModel(
            IFeaturesListViewModel featuresListViewModel,
            ILocationsListViewModel locationsListViewModel,
            IEventAggregator eventAggregator
            )
        {
            FeaturesListViewModel = featuresListViewModel;
            LocationsListViewModel = locationsListViewModel;

            eventAggregator.GetEvent<SetProgressBarEvent>().Subscribe(OnSetProgressBar);
            eventAggregator.GetEvent<SetStatusBarEvent>().Subscribe(OnSetStatusBar);

            ReloadCommand = new DelegateCommand(Load);
        }

        private void OnSetStatusBar(string status)
        {
            Status = status;
        }

        private void OnSetProgressBar(int percentage)
        {
            ProgressPercentage = percentage;
        }

        public void Load()
        {
            ReloadButtonEnabled = false;
            ProgressPercentage = 0;
            ProgressBarVisibility = Visibility.Visible;

            FeaturesListViewModel.Load();

            LocationsListViewModel.Load();
        }
    }
}
