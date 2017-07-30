using System.ComponentModel;

namespace FeatureAdmin3.UI.Common
{
    public interface IBindableBase
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}