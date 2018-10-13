using System;

namespace FeatureAdmin.Core.Messages.Request
{
    public class LoadFeatureDefinitionQuery : BaseTaskMessage
    {
        public LoadFeatureDefinitionQuery(Guid taskId) : base(taskId)
        {
        }
    }
}
