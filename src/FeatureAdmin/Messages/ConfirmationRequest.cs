using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class ConfirmationRequest
    {
        /// <summary>
        /// Creates a message for DialogViewModel
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dialogText"></param>
        /// <param name="taskId">the task Id</param>
        /// <param name="confirmation">if true, confirmation message will be sent back</param>
        public ConfirmationRequest(string title, string dialogText, Guid taskId, bool confirmation = true)
        {
            Title = title;
            DialogText = dialogText;
            TaskId = taskId;
            Confirmation = confirmation;
        }

        /// <summary>
        /// This Dialog message will not send confirmation back
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dialogText"></param>
        public ConfirmationRequest(string title, string dialogText)
            : this(title, dialogText, Guid.NewGuid(), false)
        {
        }

        public bool Confirmation { get; }
        public string Title { get; }
        public string DialogText { get; }
        public Guid TaskId { get; }
    }
}
