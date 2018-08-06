using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class FeatureActivationCompleted : BaseFeatureToggleCompleted
    {
        /// <summary>
        /// provides the information for the activated feature
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReferenceId">where did action take place</param>
        /// <param name="activatedFeature">the activated feature</param>
        /// <param name="error">was activation successful?</param>
        public FeatureActivationCompleted(
            Guid taskId
            , string locationReferenceId
            , ActivatedFeature activatedFeature
            )

            : base(taskId, locationReferenceId)
        {
            ActivatedFeature = activatedFeature;

        }

        public ActivatedFeature ActivatedFeature { get; }
    }
}
