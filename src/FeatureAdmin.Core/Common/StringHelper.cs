using FeatureAdmin.Core.Models.Enums;
using System;
using System.Linq;

namespace FeatureAdmin.Core.Common
{
    public static class StringHelper
    {
        /// <summary>
        /// converts Unique Id to guid
        /// </summary>
        /// <param name="uniqueId">the unique ID starting with a guid</param>
        /// <returns>the first part of a string that is a guid as guid</returns>
        /// <remarks>
        /// if the location Id contains also database guid, only first part as guid is returned
        /// if unique feature definition guid is provided as parameter, first part is feature id as guid
        /// see also https://msdn.microsoft.com/en-us/library/system.guid.tryparse(v=vs.110).aspx
        /// </remarks>
        public static Guid UniqueIdToGuid(string uniqueId)
        {
            if (string.IsNullOrEmpty(uniqueId))
            {
                return Guid.Empty;
            }

            Guid newGuid;

            if (Guid.TryParse(uniqueId, out newGuid))
            { return newGuid; }
            else if (uniqueId.Contains(Core.Common.Constants.MagicStrings.GuidSeparator))
            {
                string[] guids = uniqueId.Split(Core.Common.Constants.MagicStrings.GuidSeparator);
                if (Guid.TryParse(guids[0], out newGuid))
                { return newGuid; }
            }

            return Guid.Empty;
        }




        /// <summary>
        /// Generates a unique Id (proprietary format for feature admin)
        /// </summary>
        /// <param name="featureGuidId">the id of a feature definition or feature as guid</param>
        /// <param name="compatibilityLevel">the compatibility level of the feature definition</param>
        /// <param name="sandBoxedSolutionLocationId">the custom sandboxedSolutionLocationId</param>
        /// <returns></returns>
        public static string GenerateUniqueId(Guid uniqueId, int compatibilityLevel, string sandBoxedSolutionLocationId = null)
        {

            string uniqueIdentifier = uniqueId + Common.Constants.MagicStrings.GuidSeparator.ToString() + compatibilityLevel;
            if (!string.IsNullOrEmpty(sandBoxedSolutionLocationId))
            {
                uniqueIdentifier += Common.Constants.MagicStrings.GuidSeparator.ToString() + sandBoxedSolutionLocationId;
            }
            
            return uniqueIdentifier;
        }

        public static string GetApplicationDisplayName(Backend backend)
        {
            switch (backend)
            {
                case Backend.DEMO:
                    return Constants.Labels.AppDisplayNameDemo;
                case Backend.SP2007:
                    return Constants.Labels.AppDisplayName2007;
                case Backend.SP2010:
                    return Constants.Labels.AppDisplayName2010;
                case Backend.SP2013:
                    return Constants.Labels.AppDisplayName2013;
                case Backend.SP2016:
                    return Constants.Labels.AppDisplayName2016;
                case Backend.SP2019:
                    return Constants.Labels.AppDisplayName2019;
                default:
                    return "Unknown SharePoint Version!";
            }
        }
    }
}
