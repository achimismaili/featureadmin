using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Serializable]
    /// <summary>
    /// Data transfer object for loaded locations, activated features and definitions
    /// </summary>
    public class LoadedDto
    {
        /// <summary>
        /// Data transfer object for loaded locations, activated features and definitions
        /// </summary>
        /// <param name="parent">parent location</param>
        /// <param name="loadedLocations">loaded locations</param>
        /// <param name="activatedFeatures">loaded activated features</param>
        /// <param name="definitions">usually feature definitions of sandboxed features</param>
        public LoadedDto(Location parent,
            [NotNull] IEnumerable<Location> loadedLocations,
            [NotNull] IEnumerable<ActivatedFeature> activatedFeatures,
            [NotNull] IEnumerable<FeatureDefinition> definitions)
        {
            this.ChildLocations = loadedLocations;
            Parent = parent;
            ActivatedFeatures = activatedFeatures;
            Definitions = definitions;
        }
        public IEnumerable<ActivatedFeature> ActivatedFeatures { get; private set; }
        public IEnumerable<Location> ChildLocations { get; private set; }

        public Location Parent { get; private set; }
        public IEnumerable<FeatureDefinition> Definitions { get; private set; }

    }
}
