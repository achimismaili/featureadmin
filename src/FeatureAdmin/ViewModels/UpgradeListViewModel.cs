using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class UpgradeListViewModel : BaseListViewModel<ActivatedFeatureSpecial>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<SetSearchFilter<Location>>
    {
        public UpgradeListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            DisplayName = "Upgrade";
        }

        protected override void OnActivate()
        {
            SelectionChanged();

            // check if any special features exist
            if (Items.Count == 0)
            {
                bool openNoSpecialFeaturesFoundDialog = false;

                // if filters are active initiate a search with no filters
                if (string.IsNullOrEmpty(searchInput) && SelectedScopeFilter == null)
                {
                    openNoSpecialFeaturesFoundDialog = true;
                }
                else
                {
                    var anySpecialFeatures = repository.SearchFeaturesToCleanup(string.Empty, null);

                    openNoSpecialFeaturesFoundDialog = anySpecialFeatures.Count() == 0;
                }

                // if no features exist in farm, notify user on activation, that no special features exist
                if (openNoSpecialFeaturesFoundDialog)
                {
                    var noSpecialFeaturesFound = new ConfirmationRequest(
                        "No features found for upgrade",
                        "No activated features were found during farm load that require an upgrade.\n" +
                        "All activated features in the farm seem to be up to date."
                        );

                    eventAggregator.BeginPublishOnUIThread(noSpecialFeaturesFound);
                }
            }
        }

        public bool CanFilterFeature { get; protected set; }

        public void FilterFeature()
        {
            var searchFilter = new SetSearchFilter<FeatureDefinition>(

                ActiveItem == null ? string.Empty : ActiveItem.ActivatedFeature.FeatureId.ToString(), null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        // as SetSearchFilter is handled in generic base class, search filter has to be converted to AcitvatedFeatureSpecial
        public void Handle(SetSearchFilter<Location> message)
        {
            var genericSearchFilter = new SetSearchFilter<ActivatedFeatureSpecial>(
                message.SearchQuery,
                message.SearchScope
                );

            Handle(genericSearchFilter);
        }

        public void Handle([NotNull] ItemSelected<FeatureDefinition> message)
        {
            SelectedFeatureDefinition = message.Item;
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CanFilterFeature = ActiveItem != null;
        }

        protected override void FilterResults()
        {
            var searchResult = repository.SearchFeaturesToUpgrade(searchInput, SelectedScopeFilter);

            ShowResults(searchResult);
        }
    }
}
