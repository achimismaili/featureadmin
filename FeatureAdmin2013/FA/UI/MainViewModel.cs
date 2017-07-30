using FA.SharePoint;
using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private BackgroundWorker backgroundWorker;
        private int iterations = 50;
        private int progressPercentage = 0;
        private string output;
        private bool loadEnabled = true;

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
                    RaisePropertyChanged("Iterations");
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
                    RaisePropertyChanged("ProgressPercentage");
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
                    RaisePropertyChanged("Output");
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
                    RaisePropertyChanged("LoadEnabled");
                }
            }
        }

        #endregion Bindable Properties

        public MainViewModel()
        {
            backgroundWorker = new BackgroundWorker();
            // Background Process
            backgroundWorker.DoWork += backgroundWorker_DoWorkGetFeatureDefinitions;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            // Progress
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

        }

        internal void Reload()
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }

            LoadEnabled = !backgroundWorker.IsBusy;
            Output = string.Empty;
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
