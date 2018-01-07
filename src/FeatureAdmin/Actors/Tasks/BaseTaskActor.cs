using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FeatureAdmin.Core.Models.Tasks
{
    public abstract class BaseTaskActor : ReceiveActor
    {
        protected readonly IEventAggregator eventAggregator;
        public BaseTaskActor(IEventAggregator eventAggregator, string title, Guid id)
        {
            this.eventAggregator = eventAggregator;
            Status = TaskStatus.Started;
            Id = id;
            Title = title;
            Start = null;
            End = null;
        }

        public DateTime? End { get; set; }
        public Guid Id { get; private set; }

        public abstract double PercentCompleted { get; }
        public DateTime? Start { get; set; }
        public TaskStatus Status { get; private set; }
        public string Title { get; protected set; }

        protected void SendProgress()
        {
            if (PercentCompleted > 0d && Status == TaskStatus.Started)
            {
                Status = TaskStatus.InProgress;
            }

            if (PercentCompleted >= 1d && Status != TaskStatus.Failed && Status != TaskStatus.Canceled
                && Status != TaskStatus.Completed)
            {
                Status = TaskStatus.Completed;
            }

            var progressMsg = new ProgressMessage(PercentCompleted,Title);
            eventAggregator.PublishOnUIThread(progressMsg);

            if (PercentCompleted != 1d && Start == null)
            {
                Start = DateTime.Now;
                var logMsg = new LogMessage(LogLevel.Information,
                string.Format("Started '{1}' (ID: '{0}')", Id, Title)
                );
                eventAggregator.PublishOnUIThread(logMsg);
            }

            if (PercentCompleted >= 1d && End == null)
            {
                End = DateTime.Now;
                var logEndMsg = new LogMessage(Core.Models.Enums.LogLevel.Information,
                string.Format("Completed {0}", StatusReport)
                );
                eventAggregator.PublishOnUIThread(logEndMsg);

             // as task list ist deleted after restart, no need to delete tasks here
            }

        }

        public abstract string StatusReport { get; }
    }
}
