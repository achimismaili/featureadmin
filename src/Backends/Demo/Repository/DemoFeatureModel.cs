using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Demo.Repository
{
    public class DemoFeatureModel : OrigoDb.FeatureModel
    {
        public DemoFeatureModel()
        {
            var demoLocations = SampleData.SampleLocationHierarchy.GetAllLocations();

            Locations = demoLocations.ToDictionary(l => l.Id);
            ActivatedFeatures = SampleData.SampleLocationHierarchy.GetAllActivatedFeatures(demoLocations).ToList();
            FeatureDefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions().ToDictionary(fd => fd.UniqueIdentifier);
        }

    }
}
