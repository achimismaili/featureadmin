using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseSpecialFeaturesListViewModel : BaseListViewModel<ActivatedFeatureSpecial>,
        IHandle<ItemSelected<FeatureDefinition>>,
        IHandle<SetSearchFilter<Location>>
    {
        protected IEnumerable<ActivatedFeatureSpecial> specialActionableFeaturesInFarm;

        public BaseSpecialFeaturesListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
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
            var searchResult = SearchSpecialFeatures(searchInput, SelectedScopeFilter, out specialActionableFeaturesInFarm);

            ShowResults(searchResult);
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
                    specialActionableFeaturesInFarm = Items; ;
                }
            }
            else
            {
                // in case of set filters, always perform a search with no filters to get all available features
                specialActionableFeaturesInFarm = SearchSpecialFeatures(string.Empty, null, out specialActionableFeaturesInFarm);

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

        public abstract IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(
            string searchInput, 
            Core.Models.Enums.Scope? selectedScopeFilter,
            out IEnumerable<ActivatedFeatureSpecial> specialActionableFeaturesInFarm);

        protected abstract void PublishSpecialActionRequest(IEnumerable<ActivatedFeatureSpecial> features);

        public void SpecialAction()
        {
            if (ActiveItem != null && ActiveItem.ActivatedFeature != null)
            {
                var features = new ActivatedFeatureSpecial[] { ActiveItem };
                PublishSpecialActionRequest(features);
            }

        }

        public void SpecialActionFarm()
        {
            PublishSpecialActionRequest(specialActionableFeaturesInFarm);
        }

        public void SpecialActionFiltered()
        {
            PublishSpecialActionRequest(Items);
        }
    }
}
