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
            _eventAggregator = eventAggregator;
            _featuresRepository = featuresRepository;
            // Background Process
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWorkGetFeatureDefinitions;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        public void Load()
        {
            _eventAggregator.GetEvent<SetStatusBarEvent>()
                    .Publish("Loading Feature Definitions ...");

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        public ObservableCollection<IFeatureViewModel> Features { get; private set; }

        #region BackgroundWorker Events

        // Runs on Background Thread
        private void backgroundWorker_DoWorkGetFeatureDefinitions(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            var spDefs = _featuresRepository.FeatureDefiniitions;

            var result = ConvertToViewModelObservableCollection(spDefs);

            e.Result = result;
        }

        private ObservableCollection<IFeatureViewModel> ConvertToViewModelObservableCollection(List<IFeatureDefinition> spDefs)
        {
            var vmList = new ObservableCollection<IFeatureViewModel>();

            if (spDefs == null)
            {
                return vmList;
            }

            foreach (IFeatureDefinition fd in spDefs)
            {
                var featureViewModel = new FeatureViewModel(fd);
                vmList.Add(featureViewModel);
            }


            return vmList;
        }

        // Runs on UI Thread
        private void backgroundWorker_RunWorkerCompleted(object sender,
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
                // hard code 5% as progress, when feature definitions of a farm are loaded
                _eventAggregator.GetEvent<SetProgressBarEvent>()
               .Publish(5);

                Features = e.Result as ObservableCollection<IFeatureViewModel>;
                Log.Information("{0} feature definitions loaded from farm.", Features.Count);
                
            }
        }

        #endregion BackgroundWorker Events
    }
}
