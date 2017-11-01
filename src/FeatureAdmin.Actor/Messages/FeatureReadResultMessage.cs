using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Messages
{
    public class FeatureReadResultMessage
    {
        public IEnumerable<ActivatedFeature> ActivatedFeatures { get; set; }
        public IEnumerable<FeatureDefinition> FeatureDefinitions { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
