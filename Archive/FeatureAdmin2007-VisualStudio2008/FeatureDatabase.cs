using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Be.Timvw.Framework.ComponentModel;

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

        public SortableBindingList<Feature> GetAllFeatures()
        {
            return new SortableBindingList<Feature>(_AllFeatureDefinitions.Values);
        }
        public int GetAllFeaturesCount()
        {
            return _AllFeatureDefinitions.Count;
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
        /// <summary>
        /// Flag all specified features as faulty
        /// (& add to our master definition list any that are missing)
        /// </summary>
        public void MarkFaulty(List<Guid> featureIds)
        {
            foreach (Guid featureId in featureIds)
            {
                Feature feature = null;
                if (_AllFeatureDefinitions.ContainsKey(featureId))
                {
                    feature = _AllFeatureDefinitions[featureId];
                }
                else
                {
                    feature = new Feature(featureId);
                }
                feature.IsFaulty = true;
                _AllFeatureDefinitions[featureId] = feature;
            }
        }
        /// <summary>
        /// Record that a feature has been activated (at location specified by object)
        /// </summary>
        public void RecordFeatureActivation(object locobj, Guid featureId)
        {
            Location location = LocationManager.GetLocation(locobj);
        }
        /// <summary>
        /// Record that a feature has been activated (at location specified by Location)
        /// </summary>
        public void RecordFeatureActivationAtLocation(Location location, Guid featureId)
        {
            Feature feature = GetOrAddFeatureFromDefinitions(featureId, location.Scope);
            AddToFeatureLocations(feature, location);
            AddToLocationFeatures(location, feature);
        }
        /// <summary>
        /// Record that a feature has been deactivated
        /// </summary>
        public void RecordFeatureDeactivation(object locobj, Guid featureId)
        {
            Location location = LocationManager.GetLocation(locobj);
            RecordFeatureDeactivationAtLocation(location, featureId);
        }
        public void DoIt()
        {
        }
        /// <summary>
        /// Record that a feature has been deactivated (at location specified by Location)
        /// </summary>
        public void RecordFeatureDeactivationAtLocation(Location location, Guid featureId)
        {
            Feature feature = GetOrAddFeatureFromDefinitions(featureId, location.Scope);
            RemoveFromFeatureLocations(feature, location);
            RemoveFromLocationFeatures(location, feature);
        }
        private void LoadAllFeatureDefinitions()
        {
            foreach (SPFeatureDefinition spfeatureDef in SPFarm.Local.FeatureDefinitions)
            {
                Feature feature = new Feature(spfeatureDef.Id);
                try
                {
                    // Id
                    GetFeatureScopeFromDefinition(spfeatureDef, ref feature);
                    GetFeatureCompatibilityFromDefinition(spfeatureDef, ref feature);
                    GetFeatureNameFromDefinition(spfeatureDef, ref feature);
                    // Faulty
                    // ExceptionMessage
                    // Activations
                    // Locations
                }
                catch (Exception exc)
                {
                    feature.AppendExceptionMsg(exc);
                    feature.IsFaulty = true;
                }
                _AllFeatureDefinitions[feature.Id] = feature;
            }
        }
        private void GetFeatureScopeFromDefinition(SPFeatureDefinition fdef, ref Feature feature)
        {
            try
            {
                feature.Scope = fdef.Scope;
            }
            catch (Exception exc)
            {
                feature.AppendExceptionMsg(exc);
                feature.IsFaulty = true;
            }
        }
        private void GetFeatureCompatibilityFromDefinition(SPFeatureDefinition fdef, ref Feature feature)
        {
            try
            {
                feature.CompatibilityLevel = FeatureManager.GetFeatureCompatibilityLevel(fdef);
            }
            catch (Exception exc)
            {
                feature.AppendExceptionMsg(exc);
                feature.IsFaulty = true;
            }
        }
        private void GetFeatureNameFromDefinition(SPFeatureDefinition fdef, ref Feature feature)
        {
            try
            {
                feature.Name = fdef.GetTitle(System.Threading.Thread.CurrentThread.CurrentCulture);
                return;
            }
            catch (Exception exc)
            {
                feature.AppendExceptionMsg(exc);
                feature.IsFaulty = true;
            }
            try
            {
                feature.Name = fdef.DisplayName;
                return;
            }
            catch (Exception exc)
            {
                feature.AppendExceptionMsg(exc);
                feature.IsFaulty = true;
            }
        }
        private void LoadAllFeatureActivations(Dictionary<Guid, List<Location>> activatedFeatureLocations)
        {
            foreach (KeyValuePair<Guid, List<Location>> activation in activatedFeatureLocations)
            {
                Guid featureId = activation.Key;
                List<Location> locations = activation.Value;
                SPFeatureScope scope = locations[0].Scope;
                Feature feature = GetOrAddFeatureFromDefinitions(featureId, scope);
                // Add to FeatureLocations, which can be done as a lump
                _FeatureLocations[featureId] = locations;
                feature.Activations = locations.Count;
                // Add to LocationFeatures, which has to be done one location at a time
                foreach (Location location in locations)
                {
                    AddToLocationFeatures(location, feature);
                }
            }
        }
        /// <summary>
        /// Look up & return feature from our master definition list
        /// (Create a placeholder entry if we're lacking it)
        /// </summary>
        private Feature GetOrAddFeatureFromDefinitions(Guid featureId, SPFeatureScope scope)
        {
            Feature feature = null;
            if (!_AllFeatureDefinitions.ContainsKey(featureId))
            {
                // Not sure if this can happen (an undefined feature is activated)
                // but in case, manufacture an empty definition for it in our list
                feature = new Feature(featureId, scope);
                _AllFeatureDefinitions.Add(featureId, feature);
            }
            else
            {
                feature = _AllFeatureDefinitions[featureId];
            }
            return feature;
        }
        /// <summary>
        /// Add record to our _LocationFeatures
        /// </summary>
        private void AddToLocationFeatures(Location location, Feature feature)
        {
            string lkey = location.Key;
            if (!_LocationFeatures.ContainsKey(lkey))
            {
                _LocationFeatures.Add(lkey, new List<Feature>());
            }
            List<Feature> features = _LocationFeatures[lkey];
            if (!features.Contains(feature))
            {
                features.Add(feature);
            }
        }
        /// <summary>
        /// Add record to our _FeaturesLocation
        /// </summary>
        private void AddToFeatureLocations(Feature feature, Location location)
        {
            if (!_FeatureLocations.ContainsKey(feature.Id))
            {
                _FeatureLocations.Add(feature.Id, new List<Location>());
            }
            List<Location> locations = _FeatureLocations[feature.Id];
            if (!locations.Contains(location))
            {
                locations.Add(location);
                UpdateFeatureLocationCount(feature.Id);
            }
        }
        /// <summary>
        /// Remove record from our _LocationFeatures
        /// </summary>
        private void RemoveFromLocationFeatures(Location location, Feature feature)
        {
            string lkey = location.Key;
            if (_LocationFeatures.ContainsKey(lkey))
            {
                List<Feature> features = _LocationFeatures[lkey];
                if (features.Contains(feature))
                {
                    features.Remove(feature);
                }
            }
        }
        /// <summary>
        /// Remove record from our _FeaturesLocation
        /// </summary>
        private void RemoveFromFeatureLocations(Feature feature, Location location)
        {
            if (_FeatureLocations.ContainsKey(feature.Id))
            {
                List<Location> locations = _FeatureLocations[feature.Id];
                if (locations.Contains(location))
                {
                    locations.Remove(location);
                }
                UpdateFeatureLocationCount(feature.Id);
            }
        }
        private void UpdateFeatureLocationCount(Guid featureId)
        {
            Feature feature = _AllFeatureDefinitions[featureId];
            int count = 0;
            if (_FeatureLocations.ContainsKey(featureId))
            {
                count = _FeatureLocations[featureId].Count;
            }
            feature.Activations = count;
        }
    }
}
