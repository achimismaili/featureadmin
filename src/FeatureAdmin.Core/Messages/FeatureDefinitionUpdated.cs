using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages
{
    public class FeatureDefinitionUpdated
    {
        public FeatureDefinitionUpdated(FeatureDefinition featureDefinition)
        {
            FeatureDefinition = featureDefinition;
        }
        
        public FeatureDefinition FeatureDefinition { get; private set; }
    }
}
