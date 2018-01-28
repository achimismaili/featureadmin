using Caliburn.Micro;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : BaseListViewModel<Location>, IHandle<LocationsLoaded>, IHandle<ItemSelected<FeatureDefinition>>
    {
        public LocationListViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
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

        public void Handle(LocationsLoaded message)
        {
            var locations = message.ChildLocations;

            var locationsToReplace = allItems.Where(al => locations.Any(l => l.Id == al.Id)).ToList();

            if (locationsToReplace != null && locationsToReplace.Any())
            {
                for (int i = locationsToReplace.Count() - 1; i >= 0; i--)
                {
                    allItems.Remove(locationsToReplace[i]);
                }
            }

            foreach (Location l in locations)
            {
                allItems.Add(l);

            }

            //if (lastUpdateInitiatedSearch.AddSeconds(3) < DateTime.Now)
            //{
            //    lastUpdateInitiatedSearch = DateTime.Now;
            FilterResults();
            //}


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
        /// <summary>
        /// custom guid search in items for derived class
        /// </summary>
        /// <param name="guid">the guid to search for</param>
        /// <returns>all items that contain a guid in Id, parent or activated features</returns>
        protected override Func<Location, bool> GetSearchForGuid(Guid guid)
        {
            // see also https://stackoverflow.com/questions/34220256/how-to-call-method-function-in-where-clause-of-a-linq-query-as-ienumerable-objec
            return l => l.Id == guid
                       || l.Parent == guid
                       || l.ActivatedFeatures.Any(f => f.FeatureId == guid);
        }
        /// <summary>
        /// custom search in items for derived class
        /// </summary>
        /// <param name="searchString">the search string (already in lower case)</param>
        /// <returns>returns function for searching in items</returns>
        /// <remarks>search string is expected to be already lower case (provided by base class)</remarks>
        protected override Func<Location, bool> GetSearchForString(string searchString)
        {
            return l => l.DisplayName.ToLower().Contains(searchString) ||
                            l.Url.ToLower().Contains(searchString);
        }
    }
}
