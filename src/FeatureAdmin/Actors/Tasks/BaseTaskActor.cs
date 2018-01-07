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
            PercentCompleted = 0d;
        }

        public DateTime? End { get; set; }
        public Guid Id { get; private set; }

        public double PercentCompleted { get; private set; }
        public DateTime? Start { get; set; }
        public TaskStatus Status { get; private set; }
        public string Title { get; protected set; }

        public void SetProgress(double percentage)
        {
            // cannot set progress smaller than previous value
            if (percentage <= PercentCompleted)
            {
                return;
            }
            else
            {
                IncrementProgress(percentage - PercentCompleted);
            }
        }

        public void IncrementProgress(double percentage)
        {
            if (PercentCompleted == 0d && percentage > 0d && Status == TaskStatus.Started )
            {
                Status = TaskStatus.InProgress;
            }

            PercentCompleted += percentage;

            if (PercentCompleted > 1d)
                {
                    PercentCompleted = 1d;
                }

            if (PercentCompleted == 1d && Status != TaskStatus.Failed && Status != TaskStatus.Canceled
                && Status != TaskStatus.Completed)
            {
                    Status = TaskStatus.Completed;
            }
        }

        protected void SendProgress()
        {
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
