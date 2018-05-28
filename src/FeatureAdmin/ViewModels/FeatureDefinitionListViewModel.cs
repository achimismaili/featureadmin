using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using FeatureAdmin.Core;
using FeatureAdmin.Messages;
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<ItemSelected<Location>>
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
            var searchResult = repository.SearchFeatureDefinitions(searchInput, SelectedScopeFilter, null );

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
                // check for activated feature
                if (ActiveItem.Scope == SelectedLocation.Scope)
                {
                    var isActivated = repository.IsFeatureActivated(ActiveItem.Id, SelectedLocation.Id);

                    canActivate = !isActivated;
                    canDeactivate = isActivated;
                }
                // check for bulk feature toggle, check if scope is ok and if feature is activated at all
                else if (ActiveItem.Scope < SelectedLocation.Scope && SelectedLocation.ChildCount > 0)
                {
                    canActivate = true;
                    canDeactivate = repository.IsFeatureActivated(ActiveItem.Id);
                }
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

