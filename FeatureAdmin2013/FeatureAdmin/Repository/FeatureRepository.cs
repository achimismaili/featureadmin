using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FeatureAdmin.Models;
using FeatureAdmin.Services.SharePointApi;
using FeatureAdmin.Models.Interfaces;
using Serilog;

namespace FeatureAdmin.Repository
{
    public class FeatureRepository
    {
        private InMemoryDataBase db;

        public FeatureRepository()
        {
            db = InMemoryDataBase.SingletonInstance;
        }

        public bool AlwaysUseForce {
            get
            {
                return InMemoryDataBase.ConfigSettings.AlwaysUseForce;
            }
            set {
                InMemoryDataBase.ConfigSettings.AlwaysUseForce = value;
            } }

        public int ActivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureDefinition> featureDefinitions, bool force)
        {
            Exception ex = null;

            if (featureDefinitions == null)
            {
                Log.Error("No Features selected for activation!");
                return 0;
            }

            if (sharePointContainerLevel == null)
            {
                Log.Error("No Container selected as start level!");
                return 0;
            }

            var definitionsCount = featureDefinitions.Count();
            var activationsCount = FeatureActivationAndDeactivationBulk.ActivateAllFeaturesWithinSharePointContainer(sharePointContainerLevel, featureDefinitions, force, out ex);

            var msg = activationsCount + " features activated from " + definitionsCount + " selected feature definitions starting on level " + sharePointContainerLevel.Scope + " and below";

            if (ex == null)
            {
                Log.Information(msg);
            }
            else
            {
                Log.Warning(msg, ex);
            }

            return activationsCount;
        }

        public int DeactivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureIdentifier> featureDefinitions, bool force)
        {
            Exception ex = null;

            if (featureDefinitions == null)
            {
                Log.Error("No Features selected for activation!");
                return 0;
            }

            if (sharePointContainerLevel == null)
            {
                Log.Error("No Container selected as start level!");
                return 0;
            }

            var definitionsCount = featureDefinitions.Count();
            var activationsCount = FeatureActivationAndDeactivationBulk.DeactivateAllFeaturesWithinSharePointContainer(sharePointContainerLevel, featureDefinitions, force, out ex);

            var msg = activationsCount + " features activated from " + definitionsCount + " selected feature definitions starting on level " + sharePointContainerLevel.Scope + " and below";

            if (ex == null)
            {
                Log.Information(msg);
            }
            else
            {
                Log.Warning(msg, ex);
            }

            return activationsCount;
        }

        public int DeactivateFeatures(IEnumerable<IActivatedFeature> activatedFeatures, bool force)
        {
            Exception ex = null;

            if (activatedFeatures == null)
            {
                Log.Error("No Features selected for deactivation!");
                return 0;
            }

            var countBefore = activatedFeatures.Count();
            var deactivationsCount = FeatureActivationAndDeactivationBulk.DeactivateAllFeatures(activatedFeatures, force, out ex);

            var msg = deactivationsCount + " from " + countBefore + " selected features deactivated.";

            if (ex == null)
            {
                Log.Information(msg);
            }
            else
            {
                Log.Warning(msg, ex);
            }

            return deactivationsCount;
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

        /// <summary>
        /// Gets the farm as feature parent
        /// </summary>
        /// <returns></returns>
        public FeatureParent GetFeatureParentFarm()
        {
            return FeatureParent.GetFeatureParent( FarmRead.GetFarm());
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
