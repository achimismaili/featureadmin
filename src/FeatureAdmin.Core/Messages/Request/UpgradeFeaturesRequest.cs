using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Messages.Request
{
    public class UpgradeFeaturesRequest : BaseTaskMessage
    {
        /// <summary>
        /// request to deactivate one or more activated features
        /// </summary>
        /// <param name="features">the activated features to deactivate</param>
        /// <param name="force">if to deactivate with force</param>
        /// <param name="elevatedPrivileges">if to deactivate with elevated privileges</param>
        public UpgradeFeaturesRequest([NotNull] IEnumerable<ActivatedFeatureSpecial> features, bool? force = null, bool? elevatedPrivileges = null)
        {
            Features = features;
            Force = force;
            ElevatedPrivileges = elevatedPrivileges;
            TaskId = Guid.NewGuid();

            int featureCount = features.Count();

            if (featureCount > 0)
            {
                var firstFeature = features.First();
                var firstFeatureName = firstFeature.ActivatedFeature.DisplayName;
                var locationId = firstFeature.ActivatedFeature.LocationId;
                string version;

                var definition = firstFeature.ActivatedFeature.Definition;

                if (definition != null && definition.Version != firstFeature.ActivatedFeature.Version)
                {
                    version = string.Format(
                        " from version {0} to {1}",
                        firstFeature.ActivatedFeature.Version,
                        firstFeature.ActivatedFeature.Definition.Version
                        );
                }
                else
                {
                    version = string.Empty;
                }

                string locationUrl;

                if (firstFeature.Location != null)
                {
                    locationUrl = firstFeature.Location.Url;
                }
                else
                {
                    locationUrl = "locationId " + firstFeature.ActivatedFeature.LocationId.ToString();
                }


                Title = string.Format(
                "Feature upgrade of {0} feature(s), first one is '{1}' at '{2}' {3}",
                featureCount,
                firstFeatureName,
                firstFeature.Location.Url,
                version
                );
            }
            else
            {
                Title = "Feature upgrade with no features selected";
            }
        }

        public IEnumerable<ActivatedFeatureSpecial> Features { get; }

        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? Force { get; }
        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? ElevatedPrivileges { get; }
    }
}
