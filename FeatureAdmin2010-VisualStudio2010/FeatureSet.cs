using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;

namespace FeatureAdmin
{
    /// <summary>
    /// A list of features
    /// </summary>
    class FeatureSet
    {
        List<Feature> _FarmFeatures = new List<Feature>();
        List<Feature> _WebAppFeatures = new List<Feature>();
        List<Feature> _SiteCollectionFeatures = new List<Feature>();
        List<Feature> _WebFeatures = new List<Feature>();
        SPFeatureScope _HighestScope = SPFeatureScope.ScopeInvalid;
        SPFeatureScope _LowestScope = SPFeatureScope.ScopeInvalid;

        public SPFeatureScope HighestScope { get { return _HighestScope; } }
        public SPFeatureScope LowestScope { get { return _LowestScope; } }
        public IEnumerable<Feature> FarmFeatures { get { return _FarmFeatures; } }
        public IEnumerable<Feature> WebAppFeatures { get { return _WebAppFeatures; } }
        public IEnumerable<Feature> SiteCollectionFeatures { get { return _SiteCollectionFeatures; } }
        public IEnumerable<Feature> WebFeatures { get { return _WebFeatures; } }
        public int FarmFeatureCount { get { return _FarmFeatures.Count; } }
        public int WebAppFeatureCount { get { return _WebAppFeatures.Count; } }
        public int SiteCollectionFeatureCount { get { return _SiteCollectionFeatures.Count; } }
        public int WebFeatureCount { get { return _WebFeatures.Count; } }

        public FeatureSet(List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                if (_HighestScope == SPFeatureScope.ScopeInvalid)
                {
                    _HighestScope = feature.Scope;
                    _LowestScope = feature.Scope;
                }
                else
                {
                    if (feature.Scope > _HighestScope)
                    {
                        _HighestScope = feature.Scope;
                    }
                    if (feature.Scope < _LowestScope)
                    {
                        _LowestScope = feature.Scope;
                    }
                }
                switch (feature.Scope)
                {
                    case SPFeatureScope.Farm:
                        _FarmFeatures.Add(feature);
                        break;
                    case SPFeatureScope.WebApplication:
                        _WebAppFeatures.Add(feature);
                        break;
                    case SPFeatureScope.Site:
                        _SiteCollectionFeatures.Add(feature);
                        break;
                    case SPFeatureScope.Web:
                        _WebFeatures.Add(feature);
                        break;
                    default:
                        throw new Exception(string.Format(
                            "Unknown feature scope {0} in feature {1}",
                            feature.Scope,
                            feature.Name
                            ));
                }
            }
        }
    }
}
