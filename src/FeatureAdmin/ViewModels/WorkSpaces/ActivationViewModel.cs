using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class ActivationViewModel : Screen, IWorkSpace
    {
        public ActivationViewModel()
        {
            DisplayName = "Activation";
        }

        protected override void OnInitialize()
        {

            FeatureDefinitionListVm = ((AppViewModel)Parent).FeatureDefinitionListVm;
            LocationListVm = ((AppViewModel)Parent).LocationListVm;
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }
        public LocationListViewModel LocationListVm { get; private set; }
    }
}
