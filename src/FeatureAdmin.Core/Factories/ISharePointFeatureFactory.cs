using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.SharePointFactories
{
    public interface ISharePointFeatureFactory
    {
        Location GetActivatedFeature(SPObject location);

        SPObject GetActivatedFeature(Location location);

        Location GetFeatureDefinition(SPObject location);

        SPObject GetFeatureDefinition(Location location);
    }
}
