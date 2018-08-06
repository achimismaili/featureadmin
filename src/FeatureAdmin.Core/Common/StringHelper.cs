using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
