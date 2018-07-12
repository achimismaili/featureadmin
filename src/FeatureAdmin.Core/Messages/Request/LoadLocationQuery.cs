using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Core.Messages.Request
{
    public class LoadChildLocationQuery : BaseTaskMessage
    {
        /// <summary>
        /// Loads Child Locations
        /// </summary>
        /// <param name="taskId">the task id for this load request</param>
        /// <param name="location">the parent location, null for farm load</param>
        public LoadChildLocationQuery(Guid taskId, Location location, bool? elevatedPrivileges)
            : base(taskId)
        {
            Location = location;
            ElevatedPrivileges = elevatedPrivileges.HasValue ? elevatedPrivileges.Value : false;
        }

        /// <summary>
        /// parent location to load children, null for farm load
        /// </summary>
        public Location Location { get; private set; }

        public bool ElevatedPrivileges { get; private set; }
    }
}
