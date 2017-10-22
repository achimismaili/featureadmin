using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionViewModel : BaseDetailViewModel, IHandle<FeatureDefinitionSelected>
    {
        public FeatureDefinition FeatureDefinition { get; set; }

        public FeatureDefinitionViewModel(IEventAggregator eventAggregator)
         : base(eventAggregator)
        { 
            }

        public string Name
        {
            get
            {
                return FeatureDefinition == null ? "Please select a Feature" : FeatureDefinition.DisplayName;
            }
        }

        public void Handle(FeatureDefinitionSelected message)
        {
            FeatureDefinition = message.FeatureDefinition;
            ItemSelected = message.FeatureDefinition != null;
        }
    }
}
