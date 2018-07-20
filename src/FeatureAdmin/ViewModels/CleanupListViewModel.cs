using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public sealed class CleanupListViewModel : BaseListViewModel<ActivatedFeatureSpecial>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<SetSearchFilter<Location>>
    {
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
                        "No faulty features found",
                         "Please make sure, farm load is already completed (see progress bar in the footer).\n" +
                            "Currently, no faulty/orphaned activated features that require a clean up were found during farm load.\n" +
                            "So, currently, all activated features in the farm seem to be clean, meaning, they seem to have a valid definition."
                        );

                    eventAggregator.BeginPublishOnUIThread(noSpecialFeaturesFound);
                }
            }
        }
        public CleanupListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            DisplayName = "Cleanup";
        }

        public bool CanFilterFeature { get; private set; }

        public void FilterFeature()
        {
            var searchFilter = new SetSearchFilter<FeatureDefinition>(

                ActiveItem == null ? string.Empty : ActiveItem.ActivatedFeature.FeatureId.ToString(), null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void Handle([NotNull] ItemSelected<FeatureDefinition> message)
        {
            SelectedFeatureDefinition = message.Item;
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

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CanFilterFeature = ActiveItem != null;
        }

        protected override void FilterResults()
        {
            var searchResult = repository.SearchFeaturesToCleanup(searchInput, SelectedScopeFilter);

            ShowResults(searchResult);
        }
    }
}
