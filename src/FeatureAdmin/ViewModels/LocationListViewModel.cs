using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : BaseListViewModel<ActiveIndicator<Location>, Location>, IHandle<ItemSelected<FeatureDefinition>>, IHandle<SetSearchFilter<Location>>
    {
        public LocationListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            DisplayName = "Activation";
        }

        protected override void OnActivate()
        {
            var showActivatedFeatureWindow = new ShowActivatedFeatureWindowMessage(true);
            eventAggregator.PublishOnUIThread(showActivatedFeatureWindow);

            SelectionChanged();
        }

        public bool CanFilterFeature { get; protected set; }

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
            var searchResult = repository.SearchLocations(
                searchInput, 
                SelectedScopeFilter, 
                SelectedFeatureDefinition);

            ShowResults(searchResult);
        }
    }
}
