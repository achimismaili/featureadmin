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

        public string ElapsedTime
        {
            get
            {
                return (End == null ? DateTime.Now : End.Value).Subtract(
                        Start == null ? DateTime.Now : Start.Value).ToString("c");
            }
        }

        public DateTime? End { get; set; }
        public Guid Id { get; private set; }

        public abstract double PercentCompleted { get; }
        public DateTime? Start { get; set; }
        public TaskStatus Status { get; private set; }
        public abstract string StatusReport { get; }
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
                string.Format("{0} {1}", Status.ToString(), StatusReport)
                );
                eventAggregator.PublishOnUIThread(logEndMsg);

                // as task list ist deleted after restart, no need to delete tasks here
            }

            ProgressMessage progressMsg;

            if (PercentCompleted < 1d)
            {
                progressMsg = new ProgressMessage(Id, PercentCompleted, string.Format("'{0}' in progress ({1:F0}%), please wait ...", Title, PercentCompleted * 100));
            }
            else
            {
                progressMsg = new ProgressMessage(Id, PercentCompleted, string.Format("'{0}' completed! Elapsed time: {1}", Title, ElapsedTime));
            }

            eventAggregator.PublishOnUIThread(progressMsg);

        }
    }
}
