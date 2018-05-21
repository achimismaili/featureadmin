using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages.Tasks
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
