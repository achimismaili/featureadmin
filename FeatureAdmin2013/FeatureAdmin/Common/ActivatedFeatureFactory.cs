using FeatureAdmin.Models;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class ActivatedFeatureFactory
    {
        public static ActivatedFeature MapSpFeatureToActivatedFeature(SPFeature feature, FeatureParent parent)
        {
            var af = new ActivatedFeature(feature, parent);

            return af;
        }
    }
}
