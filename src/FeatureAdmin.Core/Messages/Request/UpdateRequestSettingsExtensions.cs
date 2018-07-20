using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public static class UpdateRequestSettingsExtensions
    {
        /// <summary>
        /// extension method to get request with changed force and elevated settings
        /// </summary>
        /// <param name="requestToBeUpdated">the request to be updated</param>
        /// <param name="force">new force setting</param>
        /// <param name="elevatedPrivileges">new ep setting</param>
        public static DeactivateFeaturesRequest GetWithUpdatedSettings(this DeactivateFeaturesRequest requestToBeUpdated, bool force, bool elevatedPrivileges)
        {
            var updatedRequest = new DeactivateFeaturesRequest(
                requestToBeUpdated.Features,
                force,
                elevatedPrivileges
                );

            return updatedRequest;
        }

        /// <summary>
        /// extension method to get request with changed force and elevated settings
        /// </summary>
        /// <param name="requestToBeUpdated">the request to be updated</param>
        /// <param name="force">new force setting</param>
        /// <param name="elevatedPrivileges">new ep setting</param>
        public static FeatureToggleRequest GetWithUpdatedSettings(this FeatureToggleRequest requestToBeUpdated, bool force, bool elevatedPrivileges)
        {
            var updatedRequest = new FeatureToggleRequest(
                requestToBeUpdated.FeatureDefinition,
                 requestToBeUpdated.Location,
                 requestToBeUpdated.Activate,
                 force,
                 elevatedPrivileges
                );

            return updatedRequest;
        }

        /// <summary>
        /// extension method to get request with changed elevated settings
        /// </summary>
        /// <param name="requestToBeUpdated">the request to be updated</param>
        /// <param name="elevatedPrivileges">new ep setting</param>
        public static LoadTask GetWithUpdatedSettings(this LoadTask requestToBeUpdated, bool elevatedPrivileges)
        {
            var updatedRequest = new LoadTask(
                requestToBeUpdated.Id,
                requestToBeUpdated.Title,
                requestToBeUpdated.StartLocation,
                elevatedPrivileges);

            return updatedRequest;
        }

        /// <summary>
        /// extension method to get request with changed force and elevated settings
        /// </summary>
        /// <param name="requestToBeUpdated">the request to be updated</param>
        /// <param name="force">new force setting</param>
        /// <param name="elevatedPrivileges">new ep setting</param>
        public static UpgradeFeaturesRequest GetWithUpdatedSettings(this UpgradeFeaturesRequest requestToBeUpdated, bool force, bool elevatedPrivileges)
        {
            var updatedRequest = new UpgradeFeaturesRequest(
                requestToBeUpdated.Features,
                force,
                elevatedPrivileges
                );

            return updatedRequest;
        }
    }
}
