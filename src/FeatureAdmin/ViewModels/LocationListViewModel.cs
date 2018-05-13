using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Repository;
using System;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : BaseListViewModel<Location>, IHandle<ItemSelected<FeatureDefinition>>
    {
        public LocationListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public bool CanFilterFeature { get; protected set; }

        public void FilterFeature()
        {
            var searchFilter = new SetSearchFilter<FeatureDefinition>(

                ActiveItem == null ? string.Empty : ActiveItem.Id.ToString(), null);
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
            var searchResult = repository.SearchLocations(searchInput, SelectedScopeFilter);

            ShowResults(searchResult);
        }
    }
}
