using FeatureAdmin.Core.Models;
using FeatureAdmin.ViewModels;

namespace FeatureAdmin.Messages
{
    public class OpenWindow<T> where T : class
    {
        
        public OpenWindow(T viewModel)
        {
            ViewModel = viewModel;
        }

        public T ViewModel { get; private set; }
    }
}
