using FA.Models.Interfaces;

namespace FA.UI.Features
{
    public interface IFeatureViewModel
    {
        IFeatureDefinition Feature { get; }
    }
}
