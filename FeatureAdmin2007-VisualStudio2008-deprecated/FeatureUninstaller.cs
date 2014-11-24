using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public static class FeatureUninstaller
    {
        /// <summary>forcefully removes a feature definition from the farm feature definition collection</summary>
        /// <param name="id">Feature Definition ID</param>
        public static void ForceUninstallFeatureDefinition(Guid id, int compatibilityLevel)
        {
            SPFeatureDefinitionCollection featuredefs = SPFarm.Local.FeatureDefinitions;
            #if (SP2013)
            {
                featuredefs.Remove(id, compatibilityLevel, true);
            }
            #elif (SP2010)
            {
                featuredefs.Remove(id, true);
            }
            #elif (SP2007)
            {
                featuredefs.Remove(id, true);
            }
            #else
            {
            throw new Exception("Unspecified SharePoint Version");
            }
            #endif
        }
    }
}
