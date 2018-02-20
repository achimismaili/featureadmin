using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Backends.Sp2013.Services
{
    public static class FeatureDefinitionUninstall
    {

        /// <summary>forcefully removes a feature definition from the farm feature definition collection</summary>
        /// <param name="id">Feature Definition ID</param>
        public static int Uninstall(Guid id, int compatibilityLevel, bool force, out Exception exception)
        {
            var processingCounter = 0;
            exception = null;

            try
            {
                FeatureDefinition featureDefinition = null; // FarmRead.GetFeatureDefinitionCollection();
              //  var featureDefinitionsBefore = featureDefinition.Count;
#if (SP2013)
                {
                    featureDefinitions.Remove(id, compatibilityLevel, force);
                }
#else
            {
               // featureDefinition.Remove(id, force);
            }
#endif
                processingCounter = 0; // featureDefinitionsBefore - featureDefinition.Count;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }
    }
}
