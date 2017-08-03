using FA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.SharePoint
{
    public interface ILocationsRepository
    {
        FeatureParent Farm { get; }

        List<FeatureParent> GetWebApplicationsAdmin { get; }

        List<FeatureParent> GetWebApplicationsContent { get; }
    }
}
