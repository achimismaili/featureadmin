using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Linq;
using System;
using FeatureAdmin.Core.Messages;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<ItemUpdated<Location>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator)
         : base(eventAggregator)
        {
        }

        /// <summary>
        /// In feature definitions, activated features are just saved as guids
        /// </summary>
        /// <param name="message">item updated containing updated location</param>
        public void Handle(ItemUpdated<Location> message)
        {
            if (message == null || message.Item == null ||
                message.Item.ActivatedFeatures == null || message.Item.ActivatedFeatures.Count == 0)
            {
                //TODO log
                return;
            }

            var location = message.Item;

            var locationId = location.Id;

            var updatedFeatures = location.ActivatedFeatures.Select(f => f.FeatureId).ToList();

            // get existing activated features for this location

            var existingFeatures = Items.Where(GetSearchForGuid(locationId));
            if (existingFeatures != null && existingFeatures.Count() > 0)
            {
                foreach (FeatureDefinition fd in existingFeatures)
                {
                    if (!updatedFeatures.Contains(fd.Id))
                    {
                        Items.Remove(fd);
                        fd.ToggleActivatedFeature(locationId, false);
                        Items.Add(fd);
                    }
                    else
                    {
                        updatedFeatures.Remove(fd.Id);
                    }
                }
            }

            foreach (Guid f in updatedFeatures)
            {
                var fdExisting = Items.FirstOrDefault(fd => fd.Id == f);

                if (fdExisting != null)
                {
                    Items.Remove(fdExisting);
                    fdExisting.ToggleActivatedFeature(locationId, true);
                    Items.Add(fdExisting);
                }
                else
                {
                    var featureToAdd = location.ActivatedFeatures.FirstOrDefault(af => af.FeatureId == f);
                    if (featureToAdd != null )
                    {
                        FeatureDefinition newDef;

                        if (featureToAdd.Faulty || featureToAdd.Definition == null)
                        {
                            newDef = FeatureDefinition.GetFaultyDefinition(
                                featureToAdd.FeatureId,
                                location.Scope,
                                featureToAdd.Version
                              );
                            
                        }
                        else
                        {
                            newDef = featureToAdd.Definition;
                        }

                        newDef.ToggleActivatedFeature(f, true);
                        Items.Add(newDef);
                    }
                    else
                    {
                        //TODO Log unexpected definition not found
                    }
                    
                }
            }

            FilterResults();
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
                       || fd.ActivatedFeatures.Any(f => f == guid);
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

