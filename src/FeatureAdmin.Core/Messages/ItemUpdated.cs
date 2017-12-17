using System;

namespace FeatureAdmin.Core.Messages
{
    public class ItemUpdated<T> : Tasks.BaseTaskMessage where T : class
    {
        public object i;

        public ItemUpdated(Guid taskId, [NotNull] T item, bool reportToTaskManager = false)
            : base(taskId)
        {
            Item = item;
            ReportToTaskManager = reportToTaskManager;
        }

        public T Item { get; private set; }

        public bool ReportToTaskManager { get; private set; }
    }
}
