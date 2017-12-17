using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models.Tasks;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class NewTask
    {
        public NewTask(AdminTask task, TaskType taskType)
        {
            Task = task;
            TaskType = TaskType;
        }

        public AdminTask Task { get; }
        public TaskType TaskType { get; }
    }
}
