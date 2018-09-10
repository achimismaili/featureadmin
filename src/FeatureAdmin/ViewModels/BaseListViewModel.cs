using Caliburn.Micro;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Core.Models;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseListViewModel<A, T> : BaseItemViewModel<A, T>, IHandle<ProgressMessage> where A : ActiveIndicator<T> where T : class
        // , IHandle<SetSearchFilter<T>>  where T : class // search filter is handled in derivate classes
    {

        protected bool NotifyOnActiveItemChange = true;

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
        public bool CanFilterFeatureDefinitions { get; protected set; }

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
            // In case feature definiton is scope=invalid, 
            // and faulty activated feature is searched for with unique ID ending with "/0", 
            // it will not be found on the left
            // Therefore, "/0" is cut of here
            if (!string.IsNullOrEmpty(searchQuery) && searchQuery.EndsWith("/0"))
            {
                // check if first part of search query is a guid
                var firstPartOfQuery = searchQuery.Split('/');

                Guid notNeededGuid;

                if (firstPartOfQuery.Length > 0 && Guid.TryParse(firstPartOfQuery[0], out notNeededGuid))
                {
                    // then remove "/0" from the end
                    searchQuery = firstPartOfQuery[0];
                }
            }
            
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

        protected abstract void FilterResults(bool suppressActiveItemChangeEvent = false);

        protected void ShowResults(IEnumerable<A> searchResult, bool suppressActiveItemChangeEvent = false)
        {
            var activeItemCache = ActiveItem;
            try
            {
                if (activeItemCache != null && suppressActiveItemChangeEvent)
                {
                    NotifyOnActiveItemChange = false;
                }

                Items.Clear();
                Items.AddRange(searchResult);

                if (activeItemCache != null)
                {
                    var equalItemExists = Items.Where(i => i.Item.Equals(activeItemCache.Item)).FirstOrDefault();

                    if (equalItemExists != null)
                    {
                        ActivateItem(equalItemExists);
                    }
                    else
                    {
                        SelectionChanged();
                    }
                }
            }
            finally
            {
                // turn on again
                if (!NotifyOnActiveItemChange)
                {
                    NotifyOnActiveItemChange = true;
                }
            }

            CanSpecialActionFiltered = Items.Count > 0;
        }

        public abstract void SelectionChanged();


        protected virtual void SelectionChangedBase()
        {

            T item;

            if (ActiveItem == null)
            {
                item = null;
            }
            else
            {
                item = ActiveItem.Item;
            }

            if (NotifyOnActiveItemChange)
            {
                eventAggregator.PublishOnUIThread(
                                new Messages.ItemSelected<T>(item)
                                );
            }

            CanFilterFeatureDefinitions = item != null;
            CanShowDetails = item != null;
            CanFilterThis = item != null;
            CanSpecialAction = item != null;
        }
    }
}
