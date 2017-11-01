using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class ActivationViewModel : Screen, IWorkSpace
    {
        private readonly IEventAggregator eventAggregator;

        public ActivationViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "Activation";
            this.eventAggregator = eventAggregator;
        }

        protected override void OnInitialize()
        {
            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator);

            LocationListVm = new LocationListViewModel(eventAggregator);
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }
        public LocationListViewModel LocationListVm { get; private set; }
    }
}
