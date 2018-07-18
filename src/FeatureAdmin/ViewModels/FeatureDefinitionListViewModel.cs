using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using FeatureAdmin.Core;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<ItemSelected<Location>>, IHandle<SetSearchFilter<FeatureDefinition>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
         : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public void ActivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem, SelectedLocation, true));
        }

        public void DeactivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem, SelectedLocation, false));
        }

        public bool CanFilterLocation { get; protected set; }

        public bool CanUninstallFeatureDefinition { get; private set; }

        public void FilterLocation()
        {
            var searchFilter = new SetSearchFilter<Location>(
                ActiveItem == null ? string.Empty : ActiveItem.Id.ToString(), null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        protected override void FilterResults()
        {
            var searchResult = repository.SearchFeatureDefinitions(searchInput, SelectedScopeFilter, null);

            ShowResults(searchResult);
        }

        public void Handle([NotNull] ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            CheckIfCanToggleFeatures();
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
            throw new NotImplementedException();
        }

        protected void CheckIfCanToggleFeatures()
        {
            bool canActivate = false;
            bool canDeactivate = false;
            bool canUpgrade = false;

            if (ActiveItem != null && SelectedLocation != null)
            {
                int locationsThatCanActivate = repository.GetLocationsCanActivate
                    (ActiveItem, SelectedLocation).Count();

                canActivate = locationsThatCanActivate > 0;

                int locationsThatCanDeactivate = repository.GetLocationsCanDeactivate
                    (ActiveItem, SelectedLocation).Count();
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
    }
}

