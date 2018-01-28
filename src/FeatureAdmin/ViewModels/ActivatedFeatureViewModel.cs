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
    public class ActivatedFeatureViewModel : BaseItemViewModel<ActivatedFeature>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<ItemSelected<Location>>, IHandle<ActionOptionsUpdate>
    {
        public ActivatedFeatureViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

  

        public void Handle(ItemSelected<FeatureDefinition> message)
        {
            SelectedFeatureDefinition = message.Item;
            SetActivatedFeature();
        }

        public void Handle(ActionOptionsUpdate message)
        {
            CanActivateFeatures = message.CanActivate;
            CanDeactivateFeatures = message.CanDeactivate;
            CanUpgradeFeatures = message.CanUpgrade;

        }

        public void Handle(ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            SetActivatedFeature();
        }

        private void SetActivatedFeature()
        {
            Items.Clear();

            if (SelectedLocation != null &&
                SelectedFeatureDefinition != null &&
                SelectedLocation.ActivatedFeatures.Count > 0 &&
                SelectedFeatureDefinition.ActivatedFeatures.Count > 0
                )
            {
                var activeItem = SelectedLocation.ActivatedFeatures.FirstOrDefault(
                    lf => SelectedFeatureDefinition.ActivatedFeatures.Any(
                        df => df == lf
                        )
                    );

                if (activeItem != null)
                {
                    Items.Add(activeItem);
                    ActiveItem = activeItem;
                }
                else
                {
                    ActiveItem = null;
                }
            }
            else
            {
                ActiveItem = null;
            }
        }
    }
}

