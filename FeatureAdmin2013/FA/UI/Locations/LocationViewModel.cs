using FA.Models;
using FA.UI.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI.Locations
{
    public class LocationViewModel: ViewModelBase,  ILocationViewModel
    {



        public FeatureParent Location { get; private set; }
    }
}
