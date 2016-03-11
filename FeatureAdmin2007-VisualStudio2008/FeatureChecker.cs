using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;

namespace FeatureAdmin
{
    class FeatureChecker
    {
        public class Status
        {
            public Guid FeatureId = Guid.Empty;
            public int CompatibilityLevel = 0;
            public string DisplayName = "";
            public bool Faulty = false;
        }
        public Status CheckFeature(SPFeature feature)
        {
            Status status = new Status();
            try
            {
                status.FeatureId = feature.DefinitionId;
            }
            catch
            {
                status.Faulty = true;
            }
            try
            {
                status.CompatibilityLevel = FeatureManager.GetFeatureCompatibilityLevel(feature.Definition);
            }
            catch
            {
                status.Faulty = true;
            }
            try
            {
                // a feature activated somewhere with no manifest file available causes
                // an error when asking for the DisplayName
                // If this happens, we found a faulty feature
                status.DisplayName = feature.Definition.DisplayName;
            }
            catch
            {
                status.Faulty = true;
            }
            return status;
        }
    }
}
