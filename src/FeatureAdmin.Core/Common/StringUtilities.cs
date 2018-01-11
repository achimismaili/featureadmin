using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Common
{
    public static class StringUtilities
    {
        public static string PropertiesToString(Dictionary<string, string> properties)
        {
            var propString = string.Empty;

            if (properties != null)
            {
                foreach (var p in properties)
                {
                    propString += string.Format("'{0}':'{1}'\n", p.Key, p.Value);
                }
            }

            return propString;
        }
    }
}
