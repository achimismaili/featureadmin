using System;
using FeatureAdmin.Core.Models.Enums;
using Caliburn.Micro;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class DialogViewModel : IHaveDisplayName
    {
        private readonly bool Confirmation;
        private readonly IEventAggregator eventAggregator;

        private readonly Guid taskId;
        private string Title;
        public DialogViewModel(IEventAggregator eventAggregator, ConfirmationRequest confirmationRequest)
            : this(
                  eventAggregator,
                  confirmationRequest.TaskId,
                  confirmationRequest.Title,
                  confirmationRequest.DialogText,
                  confirmationRequest.Confirmation,
                  false
                  )
        {
        }

        public DialogViewModel(IEventAggregator eventAggregator, DecisionRequest confirmationRequest)
            : this(
                  eventAggregator,
                  confirmationRequest.TaskId,
                  confirmationRequest.Title,
                  confirmationRequest.DialogText,
                  true, // for a decision, always send a confirmation to get to know the decision ... ;)
                  true
                  )
        {
        }

        private DialogViewModel(
            IEventAggregator eventAggregator,
            Guid taskId,
            string title,
            string dialogText,
            bool confirmation,
            bool decision)
        {
            this.eventAggregator = eventAggregator;

            Confirmation = confirmation;
            Decision = decision;
            NoIsVisible = decision;

            this.taskId = taskId;

            Title = title;
            DialogText = dialogText;
        }

        public string DialogText { get; private set; }
        public string DisplayName
        {
            get
            {

                if (Decision)
                {
                    return string.Format(
                   "Decision for {0}",
                   Title);
                }
                else
                {
                    if (Confirmation)
                    {
                        return string.Format(
                       "Confirmation for {0}",
                       Title);
                    }
                    else
                    {
                        return Title;
                    }
                }

            }
            set
            {
                DisplayName = value;
            }
        }

        public bool NoIsVisible { get; private set; }
        public string OkText
        {
            get
            {
                return Decision ? "Yes" : "OK";
            }
        }

        private bool Decision { get; }
        public void Cancel()
        {
            if (Confirmation)
            {
                var log = new LogMessage(LogLevel.Information, string.Format("'{0}' canceled.", DisplayName));
                eventAggregator.PublishOnUIThread(log);


                var cancelInfo = new CancelMessage(taskId, log.Text, true);
                eventAggregator.PublishOnUIThread(cancelInfo);
            }
        }

        public void No()
        {
            var confirmationMessage = new Confirmation(taskId, false);
            eventAggregator.PublishOnUIThread(confirmationMessage);
        }

        public void Ok()
        {
            if (Decision)
            {
                var confirmationMessage = new Confirmation(taskId, true);
                eventAggregator.PublishOnUIThread(confirmationMessage);
            }
            else if (Confirmation)
            {
                var confirmationMessage = new Confirmation(taskId);
                eventAggregator.PublishOnUIThread(confirmationMessage);
            }
        }
    }
}
