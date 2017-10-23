using Caliburn.Micro;
using FeatureAdmin.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.ViewModels
{
    public class LeftNavViewModel : Screen
    {
        private IEventAggregator eventAggregator;

        private ActivityBaseViewModel selectedActivity;

        public LeftNavViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            Activities = new BindableCollection<ActivityBaseViewModel>
            {
                new ActivationActivityViewModel(),
                new UpgradeActivityViewModel(),
                new TaskActivityViewModel(),
            };

        }

        public BindableCollection<ActivityBaseViewModel> Activities { get; }

        public ActivityBaseViewModel SelectedActivity
        {
            get { return selectedActivity; }
            set { this.Set(ref selectedActivity, value); }
        }

    }
}
