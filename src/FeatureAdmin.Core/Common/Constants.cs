using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Common
{
    public static class Constants
    {
        public static class Defaults
        {
           
        }

        public static class Labels
        {
            public const int FaultyFeatureCompatibilityLevel = 0;
            public const string FaultyFeatureDescription = "Faulty, orphaned feature - no feature definition available";
            public const string FaultyFeatureName = "Faulty, orphaned feature";
            public const string FaultyFeatureUiVersion = "n/a";
            public const string NumberOfActivatedFeatures = "# of active features";
            public const string UniqueLocationId = "UniqueLocationId\n(can contain database id)";
        }
        public static class MagicStrings
        {
            public const char GuidSeparator = '/';
        }
         
    }
}
