using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Messages.Request
{
    public class DeactivateFeaturesRequest : BaseTaskMessage
    {
        /// <summary>
        /// request to deactivate one or more activated features
        /// </summary>
        /// <param name="features">the activated features to deactivate</param>
        /// <param name="force">if to deactivate with force</param>
        /// <param name="elevatedPrivileges">if to deactivate with elevated privileges</param>
        public DeactivateFeaturesRequest([NotNull] IEnumerable <ActivatedFeature> features, bool? force = null, bool? elevatedPrivileges = null)
        {
            Features = features;
            Force = force;
            ElevatedPrivileges = elevatedPrivileges;
            TaskId = Guid.NewGuid();

            int featureCount = features.Count();

            if (featureCount > 0)
            {
                var firstFeature = features.First();
                var firstFeatureName = firstFeature.DisplayName;
                var locationId = firstFeature.LocationId;
                string version;

                if (firstFeature.Definition != null)
                {
                    version = string.Format(
                        " from version {0} to {1}",
                        firstFeature.Version,
                        firstFeature.Definition.Version
                        );
                }
                else
                {
                    version = string.Empty;
                }


                Title = string.Format(
                "Feature deactivation of {0} feature(s), first one is '{1}' at location id '{2}' {3}",
                featureCount,
                firstFeatureName,
                locationId,
                version
                );
            }
            else
            {
                Title = "Feature upgrade with no features selected";
            }
        }
        
        public IEnumerable<ActivatedFeature> Features { get; }
       
        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? Force { get; }
        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? ElevatedPrivileges { get; }
    }
}
