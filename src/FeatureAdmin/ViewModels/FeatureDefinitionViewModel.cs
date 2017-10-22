using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionViewModel : BaseDetailViewModel, IHandle<ItemSelected<FeatureDefinition>>
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

        public void Handle(ItemSelected<FeatureDefinition> message)
        {
            FeatureDefinition = message.Item;
            ItemSelected = message.Item != null;
        }
    }
}
