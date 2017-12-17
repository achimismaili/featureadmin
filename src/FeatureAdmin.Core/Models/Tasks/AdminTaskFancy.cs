using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class AdminTaskFancy
    {
        private List<String> log;
        public AdminTaskFancy(TaskType taskType, string title)
        {
            log = new List<string>();
            TaskType = taskType;
            Title = title;
            Status = TaskStatus.Started;
            Id = new Guid();
            Start = DateTime.Now;
            End = null;
            ManualPercentCompleted = 0;

        }

        public DateTime? End { get; set; }
        public int ProcessedFarms { get; set; }
        public int ProcessedFeatures { get; set; }
        public Guid Id { get; private set; }
        public ReadOnlyCollection<string> Log
        {
            get
            {
                return log.AsReadOnly();
            }
        }

        public int TotalWebApps { get; set; }
        public int TotalWebs { get; set; }
        public int TotalSites { get; set; }

        public int TotalFarms { get; set; }
        public int TotalFeatures { get; set; }

        public int TotalItems
        {
            get
            {
                return TotalFeatures + TotalFarms + TotalWebApps + TotalSites + TotalWebs;
            }
        }
        public int ProcessedItems { get {
                return ProcessedFeatures + ProcessedFarms + ProcessedWebApps + ProcessedSites + ProcessedWebs;
            } }

        public double PercentCompleted
        {
            get
            {
                if (TotalItems == 0)
                {
                    return ManualPercentCompleted;
                }
                else
                {
                    if (TotalFeatures > 0 && TotalFeatures == TotalItems)
                    {
                        return ProcessedFeatures / TotalFeatures;
                    }

                    var proc = ProcessedWebs;
                    var total = TotalWebs;

                    if (TotalWebs > 0 && TotalWebs == TotalItems)
                    {
                        return proc / total;

                    }
                    if (TotalSites > 0)
                    {
                        proc = proc / 10 + ProcessedSites;
                        total = total / 10 + TotalSites;
                        if (TotalSites > 0 && TotalSites == TotalItems)
                        {
                            return proc / total;

                        }

                    }

                    if (TotalWebApps > 0)
                    {
                        proc = proc / 10 + ProcessedWebApps;
                        total = total / 10 + TotalWebApps;
                        if (TotalWebApps > 0 && TotalWebApps == TotalItems)
                        {
                            return proc / total;

                        }

                    }

                    if (TotalFarms > 0)
                    {
                        proc = proc / 10 + ProcessedFarms;
                        total = total / 10 + TotalFarms;
                    }

                    return proc / total;

                }
            }
        }

        public double ManualPercentCompleted { get; private set; }
        public int ProcessedSites { get; set; }
        public DateTime Start { get; set; }
        public TaskStatus Status { get; private set; }
        public TaskType TaskType { get; set; }
        public string Title { get; private set; }
        public int ProcessedWebApps { get; set; }
        public int ProcessedWebs { get; set; }
        public void CompleteTask(string logEntry)
        {
            UpdateTask(logEntry, 1);
        }

        public void TaskFailed(string logEntry)
        {
            Status = TaskStatus.Failed;

            UpdateTask(logEntry, ManualPercentCompleted);
        }


        public void UpdateExpectedItems(int features, int webs, int sites, int webApps, int farms)
        {
            TotalFeatures += features;
            TotalWebs += webs;
            TotalSites += sites;
            TotalWebApps += webApps;
            TotalFarms += farms;
        }


        public void UpdateProcessedItems(int features, int webs, int sites, int webApps, int farms)
        {
            ProcessedFeatures += features;
            ProcessedWebs += webs;
            ProcessedSites += sites;
            ProcessedWebApps += webApps;
            ProcessedFarms += farms;
        }

        public void UpdateTask(string logEntry, double percentage)
        {
            ManualPercentCompleted = percentage;
            if (percentage == 1)
            {
                if (Status != TaskStatus.Failed)
                {
                    Status = TaskStatus.Completed;
                }

                // set end date only the first time, end is set
                if (End == null)
                {
                    End = DateTime.Now;
                }
            }
            UpdateTask(logEntry);
        }

        public void UpdateTask(string logEntry)
        {
            if (Status == TaskStatus.Started)
            {
                Status = TaskStatus.InProgress;
            }
            if (!string.IsNullOrEmpty(logEntry))
            {
                log.Add(logEntry);
            }
        }
    }
}
