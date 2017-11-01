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
                SetProperty(ref iterations, value);
            }
        }

        public int ProgressPercentage
        {
            get { return progressPercentage; }
            set
            {
                if (progressPercentage != value)
                {
                    SetProperty(ref progressPercentage, value);
                    // check, if other properties also have to be checked ...
                    ReloadButtonEnabled = (value >= 100);
                    ProgressBarVisibility = (value >= 100) ? Visibility.Hidden : Visibility.Visible;
                }
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                SetProperty(ref status, value);
            }
        }

        public bool ReloadButtonEnabled
        {
            get { return reloadButtonEnabled; }
            set
            {
                SetProperty(ref reloadButtonEnabled, value);
            }
        }

        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                SetProperty(ref progressBarVisibility, value);
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
