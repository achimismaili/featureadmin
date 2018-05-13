using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{
    public abstract class BaseListViewModel<T> : BaseItemViewModel<T>, IHandle<SetSearchFilter<T>> where T : class, IBaseItem
    {

        protected DateTime lastUpdateInitiatedSearch;
        protected IFeatureRepository repository;
        protected string searchInput;

        private Scope? selectedScopeFilter;
        public BaseListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository) :
            base(eventAggregator)
        {
            ScopeFilters = new ObservableCollection<Scope>(Common.Constants.Search.ScopeFilterList);
            
            lastUpdateInitiatedSearch = DateTime.Now;

            // https://github.com/Fody/PropertyChanged/issues/269
            ActivationProcessed += (s, e) => SelectionChanged();

            this.repository = repository;
        }

        public bool CanFilterThis { get; protected set; }

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

        public void FilterThis()
        {
            var searchFilter = new SetSearchFilter<T>(
                ActiveItem == null ? string.Empty : ActiveItem.Id.ToString(), null);
            Handle(searchFilter);
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

        public abstract void SelectionChanged();

        protected void FilterResults()
        {

            var searchResult = repository.Search<T>()
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
        }

        // Searching for results can be different in derived types
        protected abstract Func<T, bool> GetSearchForGuid(Guid guid);

        // Searching for results can be different in derived types
        protected abstract Func<T, bool> GetSearchForString(string searchString);

        protected virtual void SelectionChangedBase()
        {
            eventAggregator.PublishOnUIThread(
                 new Messages.ItemSelected<T>(ActiveItem)
                 );

            CanShowDetails = ActiveItem != null;
            CanFilterThis = ActiveItem != null;
        }
    }
}
