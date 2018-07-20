using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public class FeatureToggleRequest : BaseTaskMessage
    {
        /// <summary>
        /// request to activate or deactivate a feature in a location or below
        /// </summary>
        /// <param name="featureDefinition">the feature definition</param>
        /// <param name="location">the location or top location</param>
        /// <param name="action">Activate, Deactivate or Upgrade</param>
        /// <remarks>force and elevated privileges are system wide settings</remarks>
        public FeatureToggleRequest([NotNull] FeatureDefinition featureDefinition, [NotNull] Location location, FeatureAction action, bool? force = null, bool? elevatedPrivileges = null)
        {
            FeatureDefinition = featureDefinition;
            Location = location;
            Action = action;
            Force = force;
            ElevatedPrivileges = elevatedPrivileges;
            TaskId = Guid.NewGuid();

            string locationPrefix = FeatureDefinition.Scope < location.Scope ? "across" : "at";

            Title = string.Format("Feature {3} of feature '{0}' {4} {1} '{2}'",
                featureDefinition.DisplayName,
                location.Scope.ToString(),
                location.DisplayName,
                Action.ToString().ToLower(),
                locationPrefix);
        }

        public FeatureDefinition FeatureDefinition { get; }
        public Location Location { get; }
        public FeatureAction Action { get; }
        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? Force { get; }
        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? ElevatedPrivileges { get; }
    }
}
