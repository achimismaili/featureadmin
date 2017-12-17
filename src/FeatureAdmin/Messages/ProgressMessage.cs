using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models.Tasks;
using System;

namespace FeatureAdmin.Messages
{
    public class ProgressMessage
    {
        public ProgressMessage(double progress, string title)
        {
            Progress = progress;
            Title = title;
        }

        public ProgressMessage(AdminTask task)
        {
            Progress = task.PercentCompleted;
            Title = task.Title;
        }

        public double Progress { get; }
        public string Title { get; }

    }
}
