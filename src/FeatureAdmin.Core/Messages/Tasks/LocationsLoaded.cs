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
        public LocationsLoaded(Guid taskId, [NotNull] IEnumerable<Location> locations)
            : base (taskId)
        {
            Locations = locations;

            LoadedFeatures = locations.SelectMany(l => l.ActivatedFeatures).GroupBy(f => f.Definition);
         }
        public IEnumerable<Location> Locations { get; private set; }

        public IEnumerable<IGrouping<FeatureDefinition,ActivatedFeature>> LoadedFeatures { get; private set; }
        
    }
}
