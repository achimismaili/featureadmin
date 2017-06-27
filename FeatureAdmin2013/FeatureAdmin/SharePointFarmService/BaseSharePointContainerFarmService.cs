using FeatureAdmin.SharePointFarmService.Interfaces;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public abstract class BaseSharePointContainer : IActivatedFeatureService
    {
        protected SPFeatureCollection featureCollection;

        public SPFeature GetActivatedFeature(Guid Id)
        {
            if (featureCollection != null || Id == Guid.Empty)
            {
                return null;
            }
            return featureCollection.FirstOrDefault(f => f.DefinitionId == Id);
        }

        public SPFeatureCollection GetAllActivatedFeatures()
        {
            if (featureCollection != null)
            {
                return null;
            }

            return featureCollection;
        }
    }
}
