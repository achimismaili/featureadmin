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
        private void ConvertLocationsForFeatureDefinitions([NotNull] IEnumerable<Location> locations)
        {
            var activatedFeatures = locations.SelectMany(af => af.ActivatedFeatures).ToList();

            FaultyFeatures = activatedFeatures.Where(f => f.Faulty);

            ActivatedFarmFeatures = activatedFeatures.Where(f => !f.Faulty && f.DefinitionInstallationScope.Equals(Common.Constants.Defaults.DefinitionInstallationScopeFarm));

            var sandboxedFeatures = activatedFeatures.Where(f => !f.Faulty && !f.DefinitionInstallationScope.Equals(Common.Constants.Defaults.DefinitionInstallationScopeFarm));

            if (sandboxedFeatures.Count() > 0)
            {
                SandboxedFeatureDefinitions = sandboxedFeatures.Select(fd => fd.Definition).Distinct();

                foreach (ActivatedFeature f in sandboxedFeatures)
                {
                    SandboxedFeatureDefinitions.Single(fd => fd == f.Definition).ToggleActivatedFeature(f, true);
                }
            }
            else
            {
                SandboxedFeatureDefinitions = new List<FeatureDefinition>();
            }

        }

        public LocationsLoaded(Guid taskId, [NotNull] IEnumerable<Location> locations)
            : base (taskId)
        {
            Locations = locations;

            ConvertLocationsForFeatureDefinitions(locations);
         }
        public IEnumerable<Location> Locations { get; private set; }

        public IEnumerable<ActivatedFeature> ActivatedFarmFeatures { get; private set; }
        public IEnumerable<ActivatedFeature> FaultyFeatures { get; private set; }
        public IEnumerable<FeatureDefinition> SandboxedFeatureDefinitions { get; private set; }
    }
}
