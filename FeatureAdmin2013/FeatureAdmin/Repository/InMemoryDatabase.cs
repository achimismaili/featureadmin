using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Repository
{
    public static class InMemoryDatabase
    {
        public static List<ActivatedFeature> ActivatedFeatures;
        public static List<FeatureDefinition> FeatureDefinitions;
    }
}
