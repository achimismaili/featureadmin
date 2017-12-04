using FeatureAdmin.Core.Models;
using FeatureAdmin.ViewModels;

namespace FeatureAdmin.Messages
{
    public class OpenWindow
    {
        
        public OpenWindow(DetailViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public DetailViewModel ViewModel { get; private set; }
    }
}
