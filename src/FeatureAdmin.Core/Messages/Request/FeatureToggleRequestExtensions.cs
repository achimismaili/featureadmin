using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public static class FeatureToggleRequestExtensions
    {
        /// <summary>
        /// extension method to get request with changed force and elevated settings
        /// </summary>
        /// <param name="requestToBeUpdated">the request to be updated</param>
        /// <param name="force">new force setting</param>
        /// <param name="elevatedPrivileges">new ep setting</param>
        public static FeatureToggleRequest GetFeatureToggleRequest(this FeatureToggleRequest requestToBeUpdated, bool force, bool elevatedPrivileges)
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
    }
}
