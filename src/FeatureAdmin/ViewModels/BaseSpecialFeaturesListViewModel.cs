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
        protected System.Collections.Generic.IEnumerable<ActivatedFeature> specialActionableFeaturesInFarm;

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
            var searchResult = SearchSpecialFeatures(searchInput, SelectedScopeFilter);

            ShowResults(searchResult);
        }

        protected abstract string noSpecialFeaturesFoundMessageTitle { get; }
        protected abstract string noSpecialFeaturesFoundMessageBody { get; }

        protected override void OnActivate()
        {
            SelectionChanged();

            bool specialFeaturesAnyInFarm = false;
            specialActionableFeaturesInFarm = new ActivatedFeature[0];

            // check if any special features exist
            if (Items.Count == 0)
            {
                // if filters are active initiate a search with no filters
                if (string.IsNullOrEmpty(searchInput) && SelectedScopeFilter == null)
                {
                    // no special features in farm
                }
                else
                {
                    var allSpecialFeaturesInFarmBySearch = SearchSpecialFeatures(string.Empty, null);

                    if (allSpecialFeaturesInFarmBySearch.Count() > 0)
                    {
                        specialActionableFeaturesInFarm = allSpecialFeaturesInFarmBySearch.Select(sf => sf.ActivatedFeature);
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
            }
            else
            {
                specialFeaturesAnyInFarm = true;
                specialActionableFeaturesInFarm = Items.Select(sf => sf.ActivatedFeature); ;
            }

            CanSpecialActionFarm = specialFeaturesAnyInFarm;
        }

        public abstract IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(string searchInput, Core.Models.Enums.Scope? selectedScopeFilter);

        public abstract void SpecialAction();
        
        public abstract void SpecialActionFarm();

        public abstract void SpecialActionFiltered();
    }
}
