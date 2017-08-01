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


namespace FA.UI
{
    public class MainViewModel : FA.UI.BaseClasses.ViewModelBase
    {
        #region Fields
        private IFeaturesListViewModel _featuresListViewModel;
        private ILocationsListViewModel _locationsListViewModel;


        private BackgroundWorker backgroundWorker;
        private int iterations = 50;
        private int progressPercentage = 0;
        private string output;
        private bool loadEnabled = true;

        //private IFeatureViewModel _selectedFeatureDefinition;

        //private ILocationViewModel _selectedLocation;



        #endregion

        #region Bindable Properties

        public ObservableCollection<FeatureDefinition> FeatureDefinitions;
        public ObservableCollection<FeatureParent> Parents;
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

        public string Output
        {
            get { return output; }
            set
            {
                if (output != value)
                {
                    output = value;
                    OnPropertyChanged("Output");
                }
            }
        }

        public bool LoadEnabled
        {
            get { return loadEnabled; }
            set
            {
                if (loadEnabled != value)
                {
                    loadEnabled = value;
                    OnPropertyChanged("LoadEnabled");
                }
            }
        }

        #endregion Bindable Properties

        public MainViewModel(
            IFeaturesListViewModel featuresListViewModel,
            ILocationsListViewModel locationsListViewModel
            )
        {
            _featuresListViewModel = featuresListViewModel;
            _locationsListViewModel = locationsListViewModel;

            backgroundWorker = new BackgroundWorker();
            // Background Process
            backgroundWorker.DoWork += backgroundWorker_DoWorkGetFeatureDefinitions;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            // Progress
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

        }
        
        public void Load()
        {

            loadEnabled = false;

            _featuresListViewModel.Load();

            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }

            LoadEnabled = true;
        }

        #region BackgroundWorker Events

        // Runs on Background Thread
        private void backgroundWorker_DoWorkGetFeatureDefinitions(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // int result = 0;

            //foreach (var current in processor)
            //{
            //    if (worker != null)
            //    {

            //        if (worker.WorkerReportsProgress)
            //        {
            //            int percentageComplete =
            //                (int)((float)current / (float)iterations * 100);
            //            string progressMessage =
            //                string.Format("Iteration {0} of {1}", current, iterations);
            //            worker.ReportProgress(percentageComplete, progressMessage);
            //        }
            //    }
            //    result = current;
            //}

            var spDefs = FarmRead.GetFeatureDefinitionCollection();

            var result = new ObservableCollection<FeatureDefinition>(FeatureDefinition.GetFeatureDefinition(spDefs));

            e.Result = result;
        }

        // Runs on UI Thread
        private void backgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Output = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                Output = "Cancelled";
            }
            else
            {
                FeatureDefinitions = e.Result as ObservableCollection<FeatureDefinition>;
                Output = FeatureDefinitions[0].Name;
                ProgressPercentage = 0;
            }
            LoadEnabled = !backgroundWorker.IsBusy;

        }

        // Runs on UI Thread
        private void backgroundWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
            Output = (string)e.UserState;
        }

        #endregion
    }
}
