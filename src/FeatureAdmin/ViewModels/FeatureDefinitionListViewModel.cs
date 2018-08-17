using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using FeatureAdmin.Core;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<ActiveIndicator<FeatureDefinition>, FeatureDefinition>,
        IHandle<ItemSelected<ActivatedFeatureSpecial>>,
        IHandle<ItemSelected<Location>>, 
        IHandle<SetSearchFilter<FeatureDefinition>>,
        IHandle<ResendItemSelectedRequest<FeatureDefinition>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
         : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public bool CanFilterLocation { get; protected set; }

        public bool CanUninstallFeatureDefinition { get; private set; }

        public void ActivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem.Item, SelectedLocation, Core.Models.Enums.FeatureAction.Activate));
        }

        public void DeactivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem.Item, SelectedLocation, Core.Models.Enums.FeatureAction.Deactivate));
        }

        public void FilterRight(string searchQuery)
        {
            var searchFilter = new SetSearchFilter<Core.Models.Location>(
               searchQuery, null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void Handle(ResendItemSelectedRequest<FeatureDefinition> message)
        {
            SelectionChangedBase();
        }

        public void Handle([NotNull] ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            CheckIfCanToggleFeatures();
        }

        public void Handle([NotNull] ItemSelected<ActivatedFeatureSpecial> message)
        {
            
            if (message.Item == null || message.Item.Location == null)
            {
                if (SelectedLocation != null)
                {
                    SelectedLocation = null;
                    CheckIfCanToggleFeatures();
                }
            }
            else
            {
                var newLocation = SelectedLocation = message.Item.Location;

                if (SelectedLocation != newLocation)
                {
                    SelectedLocation = newLocation;
                    CheckIfCanToggleFeatures();
                }
            }
            
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CheckIfCanToggleFeatures();
            CanUninstallFeatureDefinition = ActiveItem != null;
            CanFilterLocation = ActiveItem != null;
        }

        public void UninstallFeatureDefinition()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeinstallationRequest(ActiveItem.Item));
        }

        protected void CheckIfCanToggleFeatures()
        {
            bool canActivate = false;
            bool canDeactivate = false;
            bool canUpgrade = false;

            if (ActiveItem != null && ActiveItem.Item != null && SelectedLocation != null)
            {
                int locationsThatCanActivate = repository.GetLocationsCanActivate
                    (ActiveItem.Item, SelectedLocation).Count();

                canActivate = locationsThatCanActivate > 0;

                int locationsThatCanDeactivate = repository.GetLocationsCanDeactivate
                    (ActiveItem.Item, SelectedLocation).Count();
                canDeactivate = locationsThatCanDeactivate > 0;
            }


            // TODO: Implement check for can upgrade

            CanActivateFeatures = canActivate;
            CanDeactivateFeatures = canDeactivate;
            CanUpgradeFeatures = canUpgrade;

            // update ActivatedFeatureViewModel
            eventAggregator.PublishOnUIThread(
                 new Messages.ActionOptionsUpdate(canActivate, canDeactivate, canUpgrade)
                 );
        }

        protected override void FilterResults()
        {
            var searchResult = repository.SearchFeatureDefinitions(
                searchInput, 
                SelectedScopeFilter, 
                null, 
                SelectedLocation);

            ShowResults(searchResult);
        }
    }
}

