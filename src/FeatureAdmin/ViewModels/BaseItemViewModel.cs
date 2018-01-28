using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseItemViewModel<T> : Conductor<T>.Collection.OneActive where T : class, IDisplayableItem
    {

        protected IEventAggregator eventAggregator;
        public BaseItemViewModel(IEventAggregator eventAggregator)
        {
            CanActivateFeatures = false;
            CanDeactivateFeatures = false;
            CanUpgradeFeatures = false;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

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
                var vm = new DetailViewModel(
                    string.Format("{0}: {1}", ActiveItem.GetType().Name, ActiveItem.DisplayName),
                    ActiveItem.GetAsPropertyList() 
                    );
                var message = new OpenWindow(vm);
                eventAggregator.BeginPublishOnUIThread(message);
            }

        }
        #region only relevant for LocationListViewModel and ActivatedFeatureViewModel

        public FeatureDefinition SelectedFeatureDefinition { get; protected set; }
        #endregion only relevant for LocationListViewModel and ActivatedFeatureViewModel

        #region only relevant for FeatureDefinitionListViewModel and ActivatedFeatureViewModel

        public bool CanActivateFeatures { get; protected set; }
        public bool CanDeactivateFeatures { get; protected set; }

        public bool CanUpgradeFeatures { get; protected set; }

        public Location SelectedLocation { get; protected set; }

        public void ActivateFeatures()
        {
            if (ActiveItem != null && SelectedLocation != null)
            {
                var vm = new DetailViewModel(
                    string.Format("{0}: {1}", ActiveItem.GetType().Name, ActiveItem.DisplayName),
                    ActiveItem.GetAsPropertyList()
                    );
                var message = new OpenWindow(vm);
                eventAggregator.BeginPublishOnUIThread(message);
            }
            else
            {

            }

        }

        public void DeactivateFeatures()
        {
            throw new NotImplementedException();
        }

        public void UpgradeFeatures()
        {
            throw new NotImplementedException();
        }
        #endregion only relevant for FeatureDefinitionListViewModel and ActivatedFeatureViewModel

    }
}
