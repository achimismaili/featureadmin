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
        /// <param name="LoadedLocations">the loaded locations of the parent location</param>
        public LocationsLoaded(Guid taskId, Location parent, 
            [NotNull] IEnumerable<Location> LoadedLocations, 
            [NotNull] IEnumerable<ActivatedFeature> activatedFeatures,
            [NotNull] IEnumerable<FeatureDefinition> definitions)
            : base (taskId)
        {
            this.ChildLocations = LoadedLocations;
            Parent = parent;
            ActivatedFeatures = activatedFeatures;
            Definitions = definitions;
         }

        /// <summary>
        /// Generates a LocationsLoaded message with a non relevant task id
        /// </summary>
        /// <param name="parent">parent location</param>
        /// <param name="LoadedLocations">loaded locations</param>
        /// <param name="activatedFeatures">loaded activated features</param>
        /// <param name="definitions">usually feature definitions of sandboxed features</param>
        /// <remarks>
        /// This is useful to leverage these messages internally while collecting locations
        /// </remarks>
        public LocationsLoaded(Location parent,
            [NotNull] IEnumerable<Location> LoadedLocations,
            [NotNull] IEnumerable<ActivatedFeature> activatedFeatures,
            [NotNull] IEnumerable<FeatureDefinition> definitions)
            : this(Guid.Empty, parent, LoadedLocations, activatedFeatures, definitions)
        {
        }

        public IEnumerable<ActivatedFeature> ActivatedFeatures { get; private set; }
        public IEnumerable<Location> ChildLocations { get; private set; }

        public Location Parent { get; private set; }
        public IEnumerable<FeatureDefinition> Definitions { get; private set; }

    }
}
