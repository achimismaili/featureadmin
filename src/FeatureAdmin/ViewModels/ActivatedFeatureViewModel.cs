using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;

namespace FeatureAdmin.ViewModels
{
    public class ActivatedFeatureViewModel : BaseItemViewModel<ActiveIndicator<ActivatedFeature>, ActivatedFeature>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<ItemSelected<Location>>, IHandle<ActionOptionsUpdate>
    {
        public ActivatedFeatureViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
        }

        public void DeactivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(SelectedFeatureDefinition, SelectedLocation, Core.Models.Enums.FeatureAction.Deactivate));
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
                var activeItem = repository.GetActivatedFeature(SelectedFeatureDefinition.UniqueIdentifier, SelectedLocation.UniqueId);

                if (activeItem != null)
                {
                    var activeIndicatorItem = new ActiveIndicator<ActivatedFeature>(activeItem, true);
                    Items.Add(activeIndicatorItem);
                    ActiveItem = activeIndicatorItem;
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

