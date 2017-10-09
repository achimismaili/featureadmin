using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Messages
{
    public class LoadLocationMessage : Location
    {
        public LoadLocationMessage(Location location)
        {
            Location = location;
        }
        public Location Location { get; private set; }
    }
}
