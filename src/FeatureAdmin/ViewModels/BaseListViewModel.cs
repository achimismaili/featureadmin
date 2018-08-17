using Caliburn.Micro;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseListViewModel<A, T> : BaseItemViewModel<A, T>, IHandle<ProgressMessage> where A : ActiveIndicator<T> where T : class
        // , IHandle<SetSearchFilter<T>>  where T : class // search filter is handled in derivate classes
    {

        protected DateTime lastUpdateInitiatedSearch;
        protected string searchInput;

        private Scope? selectedScopeFilter;
        public BaseListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository) :
            base(eventAggregator, repository)
        {
            ScopeFilters = new ObservableCollection<Scope>(Common.Constants.Search.ScopeFilterList);

            lastUpdateInitiatedSearch = DateTime.Now;

            // https://github.com/Fody/PropertyChanged/issues/269
            ActivationProcessed += (s, e) => SelectionChanged();
        }

        public bool CanFilterThis { get; protected set; }
        public bool CanSpecialAction { get; protected set; }
        public bool CanSpecialActionFiltered { get; protected set; }
        public bool CanSpecialActionFarm { get; protected set; }


        public ObservableCollection<Scope> ScopeFilters { get; private set; }

        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                FilterResults();
            }
        }

        public Scope? SelectedScopeFilter
        {
            get { return selectedScopeFilter; }
            set
            {
                selectedScopeFilter = value;
                FilterResults();
            }
        }

        public void FilterFeatureDefinitions(string searchQuery)
        {
            var searchFilter = new SetSearchFilter<Core.Models.FeatureDefinition>(
                                            searchQuery, null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void FilterThis(string searchQuery)
        {
            var searchFilter = new SetSearchFilter<T>(
                searchQuery, null);
            Handle(searchFilter);
        }

        public void Handle(SetSearchFilter<T> message)
        {
            // only set search filter, if active
            if (IsActive || message is SetSearchFilter<Core.Models.FeatureDefinition>)
            {
                if (message == null)
                {
                    return;
                };

                if (message.SetQuery)
                {
                    SearchInput = message.SearchQuery;
                }

                if (message.SetScope)
                {
                    SelectedScopeFilter = message.SearchScope;
                }
            }
        }

        /// <summary>
        /// whenever Progress is made, update the search results
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ProgressMessage message)
        {
            FilterResults();
        }

        protected abstract void FilterResults();

        protected void ShowResults(IEnumerable<A> searchResult)
        {
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

            CanSpecialActionFiltered = Items.Count > 0;
        }

        public abstract void SelectionChanged();


        protected virtual void SelectionChangedBase()
        {
            eventAggregator.PublishOnUIThread(
                 new Messages.ItemSelected<T>(ActiveItem.Item)
                 );

            CanShowDetails = ActiveItem != null;
            CanFilterThis = ActiveItem != null;
            CanSpecialAction = ActiveItem != null;
        }
    }
}
