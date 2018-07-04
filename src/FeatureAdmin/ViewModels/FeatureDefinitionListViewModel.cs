using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using FeatureAdmin.Core;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;

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
                // check for SAME SCOPE
                if (ActiveItem.Scope == SelectedLocation.Scope)
                {
                    // NOT Sandboxed Solution
                    if (ActiveItem.SandBoxedSolutionLocation == null)
                    {

                        var isActivated = repository.IsFeatureActivated(ActiveItem.Id, SelectedLocation.Id);

                        canActivate = !isActivated;
                        canDeactivate = isActivated;


                    }
                    // Same Scope AND Sandboxed Solution
                    else
                    {
                        // SANDBOX scope SITE
                        if (ActiveItem.Scope == Core.Models.Enums.Scope.Site)
                        {
                            if (ActiveItem.SandBoxedSolutionLocation == SelectedLocation.Id)
                            {
                                var isActivated = repository.IsFeatureActivated(ActiveItem.Id, SelectedLocation.Id);

                                canActivate = !isActivated;
                                canDeactivate = isActivated;
                            }
                            else
                            {
                                // sandboxed solution site feature cannot be activated in different site -->
                                canActivate = false;
                                canDeactivate = false;
                            }
                        }


                        // SANDBOX scope WEB
                        else if (ActiveItem.Scope == Core.Models.Enums.Scope.Web)
                        {
                            if (ActiveItem.SandBoxedSolutionLocation == SelectedLocation.Parent)
                            {
                                var isActivated = repository.IsFeatureActivated(ActiveItem.Id, SelectedLocation.Id);

                                canActivate = !isActivated;
                                canDeactivate = isActivated;
                            }
                            else
                            {
                                // sandboxed solution site feature cannot be activated in different site -->
                                canActivate = false;
                                canDeactivate = false;
                            }
                        }
                    }
                }
                // check for DIFFERENT SCOPE - bulk feature toggle, check if scope-relation is ok and if feature is activated at all
                else if (ActiveItem.Scope < SelectedLocation.Scope && SelectedLocation.ChildCount > 0)
                {
                    // only needs to check, if it is active or inactive anywhere in the farm, as the scope of feature definition is lower than location scope
                    canActivate = repository.IsItPossibleToActivateFeature(ActiveItem);
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

