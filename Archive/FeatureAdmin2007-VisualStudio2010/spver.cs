using System;

namespace FeatureAdmin
{
    class spver
    {
        #if (SP2007)
            public static string SharePointVersion = "2007";
        #endif
        #if (SP2010)
            public static string SharePointVersion = "2010";
        #endif
        #if (SP2013)
            public static string SharePointVersion = "2013";
        #endif
    }
}
