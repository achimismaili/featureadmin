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

        public FeatureManager(string url, bool unused)
        {
            this._url = url;
            this._features = new List<Feature>();
        }

        #endregion

        /// <summary>Adds features to the custom Feature class</summary>
        /// <param name="spFeatureDefinitions"></param>
        public void AddFeatures_NotUsed(SPFeatureCollection spfeatures, SPFeatureScope scope)
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
        public void AddFeatures_Unused(SPFeatureDefinitionCollection spFeatureDefinitions)
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
        public static int GetFeatureCompatibilityLevel(SPFeatureDefinition definition)
        {
#if (SP2013)
            {
                return definition.CompatibilityLevel;
            }
#else
            {
                return Feature.COMPATINAPPLICABLE; // inapplicable
            }
#endif
        }
    }
}
