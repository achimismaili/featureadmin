using FeatureAdmin.Models;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Repository
{
    public class AdminRepository
    {
        private SharePointFarmService.SharePointDataBase db;

        public AdminRepository()
        {
            db = SharePointFarmService.SharePointDataBase.SingletonInstance;
        }

        public List<FeatureDefinition> GetFeatureDefinitions (SPFeatureScope? scope = null)
        {
            if(scope == null)
            {
                return db.FeatureDefinitions;
            }
            else
            {
                return db.FeatureDefinitions.Where(fd => fd.Scope == scope.Value).ToList();
            }
        }

        /// <summary>
        /// Gets the Web Applications
        /// </summary>
        /// <returns></returns>
        public List<FeatureParent> GetSharePointWebApplications()
        {
            return GetSharePointChildHierarchy(db.FarmId);
        }

        public List<FeatureParent> GetSharePointChildHierarchy(Guid containerId)
        {
            if(db.SharePointParentHierarchy.ContainsKey(db.FarmId))
            {
                return db.SharePointParentHierarchy[containerId];
            }
            else
            {
                return null;
            }
        }
    }
}
