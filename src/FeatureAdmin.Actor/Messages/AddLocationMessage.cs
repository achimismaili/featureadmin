using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Messages
{
    public class AddLocationMessage 
    {
        public AddLocationMessage(Location location)
        {
            Location = location;
        }
        public Location Location { get; set; }
    }
}
