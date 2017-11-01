using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : BaseListViewModel<Location>
    {
        public LocationListViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
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
