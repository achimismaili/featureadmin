using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class UpgradeViewModel : Screen, IWorkSpace
    {
        private IEventAggregator eventAggregator;
        public UpgradeViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "Upgrade";
            this.eventAggregator = eventAggregator;
        }

        protected override void OnInitialize()
        {
            //FeatureDefinitionVm = ((AppViewModel)Parent).FeatureDefinitionVm;
            //FeatureDefinitionListVm = ((AppViewModel)Parent).FeatureDefinitionListVm;
            //LocationVm = ((AppViewModel)Parent).LocationVm;
            //LocationListVm = ((AppViewModel)Parent).LocationListVm;
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }

        //public FeatureDefinitionViewModel FeatureDefinitionVm { get; private set; }

        //public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        //public LocationViewModel LocationVm { get; private set; }
        //public LocationListViewModel LocationListVm { get; private set; }
    }
}