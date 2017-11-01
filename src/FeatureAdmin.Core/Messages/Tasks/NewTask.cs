using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class NewTask
    {
        public NewTask(LogTask task)
        {
            Task = task;
        }

        public LogTask Task { get; set; }
    }
}
