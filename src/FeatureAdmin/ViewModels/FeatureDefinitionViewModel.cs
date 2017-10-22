using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{
    public class FeatureDefinitionViewModel : BaseDetailViewModel<FeatureDefinition>
    {
        public FeatureDefinitionViewModel(IEventAggregator eventAggregator)
         : base(eventAggregator)
        { 
            }

    }
}
