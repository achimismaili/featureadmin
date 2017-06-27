using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class Constants
    {
        public static class Text
        {
            // warning, when no feature was selected
            public const string NOFEATURESELECTED = "No feature selected. Please select at least 1 feature.";

            // defines, how the format of the log time
            public static string DATETIMEFORMAT = "yyyy/MM/dd HH:mm:ss";
            // prefix for log entries: Environment.NewLine + DateTime.Now.ToString(DATETIMEFORMAT) + " - "; 

        }
        public static class PropertyNames
        {
            public const string Activations = "Activations";
            public const string UpgradesRequired = "UpgradesRequired";
        }

#if (SP2013)
        public static string SharePointVersion = "2013";
#elif (SP2010)
        public static string SharePointVersion = "2010";
#else
        public static string SharePointVersion = "2007";
#endif
    }
}
