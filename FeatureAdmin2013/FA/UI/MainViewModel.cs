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

namespace FA.UI
{
    public class MainViewModel : FA.UI.BaseClasses.ViewModelBase
    {
        #region Fields

        private int iterations = 50;
        private int progressPercentage = 0;
        private string status;
        private bool loadingBusy = false;

        //private IFeatureViewModel _selectedFeatureDefinition;

        //private ILocationViewModel _selectedLocation;

        #endregion

        #region Bindable Properties

        public IFeaturesListViewModel FeaturesListViewModel { get; private set; }
        public ILocationsListViewModel LocationsListViewModel { get; private set; }

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

        public bool LoadingBusy
        {
            get { return loadingBusy; }
            set
            {
                if (loadingBusy != value)
                {
                    loadingBusy = value;
                    OnPropertyChanged("LoadEnabled");
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
            ProgressPercentage = 0;
            LoadingBusy = true;
            
            FeaturesListViewModel.Load();
            LocationsListViewModel.Load();

            // set progress to 100% and delete status message
            ProgressPercentage = 100;
            Status = "";
            // TODO (optional) add an asynchronous wait here to show 100% and 'finished' status message for a few additional seconds

            LoadingBusy = false;
            
        }

     
    }
}
