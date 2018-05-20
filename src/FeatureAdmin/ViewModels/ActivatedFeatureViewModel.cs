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
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{
    public class ActivatedFeatureViewModel : BaseItemViewModel<ActivatedFeature>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<ItemSelected<Location>>, IHandle<ActionOptionsUpdate>
    {
        protected IFeatureRepository repository;

        public ActivatedFeatureViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator)
        {
            this.repository = repository;
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
            // change logic to refer to repository for this

            Items.Clear();

            if (SelectedLocation != null && SelectedFeatureDefinition != null)
            {
                var activeItem = repository.GetActivatedFeature(SelectedFeatureDefinition.Id, SelectedLocation.Id);

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

