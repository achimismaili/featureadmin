using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            eventAggregator.PublishOnUIThread(new LoadFeatureDefinitionCommand());
            eventAggregator.PublishOnUIThread(new LoadLocationCommand(SPLocation.GetDummyFarmForLoadCommand()));
        }
    }
}
