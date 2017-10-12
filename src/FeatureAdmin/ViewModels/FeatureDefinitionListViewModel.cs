using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : Screen, IHandle<FeatureDefinitionUpdated>
    {
        private IEventAggregator eventAggregator;
        public ObservableCollection<FeatureDefinition> FeatureDefinitions { get; private set; }
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator)
        {
            FeatureDefinitions = new ObservableCollection<FeatureDefinition>();
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public void Handle(FeatureDefinitionUpdated message)
        {
            if (message == null || message.FeatureDefinition == null)
            {
                //TODO log
                return;
            }

            var featureToUpdate = message.FeatureDefinition;
            if (FeatureDefinitions.Any(l => l.Id == featureToUpdate.Id))
            {
                var existingFeature = FeatureDefinitions.FirstOrDefault(fd => fd.Id == featureToUpdate.Id);
                FeatureDefinitions.Remove(existingFeature);
            }

            FeatureDefinitions.Add(featureToUpdate);

        }
    }
}
