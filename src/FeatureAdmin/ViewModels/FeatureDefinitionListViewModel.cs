using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Linq;
using System;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Factories;
using FeatureAdmin.Messages;
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<FarmFeatureDefinitionsLoaded>, IHandle<LocationsLoaded>, IHandle<ItemSelected<Location>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
         : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public bool CanFilterLocation { get; protected set; }

        public bool CanUninstallFeatureDefinition { get; private set; }

        public void FilterLocation()
        {
            var searchFilter = new SetSearchFilter<Location>(
                ActiveItem == null ? string.Empty : ActiveItem.Id.ToString(), null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        protected new void FilterResults()
        {
            var searchResult = repository.SearchFeatureDefinitions(searchInput, SelectedScopeFilter, null );

            var activeItemCache = ActiveItem;

            Items.Clear();
            Items.AddRange(searchResult);

            if (activeItemCache != null)
            {
                if (Items.Contains(activeItemCache))
                {
                    ActivateItem(activeItemCache);
                }
                else
                {
                    SelectionChanged();
                }
            }
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
                    var isActivated = SelectedLocation.ActivatedFeatures.Any(f => f.FeatureId == ActiveItem.Id);

                    canActivate = !isActivated;
                    canDeactivate = isActivated;
                }
                // check for bulk feature toggle
                else if (ActiveItem.Scope < SelectedLocation.Scope && SelectedLocation.ChildCount > 0)
                {
                    canActivate = true;
                    canDeactivate = ActiveItem.ActivatedFeatures.Count > 0;
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

