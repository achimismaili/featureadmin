using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models.Tasks;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class NewTask
    {
        public NewTask(AdminTaskItems task, TaskType taskType)
        {
            Task = task;
            TaskType = taskType;
        }

        public AdminTaskItems Task { get; }
        public TaskType TaskType { get; }
    }
}
