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
            Locations = new ObservableCollection<ILocationViewModel>();
            _eventAggregator = eventAggregator;
            _locationsRepository = locationsRepository;
            _backgroundWorker = new BackgroundWorker();
            // Background Process
            _backgroundWorker.DoWork += backgroundWorker_DoWorkGetLocations;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompletedLocations;

            // Progress
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChangedLocations;
        }

        public ObservableCollection<ILocationViewModel> Locations { get; private set; }


        public void Load()
        {
            if (!_backgroundWorker.IsBusy)
            {
                Locations.Clear();
                _backgroundWorker.RunWorkerAsync();
            }
            return;
        }

        #region BackgroundWorker Events

        // Runs on Background Thread
        private void backgroundWorker_DoWorkGetLocations(object sender, DoWorkEventArgs e)
        {
            var result = new List<LocationViewModel>();

            var waCount = 0;
            var waCaCount = 0;
            var siteCount = 0;
            var webCount = 0;

            var currentPercentage = 10;

            string reportProgress;
            BackgroundWorker worker = sender as BackgroundWorker;


            // Farm 
            var farm = _locationsRepository.Farm;
            if (worker != null && worker.WorkerReportsProgress)
            {
                reportProgress = string.Format(
                    "Loaded Farm '{0}'",
                    farm.DisplayName);
                worker.ReportProgress(6, reportProgress);
            }

            // Getting CA
            var webAppCa = _locationsRepository.GetWebApplicationsAdmin;

            if (webAppCa != null & webAppCa.Count > 0)
            {
                waCaCount = webAppCa.Count;
                waCount += waCaCount;
            }

            // Getting Web Apps
            var webApps = _locationsRepository.GetWebApplicationsContent;

            if (webApps != null & webApps.Count > 0)
            {
                waCount += webApps.Count;
            }

            farm.ChildCount = waCount;
            // here, after getting farm and web apps, we are at 10 %
            if (worker != null && worker.WorkerReportsProgress)
            {
                reportProgress = string.Format(
                    "Loaded {0} Content Web Application(s) \nand {1} Central Administration in farm",
                    waCount - waCaCount, waCaCount);
                worker.ReportProgress(10, reportProgress);
            }


            // Log.Information(
            double deltaWebAppPercentage = ((float)1 / (float)waCount) * 90;

            // Getting Sites and Webs of Central Admin
            if (webAppCa != null & webAppCa.Count > 0)
            {
                foreach (FeatureParent wa in webAppCa)
                {
                    currentPercentage += (int)deltaWebAppPercentage;
                    farm.ChildLocations.Add(wa);
                    if (wa.ChildCount > 0)
                    {
                        // Get SiteCollections and webs

                        // float deltaSitePercentage = ((float)1 / (float)fp.ChildCount);

                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            reportProgress = string.Format(
                                "Loaded {1} Sites in Central Administration '{0}'",
                                wa.DisplayName,
                               wa.ChildCount);
                            worker.ReportProgress(currentPercentage, reportProgress);
                        }
                    }
                    result.Add(new LocationViewModel(wa));
                }
            }

            // Getting Sites and Webs of Content Web Apps
            if (webApps != null & webApps.Count > 0)
            {
                foreach (FeatureParent wa in webApps)
                {
                    currentPercentage += (int)deltaWebAppPercentage;
                    farm.ChildLocations.Add(wa);
                    if (wa.ChildCount > 0)
                    {
                        // Get SiteCollections and webs
                        System.Threading.Thread.Sleep(1000);
                        // float deltaSitePercentage = ((float)1 / (float)fp.ChildCount);

                        // tbd

                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            reportProgress = string.Format(
                                "Loaded {1} Sites in Web Application '{0}'",
                                wa.DisplayName,
                                wa.ChildCount);
                            worker.ReportProgress(currentPercentage, reportProgress);
                        }
                    }
                    result.Add(new LocationViewModel(wa));
                }
            }

            result.Add(new LocationViewModel(farm));

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(100, string.Empty);
            }

            e.Result = result;
        }

        // Runs on UI Thread
        private void backgroundWorker_RunWorkerCompletedLocations(object sender,
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
                var loadedLocations = e.Result as List<LocationViewModel>;

                if (loadedLocations != null && loadedLocations.Any())
                {
                    foreach (LocationViewModel l in loadedLocations)
                    {
                        Locations.Add(l);
                    }
                }
            }
        }

        // Runs on UI Thread
        private void backgroundWorker_ProgressChangedLocations(object sender,
            ProgressChangedEventArgs e)
        {
            _eventAggregator.GetEvent<SetProgressBarEvent>()
                .Publish(e.ProgressPercentage);

            var msg = e.UserState as string;

            _eventAggregator.GetEvent<SetStatusBarEvent>()
                .Publish(msg);
            if (!string.IsNullOrEmpty(msg))
            {
                Log.Information(msg);
            }
        }
        #endregion BackgroundWorker Events
    }
}
