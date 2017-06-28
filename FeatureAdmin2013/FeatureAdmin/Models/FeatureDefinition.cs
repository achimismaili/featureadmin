using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models
{
    public class FeatureDefinition
    {
        public SPFeatureDefinition Definition { get; }

        public Guid Id { get; }

        public SPFeatureScope Scope { get; }

        List<ActivatedFeature> ActivatedFeatures;

        public FeatureDefinition(SPFeatureDefinition definition) 
            :this(definition, null)
        {

        }

        public FeatureDefinition(SPFeatureDefinition definition, 
            IEnumerable<ActivatedFeature> activatedFeatures)
        {
            Definition = definition;
            if(definition != null)
            {
                Scope = definition.Scope;
            }

            ActivatedFeatures = new List<ActivatedFeature>(activatedFeatures);
        }

    }
}
