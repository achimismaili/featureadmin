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
        // to be overwritten by AppBootstrapper.cs with an error message, in case of a start error
        public static string BackendErrorMessage = "There was an error when trying to connect to the SharePoint farm.";

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
            public const string UndefinedActivatedFeature = "Faulty - Id: {0}";

            // warning, when no feature was selected
            public const string NOFEATURESELECTED = "No feature selected. Please select at least 1 feature.";

        }
        public static class PropertyNames
        {
            public const string Activations = "Activations";
            public const string UpgradesRequired = "UpgradesRequired";
        }

        public static Version FeatureAdminVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
    }
}
