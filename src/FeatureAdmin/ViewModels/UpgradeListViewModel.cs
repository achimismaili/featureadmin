using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Repository;
using System.Linq;
using FeatureAdmin.Core.Models.Enums;
using System.Collections.Generic;

namespace FeatureAdmin.ViewModels
{
    public sealed class UpgradeListViewModel : BaseSpecialFeaturesListViewModel
    {
        public UpgradeListViewModel(IEventAggregator eventAggregator, IFeatureRepository repository)
            : base(eventAggregator, repository)
        {
            DisplayName = "Upgrade";
        }

        protected override string noSpecialFeaturesFoundMessageBody
        {
            get
            {
                return "No activated features were found during farm load that require an upgrade.\n" +
                        "All activated features in the farm seem to be up to date.";
            }
        }

        protected override string noSpecialFeaturesFoundMessageTitle
        {
            get
            {
                return "No features found for upgrade";
            }
        }
        public override IEnumerable<ActivatedFeatureSpecial> SearchSpecialFeatures(string searchInput, Scope? selectedScopeFilter)
        {
            return repository.SearchFeaturesToUpgrade(searchInput, selectedScopeFilter);
        }

        public override void SpecialAction()
        {
            if (ActiveItem != null && ActiveItem.ActivatedFeature != null)
            {
                var feature = ActiveItem.ActivatedFeature;
                var features = new ActivatedFeature[] { feature };
                eventAggregator.PublishOnUIThread(new Core.Messages.Request.UpgradeFeaturesRequest(features));
            }

        }

        public override void SpecialActionFarm()
        {
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.UpgradeFeaturesRequest(specialActionableFeaturesInFarm));
        }

        public override void SpecialActionFiltered()
        {
            var features = Items.Select(sf => sf.ActivatedFeature);
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.UpgradeFeaturesRequest(features));
        }
    }
}
