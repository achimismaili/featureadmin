using System;

namespace FeatureAdmin
{
    class spver
    {
        #if (SP2007)
            public static string SharePointVersion = "2007";
        #elif (SP2010)
            public static string SharePointVersion = "2010";
        #elif (SP2013)
            public static string SharePointVersion = "2013";
        #else
            public static string SharePointVersion = "????";
        #endif
    }
}
