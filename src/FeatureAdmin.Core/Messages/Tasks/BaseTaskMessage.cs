using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class BaseTaskMessage
    {
        public BaseTaskMessage(Guid taskId)
        {
            TaskId = taskId;
        }
        public Guid TaskId { get; set; }

    }
}
