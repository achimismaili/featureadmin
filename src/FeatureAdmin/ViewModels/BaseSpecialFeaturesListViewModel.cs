using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseSpecialFeaturesListViewModel : BaseListViewModel<ActiveIndicator<ActivatedFeatureSpecial>, ActivatedFeatureSpecial>,
        IHandle<ItemSelected<FeatureDefinition>>,
        IHandle<SetSearchFilter<Location>>
    {
        protected IEnumerable<ActivatedFeatureSpecial> specialActionableFeaturesInFarm;

        public BaseSpecialFeaturesListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
        }

        public bool CanFilterFeature { get; protected set; }

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
            if (IsActive)
            {
                FilterResults(true);
            }
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CanFilterFeature = ActiveItem != null;
        }

        protected override void FilterResults(bool suppressActiveItemChangeEvent = false)
        {
            var searchResult = SearchSpecialFeatures(
                searchInput, 
                SelectedScopeFilter,
                SelectedFeatureDefinition,
                out specialActionableFeaturesInFarm
                );

            ShowResults(searchResult, suppressActiveItemChangeEvent);
        }

        protected abstract string noSpecialFeaturesFoundMessageTitle { get; }
        protected abstract string noSpecialFeaturesFoundMessageBody { get; }

        protected override void OnActivate()
        {
            SelectionChanged();

            var closeActivatedFeatureWindow = new ShowActivatedFeatureWindowMessage(false);
            eventAggregator.PublishOnUIThread(closeActivatedFeatureWindow);

            bool specialFeaturesAnyInFarm = false;
            specialActionableFeaturesInFarm = new ActivatedFeatureSpecial[0];

            // if no filters are set
            if (string.IsNullOrEmpty(searchInput) && SelectedScopeFilter == null)
            {

                // in case of no items, bring up a warning
                if (Items.Count == 0)
                {
                    // no special features in farm
                }
                else
                {
                    // no filters and items available, so this is a search in the full farm, no need to do it again
                    specialFeaturesAnyInFarm = true;
                    specialActionableFeaturesInFarm = Items.Select(afs => afs.Item); ;
                }
            }
            else
            {
                // in case of set filters, get all available features
                specialActionableFeaturesInFarm = GetAllSpecialFeatures(); 

                if (specialActionableFeaturesInFarm.Count() > 0)
                {
                    specialFeaturesAnyInFarm = true;
                }
            }

            // if no features exist in farm, notify user on activation, that no special features exist
            if (specialFeaturesAnyInFarm == false)
            {
                var noSpecialFeaturesFound = new ConfirmationRequest(
                    noSpecialFeaturesFoundMessageTitle,
                    noSpecialFeaturesFoundMessageBody
                    );

                eventAggregator.BeginPublishOnUIThread(noSpecialFeaturesFound);
            }

            CanSpecialActionFarm = specialFeaturesAnyInFarm;
        }

        public IEnumerable<ActiveIndicator<ActivatedFeatureSpecial>> SearchSpecialFeatures(
            string searchInput, 
            Core.Models.Enums.Scope? selectedScopeFilter,
            FeatureDefinition selectedFeatureDefinition,
            out IEnumerable<ActivatedFeatureSpecial> specialActionableFeaturesInFarm)
        {
            specialActionableFeaturesInFarm = GetAllSpecialFeatures();

            // deactivate option to trigger action on farm level if no special features are available
            CanSpecialActionFarm = specialActionableFeaturesInFarm.Any();

            return repository.SearchSpecialFeatures(specialActionableFeaturesInFarm, searchInput, selectedScopeFilter, selectedFeatureDefinition);
        }

        public abstract IEnumerable<ActivatedFeatureSpecial> GetAllSpecialFeatures();
        protected abstract void PublishSpecialActionRequest(IEnumerable<ActivatedFeatureSpecial> features);

        public void SpecialAction()
        {
            if (ActiveItem != null && ActiveItem.Item != null && ActiveItem.Item.ActivatedFeature != null)
            {
                var features = new ActivatedFeatureSpecial[] { ActiveItem.Item };
                PublishSpecialActionRequest(features);
            }

        }

        public void SpecialActionFarm()
        {
            PublishSpecialActionRequest(specialActionableFeaturesInFarm);
        }

        public void SpecialActionFiltered()
        {
            PublishSpecialActionRequest(Items.Select(afs => afs.Item));
        }
    }
}
