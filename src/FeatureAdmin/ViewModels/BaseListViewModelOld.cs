using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using FeatureAdmin.Core.Models.Enums;
using System.Linq.Expressions;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseListViewModelOld<T> : Conductor<T>.Collection.OneActive, IHandle<ItemUpdated<T>>, IHandle<SetSearchFilter<T>> where T : BaseItem
    {
        // The filtered Items
        public ObservableCollection<T> SearchResultItems { get; private set; }

        private IEventAggregator eventAggregator;

        public BaseListViewModelOld(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            ScopeFilters = new ObservableCollection<Scope>(Common.Constants.Search.ScopeFilterList);
            SearchResultItems = new ObservableCollection<T>(); 
        }

        private T selectedItem;
        public T SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                eventAggregator.BeginPublishOnUIThread(new ItemSelected<T>(selectedItem));
            }
        }

        public void Handle(ItemUpdated<T> message)
        {
            if (message == null || message.Item == null)
            {
                //TODO log
                return;
            }

            var itemToAdd = message.Item;
            if (Items.Any(l => l.Id == itemToAdd.Id))
            {
                var existingLocation = Items.FirstOrDefault(l => l.Id == itemToAdd.Id);
                Items.Remove(existingLocation);
            }

            Items.Add(itemToAdd);
            FilterResults();
        }

        public ObservableCollection<Scope> ScopeFilters { get; private set; }

        private Scope? selectedScopeFilter;
        public Scope? SelectedScopeFilter
        {
            get { return selectedScopeFilter; }
            set
            {
                selectedScopeFilter = value;
                FilterResults();
            }
        }

        private string searchInput;
        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                FilterResults();
            }
        }

        // Searching for results can be different in derived types
        protected abstract Func<T, bool> GetSearchForGuid(Guid guid);


        // Searching for results can be different in derived types
        protected abstract Func<T, bool> GetSearchForString(string searchString);


        protected void FilterResults()
        {
            IEnumerable<T> searchResult;

            if (string.IsNullOrEmpty(searchInput))
            {
                searchResult = Items;
            }
            else
            {
                Guid idGuid;
                Guid.TryParse(searchInput, out idGuid);

                // if searchInput is not a guid, seachstring will always be a guid.empty
                // to also catch, if user intentionally wants to search for guid empty, this is checked here, too
                if (searchInput.Equals(Guid.Empty.ToString()) || idGuid != Guid.Empty)
                {
                    searchResult = Items.Where(GetSearchForGuid(idGuid));
                }
                else
                {
                    var lowerCaseSearchInput = searchInput.ToLower();
                    searchResult =
                        Items.Where(GetSearchForString(lowerCaseSearchInput));
                }

            }

            if (SelectedScopeFilter != null)
            {
                searchResult =
                    searchResult.Where(l => l.Scope == SelectedScopeFilter.Value);
            }

            SearchResultItems = new ObservableCollection<T>(searchResult);
        }

        public void Handle(SetSearchFilter<T> message)
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
}
