using System;

namespace FeatureAdmin.SharePoint2013.SharePointApi
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
                var featureDefinitions = FarmRead.GetFeatureDefinitionCollection();
                var featureDefinitionsBefore = featureDefinitions.Count;
#if (SP2013)
                {
                    featureDefinitions.Remove(id, compatibilityLevel, force);
                }
#else
            {
                featureDefinitions.Remove(id, force);
            }
#endif
                processingCounter = featureDefinitionsBefore - featureDefinitions.Count;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processingCounter;
        }
    }
}
