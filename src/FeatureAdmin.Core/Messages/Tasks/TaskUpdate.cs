using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class TaskUpdate
    {
        private TaskUpdate(Guid taskId)
        {
            TaskId = taskId;
        }

        public TaskUpdate(Guid taskId, string logEntry, TaskStatus status = TaskStatus.InProgress)
            : this(taskId)
        {
            LogEntry = logEntry;
        }

        public TaskUpdate(Guid taskId, decimal newPercentage, TaskStatus status = TaskStatus.InProgress)
        : this(taskId)
        {
            NewPercentage = newPercentage;
        }

        public TaskUpdate(Guid taskId, int affectedFeatures, int affectedWebs, int affectedSites, int affectedWebApps, int affectedFarms, bool expected = false, TaskStatus status = TaskStatus.InProgress)
        : this(taskId)
        {
            AffectedFeatures = affectedFeatures;
            AffectedWebs = affectedWebs;
            AffectedSites = affectedSites;
            AffectedWebApps = affectedWebApps;
            AffectedFarms = affectedFarms;

            Expected = expected;
        }

        public bool Expected { get; set; } = false;

        public int AffectedFarms { get; set; }
        public int AffectedFeatures { get; set; }
        public int AffectedSites { get; set; }
        public int AffectedWebApps { get; set; }
        public int AffectedWebs { get; set; }
        public string LogEntry { get; private set; }
        public decimal? NewPercentage { get; private set; }
        public Guid TaskId { get; private set; }
    }
}
