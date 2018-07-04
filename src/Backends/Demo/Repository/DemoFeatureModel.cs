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

            var farmFeatureDefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions();

            Locations = demoLocations.ToDictionary(l => l.Id);
            ActivatedFeatures = SampleData.SampleLocationHierarchy.GetAllActivatedFeatures(demoLocations).ToList();
            FeatureDefinitions = SampleData.StandardFeatureDefinitions.GetAllFeatureDefinitions().ToDictionary(fd => fd.UniqueIdentifier);

            //if (farmFeatureDefinitions != null)
            //{
            //    foreach (FeatureDefinition fd in farmFeatureDefinitions)
            //    {
            //        if (!FeatureDefinitions.ContainsKey(fd.UniqueIdentifier))
            //        {
            //            FeatureDefinitions.Add(fd.UniqueIdentifier, fd);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Double fd is {0}", fd.UniqueIdentifier);
            //        }
            //    }
            //}
        }

    }
}
