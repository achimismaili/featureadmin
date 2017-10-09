using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Messages
{
    public class LoadTaskMessage : Location
    {
        public LoadTaskMessage(Location location)
        {
            Location = location;
           // TaskName = taskName;
        }

      //  public string TaskName { get; set; }
        public Location Location { get; private set; }
    }
}
