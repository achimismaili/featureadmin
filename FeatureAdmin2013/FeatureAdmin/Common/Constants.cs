using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class Constants
    {
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
