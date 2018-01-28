using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Linq;
using System;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Factories;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<FeatureDefinition>, IHandle<FarmFeatureDefinitionsLoaded>, IHandle<LocationsLoaded>, IHandle<ItemSelected<Location>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator)
         : base(eventAggregator)
        {
            SelectionChanged();
        }

        public bool CanFilterLocation { get; protected set; }

        public bool CanUninstallFeatureDefinition { get; private set; }

        public void FilterLocation()
        {
            var searchFilter = new SetSearchFilter<Location>(
                ActiveItem == null ? string.Empty : ActiveItem.Id.ToString(), null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void Handle([NotNull] FarmFeatureDefinitionsLoaded message)
        {
            foreach (FeatureDefinition fd in message.FarmFeatureDefinitions)
            {
                allItems.Add(fd);
            }
        }

        public void Handle([NotNull] LocationsLoaded message)
        {
            allItems.AddActivatedFeatures(message.LoadedFeatures);

            FilterResults();
        }

        public void Handle([NotNull] ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            CheckIfCanToggleFeatures();
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CheckIfCanToggleFeatures();
            CanUninstallFeatureDefinition = ActiveItem != null;
            CanFilterLocation = ActiveItem != null;
        }
        public void UninstallFeatureDefinition()
        {
            throw new NotImplementedException();
        }

        protected void CheckIfCanToggleFeatures()
        {
            bool canActivate = false;
            bool canDeactivate = false;
            bool canUpgrade = false;

            if (ActiveItem != null && SelectedLocation != null)
            {
                // check for activated feature
                if (ActiveItem.Scope == SelectedLocation.Scope)
                {
                    var isActivated = SelectedLocation.ActivatedFeatures.Any(f => f.FeatureId == ActiveItem.Id);

                    canActivate = !isActivated;
                    canDeactivate = isActivated;
                }
                // check for bulk feature toggle
                else if (ActiveItem.Scope < SelectedLocation.Scope && SelectedLocation.ChildCount > 0)
                {
                    canActivate = true;
                    canDeactivate = ActiveItem.ActivatedFeatures.Count > 0;
                }
            }

            // TODO: Implement check for can upgrade

            CanActivateFeatures = canActivate;
            CanDeactivateFeatures = canDeactivate;
            CanUpgradeFeatures = canUpgrade;

            // update ActivatedFeatureViewModel
            eventAggregator.PublishOnUIThread(
                 new Messages.ActionOptionsUpdate(canActivate, canDeactivate, canUpgrade)
                 );
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
                       || fd.ActivatedFeatures.Any(f => f.LocationId == guid);
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

