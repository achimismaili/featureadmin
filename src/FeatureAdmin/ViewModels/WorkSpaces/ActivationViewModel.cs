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
           
            LocationListVm = ((AppViewModel)Parent).LocationListVm;
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }

        // public LocationViewModel LocationVm { get; private set; }
        public LocationListViewModel LocationListVm { get; private set; }
    }
}
