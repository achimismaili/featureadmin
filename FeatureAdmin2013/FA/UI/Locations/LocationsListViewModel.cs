using FA.UI.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI.Locations
{
    public class LocationsListViewModel : ViewModelBase, ILocationsListViewModel
    {
        public void Load()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<ILocationViewModel> Locations { get; private set; }
    }
}
