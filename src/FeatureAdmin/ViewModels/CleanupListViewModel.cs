using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Repository;
using System.Linq;
using FeatureAdmin.Core.Models.Enums;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public sealed class CleanupListViewModel : BaseSpecialFeaturesListViewModel
    {
        public CleanupListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            DisplayName = "Cleanup";
        }

        protected override string noSpecialFeaturesFoundMessageBody
        {
            get
            {
                return "Please make sure, farm load is already completed (see progress bar in the footer).\n" +
                            "Currently, no faulty/orphaned activated features that require a clean up were found during farm load.\n" +
                            "So, currently, all activated features in the farm seem to be clean, meaning, they seem to have a valid definition.";
            }
        }

        protected override string noSpecialFeaturesFoundMessageTitle
        {
            get
            {
                return "No faulty features found";
            }
        }



        public override IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(string searchInput, Scope? selectedScopeFilter)
        {
            return repository.SearchFeaturesToCleanup(searchInput, selectedScopeFilter);
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CanFilterFeature = ActiveItem != null;
        }

        public override void SpecialAction()
        {
            if (ActiveItem != null && ActiveItem.ActivatedFeature != null)
            {
                var feature = ActiveItem.ActivatedFeature;
                var features = new ActivatedFeature[] { feature };
                eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeactivateFeaturesRequest(features));
            }

        }

        public override void SpecialActionFarm()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeactivateFeaturesRequest(specialActionableFeaturesInFarm));
        }

        public override void SpecialActionFiltered()
        {
            var features = Items.Select(sf => sf.ActivatedFeature);
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeactivateFeaturesRequest(features));
        }

        protected override void FilterResults()
        {
            var searchResult = repository.SearchFeaturesToCleanup(searchInput, SelectedScopeFilter);

            ShowResults(searchResult);
        }
    }
}
