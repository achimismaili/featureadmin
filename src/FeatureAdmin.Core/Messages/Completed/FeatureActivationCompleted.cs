using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class FeatureActivationCompleted : BaseFeatureToggleCompleted
    {
        /// <summary>
        /// provides the loaded location including child locations
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReference">where did action take place</param>
        /// <param name="activatedFeature">the activated feature</param>
        /// <param name="error">was activation successful?</param>
        public FeatureActivationCompleted(
            Guid taskId
            , Guid locationReference
            , ActivatedFeature activatedFeature
            , string error = null
            )

            : base(taskId, locationReference, error)
        {
            ActivatedFeature = activatedFeature;

        }

        public ActivatedFeature ActivatedFeature { get; }
    }
}
