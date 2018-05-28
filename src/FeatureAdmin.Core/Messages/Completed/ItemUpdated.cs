using System;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class ItemUpdated<T> : BaseTaskMessage where T : class
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
