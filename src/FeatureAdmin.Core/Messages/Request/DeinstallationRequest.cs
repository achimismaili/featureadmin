using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public class DeinstallationRequest : BaseTaskMessage
    {
        /// <summary>
        /// request to uninstall a feature definition
        /// </summary>
        /// <param name="featureDefinition">the feature definition</param>
        /// <param name="force">force setting</param>
        /// <param name="elevatedPrivileges">ep setting</param>
        /// <remarks>force and elevated privileges are system wide settings</remarks>
        public DeinstallationRequest([NotNull] FeatureDefinition featureDefinition, bool? force = null, bool? elevatedPrivileges = null)
        {
            FeatureDefinition = featureDefinition;
            ElevatedPrivileges = elevatedPrivileges;
            Force = force;
            TaskId = Guid.NewGuid();

            Title = string.Format("Deinstallation of feature definition '{0}'",
                featureDefinition.DisplayName);
        }

        public bool? ElevatedPrivileges { get; }
        public FeatureDefinition FeatureDefinition { get; }

        // From UI, force and elevated privileges are not required, therefore set to null if not set
        public bool? Force { get; }
    }
}
