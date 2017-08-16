using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class StringHelpers
    {
        public static string ConvertScopeToAbbreviation(SPFeatureScope scope)
        {
            switch (scope)
            {
                case SPFeatureScope.Farm: return "Farm";
                case SPFeatureScope.WebApplication: return "WebApp";
                case SPFeatureScope.Site: return "SiteColl";
                case SPFeatureScope.Web: return "Web";
                default: return "Invalid";
            }
        }
        public static SPFeatureScope ConvertScopeAbbreviationToScope(string scope)
        {
            switch (scope)
            {
                case "Farm": return SPFeatureScope.Farm;
                case "WebApp": return SPFeatureScope.WebApplication;
                case "SiteColl": return SPFeatureScope.Site;
                case "Web": return SPFeatureScope.Web;
                default: return SPFeatureScope.ScopeInvalid;
            }
        }

        public static String SerializeException(Exception exc)
        {
            return SerializeException(exc, "==++==");
        }
        public static String SerializeException(Exception exc, string sep)
        {
            StringBuilder text = new StringBuilder();
            while (exc != null)
            {
                if (text.Length > 0) text.Append(sep);
                text.Append(exc.Message);
                exc = exc.InnerException;
            }
            return text.ToString();
        }
    }
}
