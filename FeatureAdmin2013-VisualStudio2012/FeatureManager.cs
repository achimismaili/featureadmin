using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    /// <summary>Management Class for managing the custom Feature Class</summary>
    public class FeatureManager
    {

        #region class variables

        String _url = null;
        public String Url
        {
            get { return _url; }
        }

        List<Feature> _features = null;
        public List<Feature> Features
        {
            get { return _features; }
        }

        SPFeatureCollection _spfeatures = null;

        SPFeatureDefinitionCollection _spfeatureDefinitions = null;

        public FeatureManager(string url)
        {
            this._url = url;
            this._features = new List<Feature>();
        }

        #endregion

        /// <summary>Adds features to the custom Feature class</summary>
        /// <param name="spFeatureDefinitions"></param>
        public void AddFeatures(SPFeatureCollection spfeatures, SPFeatureScope scope)
        {
            this._spfeatures = spfeatures;

            foreach (SPFeature spfeature in spfeatures)
            {
                Feature feature = new Feature(spfeature.DefinitionId, scope);
                try
                {
                    feature.Name = spfeature.Definition.DisplayName;
                    feature.CompatibilityLevel = GetFeatureCompatibilityLevel(spfeature.Definition);
                }
                catch (Exception exc)
                {
                    feature.AppendExceptionMsg(exc);
                }

                this._features.Add(feature);
            }

        }

        /// <summary>Adds feature definitions to the custom Feature class</summary>
        /// <param name="spFeatureDefinitions"></param>
        public void AddFeatures(SPFeatureDefinitionCollection spFeatureDefinitions)
        {
            this._spfeatureDefinitions = spFeatureDefinitions;

            foreach (SPFeatureDefinition spfeatureDef in spFeatureDefinitions)
            {
                Feature feature = new Feature(spfeatureDef.Id);
                try
                {
                    feature.Scope = spfeatureDef.Scope;
                    feature.Name  = spfeatureDef.GetTitle(System.Threading.Thread.CurrentThread.CurrentCulture);
                    feature.CompatibilityLevel = GetFeatureCompatibilityLevel(spfeatureDef);
                }
                catch (Exception exc)
                {
                    feature.AppendExceptionMsg(exc);
                }

                this._features.Add(feature);
            }
            
        }
 
        /// <summary>forcefully removes a feature from a featurecollection</summary>
        /// <param name="id">Feature ID</param>
        public void ForceRemoveFeature(Guid id)
        {
            try
            {
                _spfeatures.Remove(id, true);
            }
            catch
            {
            }
        }
        public const int COMPATINAPPLICABLE = -8;
        /// <summary>forcefully removes a feature definition from the farm feature definition collection</summary>
        /// <param name="id">Feature Definition ID</param>
        public void ForceUninstallFeatureDefinition(Guid id, int compatibilityLevel)
        {
            #if (SP2013)
            {
                _spfeatureDefinitions.Remove(id, compatibilityLevel, true);
            }
            #elif (SP2010)
            {
                _spfeatureDefinitions.Remove(id, true);
            }
            #elif (SP2007)
            {
                _spfeatureDefinitions.Remove(id, true);
            }
            #else
            {
                throw new Exception("Unspecified SharePoint Version");
            }
            #endif
        }
        public static int GetFeatureCompatibilityLevel(SPFeatureDefinition definition)
        {
            #if (SP2013)
            {
                return definition.CompatibilityLevel;
            }
            #elif (SP2010)
            {
                return COMPATINAPPLICABLE; // inapplicable
            }
            #elif (SP2007)
            {
                return COMPATINAPPLICABLE; // inapplicable
            }
            #else
            {
                throw new Exception("Unspecified SharePoint Version");
            }
            #endif
        }
    }
}
