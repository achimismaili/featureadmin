using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System;
using FeatureAdmin.Core;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionListViewModel : BaseListViewModel<ActiveIndicator<FeatureDefinition>, FeatureDefinition>,
        IHandle<ItemSelected<ActivatedFeatureSpecial>>,
        IHandle<ItemSelected<Location>>, 
        IHandle<SetSearchFilter<FeatureDefinition>>,
        IHandle<ResendItemSelectedRequest<FeatureDefinition>>
    {
        public FeatureDefinitionListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
         : base(eventAggregator, repository)
        {
            SelectionChanged();
        }

        public bool CanFilterLocation { get; protected set; }

        public bool CanUninstallFeatureDefinition { get; private set; }

        public void ActivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem.Item, SelectedLocation, Core.Models.Enums.FeatureAction.Activate));
        }

        public void DeactivateFeatures()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.FeatureToggleRequest(ActiveItem.Item, SelectedLocation, Core.Models.Enums.FeatureAction.Deactivate));
        }

        public bool CanFilterRight { get; protected set; }

        public void FilterRight(string searchQuery)
        {
            // In case feature definiton is scope=invalid, 
            // and faulty activated feature is searched for with unique ID ending with "/15", 
            // it will not be found, as activated feature will be faulty and will probably have ending "/0"
            // Therefore, "/15" is cut of here
            if (ActiveItem != null && ActiveItem.Item != null && ActiveItem.Item.Scope == Core.Models.Enums.Scope.ScopeInvalid)
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

            var searchFilter = new SetSearchFilter<Core.Models.Location>(
               searchQuery, null);
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void Handle(ResendItemSelectedRequest<FeatureDefinition> message)
        {
            SelectionChangedBase();
        }

        public void Handle([NotNull] ItemSelected<Location> message)
        {
            SelectedLocation = message.Item;
            CheckIfCanToggleFeatures();
            FilterResults(true);
        }

        public void Handle([NotNull] ItemSelected<ActivatedFeatureSpecial> message)
        {
            
            if (message.Item == null || message.Item.Location == null)
            {
                if (SelectedLocation != null)
                {
                    SelectedLocation = null;
                    CheckIfCanToggleFeatures();
                    FilterResults(true);
                }
            }
            else
            {
                var newLocation = message.Item.Location;

                if (SelectedLocation != newLocation)
                {
                    SelectedLocation = newLocation;
                    CheckIfCanToggleFeatures();
                    FilterResults(true);
                }
            }
            
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CheckIfCanToggleFeatures();
            CanFilterRight = ActiveItem != null;
            CanUninstallFeatureDefinition = ActiveItem != null;
            CanFilterLocation = ActiveItem != null;
        }

        public void UninstallFeatureDefinition()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeinstallationRequest(ActiveItem.Item));
        }

        protected void CheckIfCanToggleFeatures()
        {
            bool canActivate = false;
            bool canDeactivate = false;
            
            if (ActiveItem != null && ActiveItem.Item != null && SelectedLocation != null)
            {
                int locationsThatCanActivate = repository.GetLocationsCanActivate
                    (ActiveItem.Item, SelectedLocation).Count();

                canActivate = locationsThatCanActivate > 0;

                int locationsThatCanDeactivate = repository.GetLocationsCanDeactivate
                    (ActiveItem.Item, SelectedLocation).Count();
                canDeactivate = locationsThatCanDeactivate > 0;
            }


            // TODO: Implement check for can upgrade

            CanActivateFeatures = canActivate;
            CanDeactivateFeatures = canDeactivate;

            // update ActivatedFeatureViewModel
            eventAggregator.PublishOnUIThread(
                 new ActionOptionsUpdate(canActivate, canDeactivate)
                 );
        }

        protected override void FilterResults(bool suppressActiveItemChangeEvent = false)
        {
            var searchResult = repository.SearchFeatureDefinitions(
                searchInput, 
                SelectedScopeFilter, 
                null, 
                SelectedLocation);

            ShowResults(searchResult, suppressActiveItemChangeEvent);
        }
    }
}

