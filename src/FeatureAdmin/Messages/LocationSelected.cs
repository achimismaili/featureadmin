using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class LocationSelected
    {
        public Location Location { get; }

        public LocationSelected(Location location)
        {
            this.Location = location;
        }
    }
}
