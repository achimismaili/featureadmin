using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public class FeatureToggleRequest : Tasks.BaseTaskMessage
    {
        /// <summary>
        /// request to activate or deactivate a feature in a location or below
        /// </summary>
        /// <param name="featureDefinition">the feature definition</param>
        /// <param name="location">the location or top location</param>
        /// <param name="activate">true = activate, false = deactivate</param>
        /// <remarks>force and elevated privileges are system wide settings</remarks>
        public FeatureToggleRequest([NotNull] FeatureDefinition featureDefinition, [NotNull] Location location, bool activate, bool force, bool elevatedPrivileges)
        {
            FeatureDefinition = featureDefinition;
            Location = location;
            Activate = activate;
            TaskId = Guid.NewGuid();

            var activationPrefix = activate ? "" : "de";

            Title = string.Format("Feature {3}activation of feature '{0}' starting at {1} '{2}'"
                , featureDefinition.DisplayName
                , location.Scope.ToString()
                , location.DisplayName
                , activationPrefix);
        }

        /// <summary>
        /// request to activate or deactivate a feature in a location or below 
        /// </summary>
        /// <param name="featureDefinition"></param>
        /// <param name="location"></param>
        /// <param name="activate"></param>
        /// <remarks>
        /// From UI, force and elevated privileges are not required, these must be known by FeatureTaskActor
        /// </remarks>
        public FeatureToggleRequest([NotNull] FeatureDefinition featureDefinition, [NotNull] Location location, bool activate)
            :this(featureDefinition, location, activate, false, false)
        { }
        
        public FeatureDefinition FeatureDefinition { get; }
        public Location Location { get; }
        public bool Activate { get; }
        public bool Force { get;}
        public bool ElevatedPrivileges { get;}
    }
}
