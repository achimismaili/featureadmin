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
        /// <param name="loadedChildLocations">loaded child locations</param>
        /// <param name="activatedFeatures">loaded activated features</param>
        /// <param name="definitions">usually feature definitions of sandboxed features</param>
        public LoadedDto(Location parent,
            [NotNull] IEnumerable<Location> loadedChildLocations,
            [NotNull] IEnumerable<ActivatedFeature> activatedFeatures,
            [NotNull] IEnumerable<FeatureDefinition> definitions)
        {
            ChildLocations = new List<Location>(loadedChildLocations);
            Parent = parent;
            ActivatedFeatures = new List<ActivatedFeature>(activatedFeatures);
            Definitions = new List<FeatureDefinition>(definitions);
        }

        public LoadedDto(Location parent)
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            Definitions = new List<FeatureDefinition>();
            ChildLocations = new List<Location>();
            Parent = parent;
        }

        public List<ActivatedFeature> ActivatedFeatures { get; private set; }

        public List<Location> ChildLocations { get; private set; }

        public List<FeatureDefinition> Definitions { get; private set; }

        public Location Parent { get; private set; }

        public void AddActivatedFeatures(IEnumerable<ActivatedFeature> features)
        {
            ActivatedFeatures.AddRange(features);
        }

        public void AddChild(Location location, IEnumerable<ActivatedFeature> activatedFeatures, IEnumerable<FeatureDefinition> definitions)
        {
            ChildLocations.Add(location);
            ActivatedFeatures.AddRange(activatedFeatures);
            Definitions.AddRange(definitions);
        }
        public void AddChildLocations(IEnumerable<Location> childLocations)
        {
            ChildLocations.AddRange(childLocations);
        }

        public void AddChildren(List<Location> childLocations, List<ActivatedFeature> activatedFeatures, List<FeatureDefinition> definitions)
        {
            ChildLocations.AddRange(childLocations);
            ActivatedFeatures.AddRange(activatedFeatures);
            Definitions.AddRange(definitions);
        }
        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> definitions)
        {
            Definitions.AddRange(definitions);
        }
    }
}
