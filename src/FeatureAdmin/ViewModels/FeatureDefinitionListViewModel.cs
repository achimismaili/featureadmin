using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : Screen
    {
        private IEventAggregator eventAggregator;

        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }
    }
}
