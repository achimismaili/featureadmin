using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class BaseFeatureToggleCompleted : BaseTaskMessage
    {
        /// <summary>
        /// provides the loaded location including child locations
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReferenceId">where did action take place</param>
        /// <param name="error">was activation successful?</param>
        public BaseFeatureToggleCompleted(
            Guid taskId
            , string locationReferenceId
            ) : base (taskId)
        {
            LocationReferenceId = locationReferenceId;
         }
        
        public string LocationReferenceId { get; }
    }
}
