using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Messages
{
    public class ClearItems : Core.Messages.Tasks.BaseTaskMessage
    {
        public ClearItems(Guid taskId) : base(taskId)
        {
        }
    }
}
