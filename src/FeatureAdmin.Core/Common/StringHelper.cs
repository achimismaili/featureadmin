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

            string guidAsString;

            if (uniqueId.Contains(Core.Common.Constants.MagicStrings.GuidSeparator))
            {
                string[] guids = uniqueId.Split(Core.Common.Constants.MagicStrings.GuidSeparator);
                guidAsString = guids[0];
            }
            else
            {
                guidAsString = uniqueId;
            }

            if (IsGuid(guidAsString))
            {
                return new Guid(guidAsString);
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// checks if string is a guid
        /// </summary>
        /// <param name="possibleGuid"></param>
        /// <returns>true if string is a guid</returns>
        /// <remarks>
        /// Once downgraded to .net 3.5, Guid.TryParse is no longer available
        /// see https://stackoverflow.com/questions/1688624/is-there-a-guid-tryparse-in-net-3-5 for this workaround
        /// </remarks>
        public static bool IsGuid(string possibleGuid)
        {
            try
            {
                Guid gid = new Guid(possibleGuid);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
