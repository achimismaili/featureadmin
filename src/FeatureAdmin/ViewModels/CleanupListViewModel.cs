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

        protected override void OnActivate()
        {
            var featureDefinitionUndefinedFilter = 
                new Messages.SetSearchFilter<FeatureDefinition>(string.Empty, Scope.ScopeInvalid);

            eventAggregator.PublishOnUIThread(featureDefinitionUndefinedFilter);

            base.OnActivate();
        }

        protected override string noSpecialFeaturesFoundMessageBody
        {
            get
            {
                return "Please make sure, farm load is already completed (see progress bar in the footer).\n" +
                            "Currently, no faulty/orphaned activated features that require a clean up were found during farm load.\n" +
                            "So, currently, all activated features in the farm seem to be clean, meaning, they seem to have a valid definition." + 
                            "Nevertheless, if orphaned feature definitions exist by checking the scope-filter 'undefined' on the left window.\n" +
                            "This filter is always set when swithing to the cleanup view.\n" +
                            "If after a reload no faulty activated features were found, but 'undefined' feature definitions show up on the left, " +
                            "it is usually recommended to uninstall them.";
            }
        }

        protected override string noSpecialFeaturesFoundMessageTitle
        {
            get
            {
                return "No faulty features found";
            }
        }



        public override IEnumerable<ActivatedFeatureSpecial> GetAllSpecialFeatures()
        {


            return repository.GetAllFeaturesToCleanUp();
        }

        public override void SelectionChanged()
        {
            SelectionChangedBase();
            CanFilterFeature = ActiveItem != null;
        }

        protected override void PublishSpecialActionRequest(IEnumerable<ActivatedFeatureSpecial> features)
        {
            // faulty feature cleanup always requires 'force'
            eventAggregator.PublishOnUIThread(new Core.Messages.Request.DeactivateFeaturesRequest(features, true));
        }
    }
}
