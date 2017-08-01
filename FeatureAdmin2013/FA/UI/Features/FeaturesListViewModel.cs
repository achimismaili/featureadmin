using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models;
using FA.SharePoint;
using FA.Models.Interfaces;

namespace FA.UI.Features
{
    public class FeaturesListViewModel : FA.UI.BaseClasses.ViewModelBase, IFeaturesListViewModel
    {
        private BackgroundWorker backgroundWorker;

        public FeaturesListViewModel()
        {
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
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
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
               // Output = e.Error.Message;
            }
            else if (e.Cancelled)
            {
              //  Output = "Cancelled";
            }
            else
            {
                Features = e.Result as ObservableCollection<IFeatureViewModel>;

                // ProgressPercentage = 0;
            }
        }

        // Runs on UI Thread
        private void backgroundWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            // ProgressPercentage = e.ProgressPercentage;
            // Output = (string)e.UserState;
        }

        #endregion

        public ObservableCollection<IFeatureViewModel> Features { get; private set; }
    }
}
