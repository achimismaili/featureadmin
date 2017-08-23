using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models;
using Microsoft.SharePoint.Administration;
using FA.Models.Interfaces;

namespace FeatureAdmin.SharePoint2013.SharePointApi
{
    public class FeaturesRepository : IFeaturesRepository
    {
        public List<IFeatureDefinition> FeatureDefiniitions
        {
            get
            {
                var spDefs = FarmRead.GetFeatureDefinitionCollection();

                return FeatureDefinition.GetFeatureDefinition(spDefs);
            }
        }
    }
}
