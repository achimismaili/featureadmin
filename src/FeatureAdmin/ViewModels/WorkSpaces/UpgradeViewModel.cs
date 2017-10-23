using Caliburn.Micro;
using FeatureAdmin.UIModels;

namespace FeatureAdmin.ViewModels.WorkSpaces
{
    public class UpgradeViewModel : Screen, IWorkSpace
    {
        public UpgradeViewModel()
        {
            DisplayName = "Upgrade";
        }

        public void Show()
        {
            ((IConductor)Parent).ActivateItem(this);
        }
    }
}
