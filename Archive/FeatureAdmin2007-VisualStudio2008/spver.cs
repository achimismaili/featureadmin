using System;

namespace FeatureAdmin
{
    class spver
    {
#if (SP2013)
        public static string SharePointVersion = "2013";
#elif (SP2010)
        public static string SharePointVersion = "2010";
#else
        public static string SharePointVersion = "2007";
#endif
    }
}
