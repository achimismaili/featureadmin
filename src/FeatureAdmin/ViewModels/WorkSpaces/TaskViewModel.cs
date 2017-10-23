using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class TaskViewModel : Screen, IWorkSpace
    {
        public TaskViewModel()
        {
            DisplayName = "Task";
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }
    }
}
