using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models.Tasks;
using System;

namespace FeatureAdmin.Messages
{
    public class ProgressMessage
    {
        public ProgressMessage(Guid taskId, double progress, string title)
        {
            TaskId = taskId;
            Progress = progress;
            Title = title;
        }
        public Guid TaskId { get; }
        public double Progress { get; }
        public string Title { get; }

    }
}
