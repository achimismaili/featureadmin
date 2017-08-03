using FA.Models.Interfaces;
using FA.UI.BaseClasses;

namespace FA.UI.Locations
{
    public class LocationViewModel: ViewModelBase,  ILocationViewModel
    {
        public LocationViewModel(IFeatureParent location)
        {
            Location = location;
        }

        public IFeatureParent Location { get; private set; }
    }
}
