using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class TaskUpdate
    {
        public TaskUpdate(Guid taskId, string logEntry, decimal? newPercentage = null)
        {
            TaskId = taskId;
            LogEntry = logEntry;
            NewPercentage = NewPercentage;
        }

        public TaskUpdate(Guid taskId, int affectedFeatures, int affectedWebs, int affectedSites, int affectedWebApps, int affectedFarms, bool expected = false)
            : this(taskId, null)
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
