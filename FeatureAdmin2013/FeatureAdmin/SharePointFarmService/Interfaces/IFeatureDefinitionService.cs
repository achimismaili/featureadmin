using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService.Interfaces
{
    public interface IFeatureDefinitionService
    {
        SPFeatureDefinition GetFeatureDefinition(Guid id);

        SPFeatureDefinitionCollection GetAllFeatureDefinitions();
    }
}
