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
            public const string AppDisplayNamePrefix = "Feature Admin 3 for SharePoint ";
            public static readonly string AppDisplayName2019 = $"{AppDisplayNamePrefix} 2019";
            public static readonly string AppDisplayName2016 = $"{AppDisplayNamePrefix} 2016+";
            public static readonly string AppDisplayName2013 = $"{AppDisplayNamePrefix} 2013+";
            public static readonly string AppDisplayName2010 = $"{AppDisplayNamePrefix} 2010";
            public static readonly string AppDisplayName2007 = $"{AppDisplayNamePrefix} 2007";
            public static readonly string AppDisplayNameDemo = $"{AppDisplayNamePrefix} - DEMO MODE";

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
