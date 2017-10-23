using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FeatureAdmin.ViewModels
{
    public class LogLeftNavItemViewModel : LeftNavViewModel
    {
        public LogLeftNavItemViewModel(IEventAggregator eventAggregator) 
            : base(eventAggregator)
        {
            Messages = new BindableCollection<string>();
        }

        protected override void OnInitialize()
        {
            Messages.Add("Initialized");
        }

        protected override void OnActivate()
        {
            Messages.Add("Activated");
        }

        protected override void OnDeactivate(bool close)
        {
            Messages.Add($"Deactivated, close: {close}");
        }

        public BindableCollection<string> Messages { get; }

    }
}
