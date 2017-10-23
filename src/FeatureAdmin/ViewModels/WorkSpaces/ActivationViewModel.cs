using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class ActivationViewModel : Screen, IWorkSpace
    {
        public ActivationViewModel(
            FeatureDefinitionViewModel featureDefinitionVm,
             FeatureDefinitionListViewModel featureDefinitionListVm,
             LocationViewModel locationVm,
             LocationListViewModel locationListVm
            )
        {
            DisplayName = "Activation";

            FeatureDefinitionVm = featureDefinitionVm;
            FeatureDefinitionListVm = featureDefinitionListVm;
            LocationVm = locationVm;
            LocationListVm = locationListVm;
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }

        public FeatureDefinitionViewModel FeatureDefinitionVm { get; private set; }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationViewModel LocationVm { get; private set; }
        public LocationListViewModel LocationListVm { get; private set; }
    }
}
