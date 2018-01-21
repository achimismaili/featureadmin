using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Linq;
using System;
using FeatureAdmin.Core.Messages;
using System.Collections.Generic;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Factories;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class ActivatedFeatureViewModel : Screen, IHandle<ItemSelected<FeatureDefinition>>, IHandle<ItemSelected<Location>>
    {
        private readonly IEventAggregator eventAggregator;
        public ActivatedFeatureViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            ActiveItem = null;
        }

        public ActivatedFeature ActiveItem { get; private set; }

        public FeatureDefinition SelectedFeatureDefinition { get; private set; }

        public Location SelectedLocation { get; private set; }

        public void Handle(ItemSelected<FeatureDefinition> message)
        {
            SelectedFeatureDefinition = message.Item;
            SetActivatedFeature();
        }

        public void Handle(ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            SetActivatedFeature();
        }

        private void SetActivatedFeature()
        {
            if (SelectedLocation != null && 
                SelectedFeatureDefinition != null &&
                SelectedLocation.ActivatedFeatures.Count > 0 &&
                SelectedFeatureDefinition.ActivatedFeatures.Count > 0
                )
            {
                ActiveItem = SelectedLocation.ActivatedFeatures.FirstOrDefault(
                    lf => SelectedFeatureDefinition.ActivatedFeatures.Any(
                        df => df == lf
                        )
                    );
                }
            else
            {
                ActiveItem = null;
            }
        }
    }
}

