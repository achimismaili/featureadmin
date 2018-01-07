using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class LocationsLoaded : Tasks.BaseTaskMessage
    {
        /// <summary>
        /// provides the loaded location including child locations
        /// </summary>
        /// <param name="taskId">reference to task id</param>
        /// <param name="parent">the parent location that was reloaded</param>
        /// <param name="ChildLocations">the child location of the parent location</param>
        public LocationsLoaded(Guid taskId, Location parent, [NotNull] IEnumerable<Location> ChildLocations)
            : base (taskId)
        {
            this.ChildLocations = ChildLocations;
            Parent = parent;
            LoadedFeatures = ChildLocations.SelectMany(l => l.ActivatedFeatures).GroupBy(f => f.Definition);
         }
        public IEnumerable<Location> ChildLocations { get; private set; }

        public Location Parent { get; private set; }

        public IEnumerable<IGrouping<FeatureDefinition,ActivatedFeature>> LoadedFeatures { get; private set; }
        
    }
}
