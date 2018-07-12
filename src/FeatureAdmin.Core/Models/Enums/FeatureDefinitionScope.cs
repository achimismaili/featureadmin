using System;

namespace FeatureAdmin.Core.Models.Enums
{
    [Serializable]
    public enum FeatureDefinitionScope
    {
        Web = 10, // scope web is available starting SP 2013, but as it is only read from farm, no problem to keep it here even for sp2010 version
        Site = 20,
        Farm = 40,
        None = 90
    }
}
