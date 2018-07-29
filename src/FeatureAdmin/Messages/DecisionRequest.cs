using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class DecisionRequest
    {
        /// <summary>
        /// Creates a message for DialogViewModel to respond to with yes, no or cancel
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dialogText"></param>
        /// <param name="taskId">the task Id</param>
        public DecisionRequest(string title, string dialogText, Guid taskId)
        {
            Title = title;
            DialogText = dialogText;
            TaskId = taskId;
        }

        public string Title { get; }
        public string DialogText { get; }
        public Guid TaskId { get; }
    }
}
