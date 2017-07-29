using System;
using System.Collections.Generic;
using FeatureAdmin.Models;
using FeatureAdmin.Models.Interfaces;
using Microsoft.SharePoint;
using System.Threading.Tasks;

namespace FeatureAdmin3.Repository
{
    public interface IFeatureRepository
    {
        Task<IFeatureRepository> Init();
        Task<IFeatureRepository> Reload();
        // get all parents tree
        List<FeatureParent> GetParents();

        FeatureParent GetFarm();
        List<FeatureParent> GetParentsChildren(Guid parentId);
        
        int ActivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureDefinition> featureDefinitions, bool force);
        int DeactivateFeatures(IEnumerable<IActivatedFeature> activatedFeatures, bool force);
        int DeactivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureIdentifier> featureDefinitions, bool force);
        List<ActivatedFeature> GetActivatedFeatures(FeatureParent parent = null);
        List<FeatureDefinition> GetFeatureDefinitions(SPFeatureScope? scope = default(SPFeatureScope?));
        
    }
}