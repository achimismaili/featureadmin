using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages
{
    public class LocationUpdated
    {
        public LocationUpdated(SPLocation spLocation)
        {
            SPLocation = spLocation;
        }
        public Location Location { get
            {
                return SPLocation as Location;
            }
        }

        public SPLocation SPLocation { get; private set; }
    }
}
