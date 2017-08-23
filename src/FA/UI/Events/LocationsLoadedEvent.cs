using FA.Models;
using FA.Models.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI.Events
{
    public class LocationsLoadedEvent : PubSubEvent<IEnumerable<IFeatureParent>>
    {
    }
}
