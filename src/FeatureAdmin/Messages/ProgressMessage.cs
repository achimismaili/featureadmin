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

        public double Progress { get; }
        public string Title { get; }

    }
}
