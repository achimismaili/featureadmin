using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class CancelMessage
    {
        public CancelMessage(Guid taskId, string cancelationMessage, bool cancelCompleteTask, Exception ex = null)
        {
            TaskId = taskId;
            CancelCompleteTask = cancelCompleteTask;

            if (ex != null && !string.IsNullOrEmpty(ex.Message))
            {
                CancelationMessage = string.Format("{0} - Error: {1}", cancelationMessage, ex.Message);
                LogLevel = Core.Models.Enums.LogLevel.Error;
            }
            else
            {
                LogLevel = Core.Models.Enums.LogLevel.Warning;
                CancelationMessage = cancelationMessage;
            }
        }

        /// <summary>
        /// if set to false, other tasks in taskmanager will not be canceled
        /// </summary>
        public bool CancelCompleteTask { get; set; }

        public Guid TaskId { get; private set; }

        public Core.Models.Enums.LogLevel LogLevel { get; private set; }

        public string CancelationMessage { get; private set; }
    }
}
