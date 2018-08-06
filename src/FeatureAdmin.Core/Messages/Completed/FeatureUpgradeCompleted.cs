using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class FeatureUpgradeCompleted : BaseFeatureToggleCompleted
    {
        /// <summary>
        /// provides the information for the upgraded feature
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReferenceId">where did action take place</param>
        /// <param name="upgradedFeature">the upgraded feature</param>
        /// <param name="error">was activation successful?</param>
        public FeatureUpgradeCompleted(
            Guid taskId
            , string locationReferenceId
            , ActivatedFeature upgradedFeature
            )

            : base(taskId, locationReferenceId)
        {
            UpgradedFeature = upgradedFeature;

        }

        public ActivatedFeature UpgradedFeature { get; }
    }
}
