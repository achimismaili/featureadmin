using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Messages
{
    public class FeatureActivationMessage
    {
        public IEnumerable<FeatureDefinition> FeatureDefinitions { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
