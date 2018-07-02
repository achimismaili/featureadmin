using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class ConfirmationRequest
    {
        public ConfirmationRequest(string title, string dialogText, Guid taskId)
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
