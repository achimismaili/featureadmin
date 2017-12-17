using Caliburn.Micro;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.ViewModels
{
    public class NavigationBarViewModel : Screen
    {
        private IEventAggregator eventAggregator;
       
        public NavigationBarViewModel(IEventAggregator eventAggregator)
        {
                this.eventAggregator = eventAggregator;
                this.eventAggregator.Subscribe(this);
            }

        public void ReLoadFarm()
        {
            var reloadTask = new Core.Models.Tasks.AdminTaskItems("Reload farm features and locations", Common.Constants.Tasks.PreparationStepsForLoad);
            eventAggregator.PublishOnUIThread(new Core.Messages.Tasks.NewTask(reloadTask, Core.Models.Enums.TaskType.Load));
        }
    }
}
