using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Linq;
using System;
using FeatureAdmin.Core.Messages;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<ItemUpdated<IEnumerable<FeatureDefinition>>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator)
         : base(eventAggregator)
        {
        }

        public void Handle(ItemUpdated<IEnumerable<FeatureDefinition>> message)
        {
            if (message == null || message.Item == null)
            {
                //TODO log
                return;
            }

            var featureDefinitions = message.Item;

            foreach (FeatureDefinition fd in featureDefinitions)
            {
                var existingItem = allItems.FirstOrDefault(l => l.Id == fd.Id);

                if (existingItem != null)
                {
                    foreach (var f in existingItem.ActivatedFeatures)
                    {
                        fd.ToggleActivatedFeature(f, true);
                    }
                    allItems.Remove(existingItem);
                }

                allItems.Add(fd);

            }

            //if (lastUpdateInitiatedSearch.AddSeconds(3) < DateTime.Now)
            //{
            //    lastUpdateInitiatedSearch = DateTime.Now;
            FilterResults();
            //}


        }

        /// <summary>
        /// custom guid search in items for derived class
        /// </summary>
        /// <param name="guid">the guid to search for</param>
        /// <returns>all items that contain a guid in Id, parent or activated features</returns>
        protected override Func<FeatureDefinition, bool> GetSearchForGuid(Guid guid)
        {
            // see also https://stackoverflow.com/questions/34220256/how-to-call-method-function-in-where-clause-of-a-linq-query-as-ienumerable-objec
            return fd => fd.Id == guid
                       || fd.ActivatedFeatures.Any(f => f.FeatureId == guid);
        }

        /// <summary>
        /// custom search in items for derived class
        /// </summary>
        /// <param name="searchString">the search string (already in lower case)</param>
        /// <returns>returns function for searching in items</returns>
        /// <remarks>search string is expected to be already lower case (provided by base class)</remarks>
        protected override Func<FeatureDefinition, bool> GetSearchForString(string searchString)
        {
            return fd => fd.DisplayName.ToLower().Contains(searchString) ||
                            fd.Title.ToLower().Contains(searchString);
        }
    }
}

