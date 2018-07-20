using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class FeatureDeactivationCompleted : BaseFeatureToggleCompleted
    {
        /// <summary>
        /// provides the information for the deactivated feature
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReference">where did action take place</param>
        /// <param name="featureId">id of the deactivated feature</param>
        /// <param name="error">was activation successful?</param>
        public FeatureDeactivationCompleted(
            Guid taskId
            , Guid locationReference
            , Guid featureId
            ) : base (taskId, locationReference)
        {
            FeatureId = featureId;
         }

        public Guid FeatureId { get; set; }
    }
}
