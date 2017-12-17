using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class AdminTask
    {
        public AdminTask(string title)
        {
            Title = title;
            Status = TaskStatus.Started;
            Id = new Guid();
            Start = DateTime.Now;
            End = null;
            PercentCompleted = 0;

        }

        public DateTime? End { get; private set; }
        public Guid Id { get; private set; }

        public double PercentCompleted { get; private set; }
        public DateTime Start { get; private set; }
        public TaskStatus Status { get; private set; }
        public string Title { get; private set; }


        public void SetProgress(double percentage)
        {
            // cannot set progress smaller than previous value
            if (percentage <= PercentCompleted)
            {
                return;
            }

            IncrementProgress(PercentCompleted - percentage);
        }

        public void IncrementProgress(double percentage)
        {
            if (PercentCompleted == 0 && percentage > 0 && Status == TaskStatus.Started )
            {
                Status = TaskStatus.InProgress;
            }

            PercentCompleted += percentage;

            if (PercentCompleted >= 1)
            {
                if (PercentCompleted > 1)
                {
                    PercentCompleted = 1;
                }
                
                if (Status != TaskStatus.Failed || Status != TaskStatus.Canceled)
                {
                    Status = TaskStatus.Completed;
                }

                // set end date only the first time, end is set
                if (End == null)
                {
                    End = DateTime.Now;
                }
            }
        }
   }
}
