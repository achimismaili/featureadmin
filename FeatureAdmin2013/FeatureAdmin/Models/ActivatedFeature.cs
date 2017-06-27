using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models
{
    public class ActivatedFeature
    {
        public SPFeature SharePointFeature { get; }
        public SPFeatureDefinition Definition { get; }

        public Guid Id { get; }
        
        FeatureParent Parent { get; }
        
        public string Name { get; }
        public SPFeatureScope Scope { get; }

        public ActivatedFeature(SPFeature spFeature, FeatureParent parent)
        {
            SharePointFeature = spFeature;

            // not good to retrieve the properties from definition, in case of faulty features ...
            if (spFeature != null && spFeature.Definition != null)
            {
                Definition = spFeature.Definition;
                Id = spFeature.DefinitionId;
                Name = spFeature.Definition.Name;
                Scope = spFeature.Definition.Scope;
            }
            Parent = parent;
        }
    }
}
