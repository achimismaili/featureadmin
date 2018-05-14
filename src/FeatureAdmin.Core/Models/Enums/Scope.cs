using System;

namespace FeatureAdmin.Core.Models.Enums
{
    [Serializable]
    public enum Scope
    {
        Web = 10,
        Site = 20,
        WebApplication = 30,
        Farm = 40,
        ScopeInvalid = 90
    }
}
