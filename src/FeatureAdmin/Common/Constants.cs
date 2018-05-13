using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class Constants
    {
        public static class Search
        {
            public static IEnumerable<Scope> ScopeFilterList = new List<Scope>()
            {
                Scope.Farm,
                Scope.WebApplication,
                Scope.Site,
                Scope.Web,
                Scope.ScopeInvalid
            };
        }

        public static class Tasks
        {
            public static string TaskTitleReload = "Reload farm features and locations";
            public static string TaskTitleInitialLoad = "Initial farm load";


        }
            public static class Text
        {
            public static string FeatureAdminTitle = string.Format(
            "FeatureAdmin for SharePoint {0} - v{1}",
                Common.Constants.SharePointVersion,
                version);

            public const string UndefinedActivatedFeature = "Faulty - Id: {0}";

            // warning, when no feature was selected
            public const string NOFEATURESELECTED = "No feature selected. Please select at least 1 feature.";

        }
        public static class PropertyNames
        {
            public const string Activations = "Activations";
            public const string UpgradesRequired = "UpgradesRequired";
        }

        public static Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

#if (SP2013)
        public static string SharePointVersion = "2013";
#elif (SP2010)
        public static string SharePointVersion = "2010";
#else
        public static string SharePointVersion = "Demo";
#endif
    }
}
