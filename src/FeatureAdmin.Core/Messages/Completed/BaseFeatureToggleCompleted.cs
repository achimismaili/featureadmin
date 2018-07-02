using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class BaseFeatureToggleCompleted : BaseTaskMessage
    {
        /// <summary>
        /// provides the loaded location including child locations
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="locationReference">where did action take place</param>
        /// <param name="error">was activation successful?</param>
        public BaseFeatureToggleCompleted(
            Guid taskId
            , Guid locationReference
            , string error = null
            ) : base (taskId)
        {
            LocationReference = locationReference;
            Error = error;
         }


        public Guid LocationReference { get; }

        /// <summary>
        /// Only add a string, if action was not successful
        /// </summary>
        public string Error { get; }

    }
}
