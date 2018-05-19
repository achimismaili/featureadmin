using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public class ActivatedFeatureForSearch : ActivatedFeatureBase
    {
        public ActivatedFeatureForSearch(
                Guid featureId,
                string displayName,
                string featureDefinitionUniqueIdentifier,
                Guid locationId,
                FeatureDefinition definition,
                bool faulty,
                DateTime timeActivated,
                Version version,
                bool canUpgrade
            ) : base(featureId,
                displayName,
                featureDefinitionUniqueIdentifier,
                locationId,
                faulty,
                timeActivated,
                version,
                canUpgrade)
        {
        }
    }
}
