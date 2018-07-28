using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Messages;
using System;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseItemViewModel<T> : Conductor<T>.Collection.OneActive where T : class
    {

        protected IEventAggregator eventAggregator;
        protected IFeatureRepository repository;
        public BaseItemViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
        {
            CanActivateFeatures = false;
            CanDeactivateFeatures = false;
            CanUpgradeFeatures = false;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            this.repository = repository;

            // https://github.com/Fody/PropertyChanged/issues/269
            ActivationProcessed += (s, e) => CanShowDetails = ActiveItem != null;
        }

        public bool CanShowDetails { get; protected set; }

        public void CopyToClipBoard(string textToCopy)
        {
            if (!string.IsNullOrEmpty(textToCopy))
            {
                Clipboard.SetText(textToCopy);
            }
        }

        public void ShowDetails()
        {
            if (ActiveItem != null)
            {
                var message = new OpenWindow<T>(ActiveItem);
                eventAggregator.BeginPublishOnUIThread(message);
            }

        }
        #region only relevant for Location-, Upgrade, CleanupListViewModel and ActivatedFeatureViewModel

        public FeatureDefinition SelectedFeatureDefinition { get; protected set; }
        #endregion only relevant for LocationListViewModel and ActivatedFeatureViewModel

        #region only relevant for FeatureDefinitionListViewModel and ActivatedFeatureViewModel

        public bool CanActivateFeatures { get; protected set; }
        public bool CanDeactivateFeatures { get; protected set; }

        public bool CanUpgradeFeatures { get; protected set; }

        public Location SelectedLocation { get; protected set; }

        #endregion only relevant for FeatureDefinitionListViewModel and ActivatedFeatureViewModel

    }
}
