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
        public LoadChildLocationQuery(Guid taskId, Location location)
            : base(taskId)
        {
            Location = location;
        }

        /// <summary>
        /// parent location to load children, null for farm load
        /// </summary>
        public Location Location { get; private set; }
    }
}
