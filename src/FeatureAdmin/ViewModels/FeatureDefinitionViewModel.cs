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

        public void CopyTitle()
        {
            copyToClipBoard(Item.Title);
        }

        public void CopyName()
        {
            copyToClipBoard(Item.DisplayName);
        }

        public void CopyId()
        {
            copyToClipBoard(Item.Id.ToString());
        }

    }
}
