using FeatureAdmin.Models;
using FeatureAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Repository
{
    public class ChangeRepository
    {
        public static int ActivateFeatures(IFeatureParent sharePointContainerLevel, IFeatureDefinition featureDefinition, bool force)
        {
            int successful = 0;


            return successful;
        }

        public static int DeactivateFeature(IActivatedFeature activatedFeature, bool force)
        {
            int successful = 0;

            return successful;
        }

        public static int DeactivateFeature(List<IActivatedFeature> activatedFeatures, bool force)
        {
            int successful = 0;

            return successful;
        }

    }
}
