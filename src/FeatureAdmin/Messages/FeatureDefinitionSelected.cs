using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class FeatureDefinitionSelected
    {
        public FeatureDefinition FeatureDefinition { get; }

        public FeatureDefinitionSelected(FeatureDefinition featureDefinition)
        {
            this.FeatureDefinition = featureDefinition;
        }
    }
}
