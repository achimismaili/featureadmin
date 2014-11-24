using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    /// <summary>
    /// Feature definition and activation data for all features in farm
    /// </summary>
    class FeatureDatabase
    {
        Dictionary<Guid, Feature> _AllFeatureDefinitions = new Dictionary<Guid, Feature>();
        Dictionary<string, List<Feature>> _LocationFeatures = new Dictionary<string,List<Feature>>();
        Dictionary<Guid, List<Location>> _FeatureLocations = new Dictionary<Guid, List<Location>>();

        public bool IsLoaded() { return _AllFeatureDefinitions.Count > 0; }

        public List<Feature> GetAllFeatures()
        {
            return new List<Feature>(_AllFeatureDefinitions.Values);
        }

        public List<Feature> GetFeaturesOfLocation(Location location)
        {
            return GetFeaturesOfLocation(location.Key);
        }
        public List<Feature> GetFeaturesOfLocation(string locationKey)
        {
            if (_LocationFeatures.ContainsKey(locationKey))
            {
                return _LocationFeatures[locationKey];
            }
            else
            {
                return new List<Feature>();
            }
        }
        public List<Location> GetLocationsOfFeature(Feature feature)
        {
            return GetLocationsOfFeature(feature.Id);
        }
        public List<Location> GetLocationsOfFeature(Guid featureId)
        {
            if (_FeatureLocations.ContainsKey(featureId))
            {
                return _FeatureLocations[featureId];
            }
            else
            {
                return new List<Location>();
            }
        }

        /// <summary>
        /// Reload all feature definition and activation data by traversing farm
        /// </summary>
        public void LoadAllData(Dictionary<Guid, List<Location>> activatedFeatureLocations)
        {
            _AllFeatureDefinitions.Clear();
            _LocationFeatures.Clear();
            _FeatureLocations.Clear();

            LoadAllFeatureDefinitions();
            LoadAllFeatureActivations(activatedFeatureLocations);
        }
        private void LoadAllFeatureDefinitions()
        {
            foreach (SPFeatureDefinition spfeatureDef in SPFarm.Local.FeatureDefinitions)
            {
                Feature feature = new Feature(spfeatureDef.Id);
                try
                {
                    // Id
                    feature.Scope = spfeatureDef.Scope;
                    feature.CompatibilityLevel = FeatureManager.GetFeatureCompatibilityLevel(spfeatureDef);
                    feature.Name = spfeatureDef.GetTitle(System.Threading.Thread.CurrentThread.CurrentCulture);
                    // Faulty
                    // ExceptionMessage
                    // Activations
                    // Locations
                }
                catch (Exception exc)
                {
                    feature.AppendExceptionMsg(exc);
                    feature.Faulty = true;
                }
                _AllFeatureDefinitions[feature.Id] = feature;
            }
        }
        private void LoadAllFeatureActivations(Dictionary<Guid, List<Location>> activatedFeatureLocations)
        {
            foreach (KeyValuePair<Guid, List<Location>> activation in activatedFeatureLocations)
            {
                Guid featureId = activation.Key;
                List<Location> locations = activation.Value;
                Feature feature = null;
                if (!_AllFeatureDefinitions.ContainsKey(featureId))
                {
                    // Not sure if this can happen (an undefined feature is activated)
                    // but in case, manufacture an empty definition for it in our list
                    SPFeatureScope scope = locations[0].Scope;
                    feature = new Feature(featureId, scope);
                    _AllFeatureDefinitions.Add(featureId, feature);
                }
                else
                {
                    feature = _AllFeatureDefinitions[featureId];
                }
                // Add to FeatureLocations, which can be done as a lump
                _FeatureLocations[featureId] = locations;
                feature.Activations = locations.Count;
                // Add to LocationFeatures, which has to be done one location at a time
                foreach (Location location in locations)
                {
                    List<Feature> features = null;
                    string lkey = location.Key;
                    if (_LocationFeatures.ContainsKey(lkey))
                    {
                        features = _LocationFeatures[lkey];
                    }
                    else
                    {
                        features = new List<Feature>();
                    }
                    features.Add(feature);
                    _LocationFeatures[lkey] = features;
                }
            }
        }
    }
}
