using System;
using System.Collections.Generic;
using FeatureAdmin.Models;
using FeatureAdmin.Models.Interfaces;
using Microsoft.SharePoint;

namespace FeatureAdminForm.Services
{
    public interface IFeatureRepository
    {
        bool AlwaysUseForce { get; set; }

        int ActivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureDefinition> featureDefinitions, bool force);
        int DeactivateFeatures(IEnumerable<IActivatedFeature> activatedFeatures, bool force);
        int DeactivateFeaturesRecursive(IFeatureParent sharePointContainerLevel, IEnumerable<IFeatureIdentifier> featureDefinitions, bool force);
        List<ActivatedFeature> GetActivatedFeatures(FeatureParent parent = null);
        List<FeatureDefinition> GetFeatureDefinitions(SPFeatureScope? scope = default(SPFeatureScope?));
        FeatureParent GetFeatureParentFarm();
        List<FeatureParent> GetSharePointChildHierarchy(Guid containerId);
        List<FeatureParent> GetSharePointWebApplications();
    }
}