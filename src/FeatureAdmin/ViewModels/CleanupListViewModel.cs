using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;

namespace FeatureAdmin.ViewModels
{
    public class CleanupListViewModel : BaseListViewModel<ActivatedFeatureSpecial>, IHandle<ItemSelected<FeatureDefinition>>
    {
        public CleanupListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public bool CanFilterFeature { get; protected set; }

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
