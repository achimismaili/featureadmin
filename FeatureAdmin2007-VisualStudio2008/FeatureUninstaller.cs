using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public static class FeatureUninstaller
    {
        public enum Forcibility { None, Regular, Forcible };
        /// <summary>forcefully removes a feature definition from the farm feature definition collection</summary>
        /// <param name="id">Feature Definition ID</param>
        public static void UninstallFeatureDefinition(Guid id, int compatibilityLevel, Forcibility forcibility)
        {
            bool force = (forcibility == Forcibility.Forcible ? true : false);
            SPFeatureDefinitionCollection featuredefs = SPFarm.Local.FeatureDefinitions;
#if (SP2013)
            {
                featuredefs.Remove(id, compatibilityLevel, force);
            }
#else
            {
                featuredefs.Remove(id, force);
            }
#endif
        }
    }
}
