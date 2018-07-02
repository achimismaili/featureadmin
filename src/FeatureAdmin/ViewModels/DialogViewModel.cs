using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models;
using Caliburn.Micro;
using System.Windows;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class DialogViewModel : IHaveDisplayName
    {
        private readonly IEventAggregator eventAggregator;

        private readonly Guid taskId;
        public DialogViewModel(IEventAggregator eventAggregator, ConfirmationRequest confirmationRequest)
        {
            this.eventAggregator = eventAggregator;
            this.taskId = confirmationRequest.TaskId;
            DisplayName = string.Format("Confirmation for {0}", confirmationRequest.Title);
            DialogText = confirmationRequest.DialogText;
        }

        public string DialogText { get; private set; }

        public string DisplayName { get; set; }

        public void Ok()
        {            
            var confirmationMessage = new Confirmation(taskId);
            eventAggregator.PublishOnUIThread(confirmationMessage);
        }

        public void Cancel()
        {
            var log = new LogMessage(LogLevel.Information, string.Format("'{0}' canceled.", DisplayName) );
            eventAggregator.PublishOnUIThread(log);
        }
    }
}
