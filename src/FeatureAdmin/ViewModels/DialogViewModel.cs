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

        private readonly bool Confirmation;

        public DialogViewModel(IEventAggregator eventAggregator, ConfirmationRequest confirmationRequest)
        {
            Confirmation = confirmationRequest.Confirmation;
            this.eventAggregator = eventAggregator;
            this.taskId = confirmationRequest.TaskId;
            DisplayName = string.Format(
                "{0}{1}",
                Confirmation ? "Confirmation for " : string.Empty,
                confirmationRequest.Title);
            DialogText = confirmationRequest.DialogText;
        }

        public string DialogText { get; private set; }

        public string DisplayName { get; set; }

        public void Ok()
        {
            if (Confirmation)
            {
                var confirmationMessage = new Confirmation(taskId);
                eventAggregator.PublishOnUIThread(confirmationMessage);
            }
        }

        public void Cancel()
        {
            if (Confirmation)
            {
                var log = new LogMessage(LogLevel.Information, string.Format("'{0}' canceled.", DisplayName));
                eventAggregator.PublishOnUIThread(log);
            }
        }
    }
}
