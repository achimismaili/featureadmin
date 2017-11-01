using FA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePoint2013.SharePointApi
{
    public interface ILocationsRepository
    {
        FeatureParent Farm { get; }

        List<FeatureParent> GetWebApplicationsAdmin { get; }

        List<FeatureParent> GetWebApplicationsContent { get; }
    }
}
