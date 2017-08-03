using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FA.SharePoint;
using FA.Models;
using Serilog;
using Prism.Events;
using FA.UI.Events;
using FA.Models.Interfaces;

namespace FA.UI.Locations
{
    public class LocationsListViewModel : FA.UI.BaseClasses.ViewModelBase, ILocationsListViewModel
    {
        private BackgroundWorker _backgroundWorker;
        private IEventAggregator _eventAggregator;
        private ILocationsRepository _locationsRepository;

        public LocationsListViewModel(
            ILocationsRepository locationsRepository,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _locationsRepository = locationsRepository;
            _backgroundWorker = new BackgroundWorker();
            // Background Process
            _backgroundWorker.DoWork += backgroundWorker_DoWorkGetLocations;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            // Progress
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
        }

        public ObservableCollection<ILocationViewModel> Locations { get; private set; }


        public void Load()
        {
            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        #region BackgroundWorker Events

        // Runs on Background Thread
        private void backgroundWorker_DoWorkGetLocations(object sender, DoWorkEventArgs e)
        {
            var result = new ObservableCollection<ILocationViewModel>();

            var waCount = 0;
            var waCaCount = 0;
            var siteCount = 0;
            var webCount = 0;

            var currentPercentage = 10;

            string reportProgress;
            BackgroundWorker worker = sender as BackgroundWorker;
            

            // Farm 5%
            if (worker != null && worker.WorkerReportsProgress)
            {
                reportProgress = string.Format("Loading Farm ...");
                worker.ReportProgress(5, reportProgress);
            }
            var farm = _locationsRepository.Farm;


            // Getting CA
            if (worker != null && worker.WorkerReportsProgress)
            {
                reportProgress = string.Format("Loading Central Administration ...");
                worker.ReportProgress(6, reportProgress);
            }
            var webAppCa = _locationsRepository.GetWebApplicationsAdmin;

            if (webAppCa != null & webAppCa.Count > 0)
            {
                waCaCount = webAppCa.Count;
                waCount += waCaCount;
            }

            // Getting Web Apps
            if (worker != null && worker.WorkerReportsProgress)
            {
                reportProgress = string.Format("Loading Content Web Applications ...");
                worker.ReportProgress(7, reportProgress);
            }
            var webApps = _locationsRepository.GetWebApplicationsContent;

            if (webApps != null & webApps.Count > 0)
            {
                waCount += webApps.Count;
            }

            farm.ChildCount = waCount;
            // here, after getting farm and web apps, we are at 10 %
            Log.Information(string.Format("Found {0} Content Web Application(s) and {1} Central Administration in farm",
                waCount - waCaCount, waCaCount));
            double deltaWebAppPercentage = ((float)1 / (float)waCount);

            // Getting Sites and Webs of Central Admin
            if (webAppCa != null & webAppCa.Count > 0)
            {
                foreach (FeatureParent wa in webAppCa)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        reportProgress = string.Format("Loading Sites from Central Administration '{0}'", wa.DisplayName);
                        worker.ReportProgress(currentPercentage, reportProgress);
                    }
                    farm.ChildLocations.Add(wa);
                    if (wa.ChildCount > 0)
                    {
                        // Get SiteCollections and webs

                        // float deltaSitePercentage = ((float)1 / (float)fp.ChildCount);

                        // tbd
                        Log.Information(string.Format("Loaded {0} Site Collections from Central Administration '{1}'",
                            wa.ChildCount, wa.DisplayName));
                    }
                    result.Add(new LocationViewModel(wa));
                    currentPercentage += (int)deltaWebAppPercentage;
                }
            }

            // Getting Sites and Webs of Content Web Apps
            if (webApps != null & webApps.Count > 0)
            {
                foreach (FeatureParent wa in webApps)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        reportProgress = string.Format("Loading Sites from Web Application '{0}'", wa.DisplayName);
                        worker.ReportProgress(currentPercentage, reportProgress);
                    }
                    farm.ChildLocations.Add(wa);
                    if (wa.ChildCount > 0)
                    {
                        // Get SiteCollections and webs

                        // float deltaSitePercentage = ((float)1 / (float)fp.ChildCount);

                        // tbd
                        Log.Information(string.Format("Loaded {0} Site Collections from Web Application '{1}'",
                           wa.ChildCount, wa.DisplayName));
                    }

                    result.Add(new LocationViewModel(wa));
                    currentPercentage += (int)deltaWebAppPercentage;
                }
            }

            result.Add(new LocationViewModel(farm));
           
            e.Result = result;
        }

        // Runs on UI Thread
        private void backgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Log.Error(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Log.Warning("Loading locations cancelled");
            }
            else
            {
                Locations = e.Result as ObservableCollection<ILocationViewModel>;
            }
        }

        // Runs on UI Thread
        private void backgroundWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            _eventAggregator.GetEvent<SetProgressBarEvent>()
                .Publish(e.ProgressPercentage);

            _eventAggregator.GetEvent<SetStatusBarEvent>()
                .Publish(e.UserState as string);
        }
        #endregion BackgroundWorker Events
    }
}
