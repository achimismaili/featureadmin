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
using Serilog;
using Prism.Events;
using FA.UI.Events;

namespace FA.UI.Features
{
    public class FeaturesListViewModel : FA.UI.BaseClasses.ViewModelBase, IFeaturesListViewModel
    {
        private BackgroundWorker _backgroundWorker;
        private IEventAggregator _eventAggregator;
        private IFeaturesRepository _featuresRepository;

        public FeaturesListViewModel(
            IFeaturesRepository featuresRepository,
            IEventAggregator eventAggregator)
        {
            Features = new ObservableCollection<IFeatureViewModel>();
            _eventAggregator = eventAggregator;
            _featuresRepository = featuresRepository;
            // Background Process
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWorkGetFeatureDefinitions;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompletedFeatures;
        }

        public void Load()
        {
            _eventAggregator.GetEvent<SetStatusBarEvent>()
                    .Publish("Loading Feature Definitions ...");
            
            if (!_backgroundWorker.IsBusy)
            {
                Features.Clear();
                _backgroundWorker.RunWorkerAsync();
            }

            return;
        }

        public ObservableCollection<IFeatureViewModel> Features { get; private set; }

        #region BackgroundWorker Events

        // Runs on Background Thread
        private void backgroundWorker_DoWorkGetFeatureDefinitions(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = _featuresRepository.FeatureDefiniitions;
        }

        private void PopulateObservableCollection(List<IFeatureDefinition> spDefs)
        {
            if (spDefs == null)
            {
                return;
            }

            foreach (IFeatureDefinition fd in spDefs)
            {
                var featureViewModel = new FeatureViewModel(fd);
                this.Features.Add(featureViewModel);
            }

            return;
        }

        // Runs on UI Thread
        private void backgroundWorker_RunWorkerCompletedFeatures(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
              Log.Error( e.Error.Message);
            }
            else if (e.Cancelled)
            {
              Log.Warning( "Loading feature definitions cancelled");
            }
            else
            {

                var rawFeatures = e.Result as List<IFeatureDefinition>;

                PopulateObservableCollection(rawFeatures);

                // hard code 5% as progress, when feature definitions of a farm are loaded
                _eventAggregator.GetEvent<SetProgressBarEvent>()
               .Publish(5);

                var msg = string.Format("Loaded {0} Feature Definitions", Features.Count);

                _eventAggregator.GetEvent<SetStatusBarEvent>()
                .Publish(msg);

                Log.Information(msg);
                
            }
        }

        #endregion BackgroundWorker Events
    }
}
