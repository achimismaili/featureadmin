using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class DeinstallationCompleted : BaseTaskMessage
    {
        public DeinstallationCompleted(
            Guid taskId
            , string definitionUniqueIdentifier
            ) : base (taskId)
        {
            DefinitionUniqueIdentifier = definitionUniqueIdentifier;
         }

        public string DefinitionUniqueIdentifier { get; private set; }
    }
}
