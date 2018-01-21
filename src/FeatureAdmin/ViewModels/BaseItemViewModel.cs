using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using FeatureAdmin.Core.Models.Enums;
using System.Linq.Expressions;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseItemViewModel<T> : Conductor<T>.Collection.OneActive where T : class, IDisplayableItem
    {

        protected IEventAggregator eventAggregator;
        public BaseItemViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            // https://github.com/Fody/PropertyChanged/issues/269
            ActivationProcessed += (s, e) => CanShowDetails = ActiveItem != null;
        }

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

        private bool canShowDetails;
        public bool CanShowDetails { get; private set; }
    
    }
}
