using System;

namespace FeatureAdmin.Core.Messages
{
    public abstract class BaseTaskMessage
    {
        protected BaseTaskMessage()
        {
        }
        protected BaseTaskMessage(Guid taskId)
        {
            TaskId = taskId;
        }
        public Guid TaskId { get; protected set; }

        public string Title { get; protected set; }
    }
}
