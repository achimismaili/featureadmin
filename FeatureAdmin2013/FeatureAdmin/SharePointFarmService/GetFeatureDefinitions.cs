using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using FeatureAdmin.Models;

namespace FeatureAdmin.SharePointFarmService
{
    public static class GetFeatureDefinitions
    {
        
        public static SPFeatureDefinitionCollection GetAllSpFeatureDefinitions()
        {
            return SPFarm.Local.FeatureDefinitions;
        }

        public static List<FeatureDefinition> GetAllFeatureDefinitions()
        {
            var spDefs = GetAllSpFeatureDefinitions();

            return FeatureDefinition.GetFeatureDefinition(spDefs);
        }
    }
}
