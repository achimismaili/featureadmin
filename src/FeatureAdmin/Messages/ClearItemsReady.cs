using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Messages
{
    public class ClearItemsReady : Core.Messages.Tasks.BaseTaskMessage
    {
        public ClearItemsReady(Guid taskId) : base(taskId)
        {
        }

    }
}
