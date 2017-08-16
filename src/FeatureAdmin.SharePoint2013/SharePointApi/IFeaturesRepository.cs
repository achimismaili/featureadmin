using FA.Models;
using FA.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePoint2013.SharePointApi
{
    public interface IFeaturesRepository
    {
        List<IFeatureDefinition> FeatureDefiniitions { get; }
    }
}
