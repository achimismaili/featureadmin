using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService.Interfaces
{
    public interface IActivatedFeatureService
    {
        SPFeature GetActivatedFeature(Guid id);

        SPFeatureCollection GetAllActivatedFeatures();
    }
}
