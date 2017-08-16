using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Models
{
    public class ActivatedFeatureFactory
    {
        /// <summary>
        /// corresponding SharePoint SPFeature to this class instance
        /// </summary>
        public SPFeature SharePointFeature { get; private set; }

        /// <summary>
        /// Feature definition related to this activated feature
        /// </summary>
        /// <remarks>this is null, when feature is faulty
        /// Overwrite of base class, because definition is not set for
        /// Activated Feature, just implicitly retrieved from SPFeature
        /// </remarks>
        public SPFeatureDefinition Definition
        {
            get
            {
                if (Faulty || SharePointFeature == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        return SharePointFeature.Definition;
                    }
                    catch
                    {
                        // TBD Log(?)
                        Faulty = true;
                        return null;
                    }
                }
            }
            protected set { } // just for base class implementation
        }

        /// <summary>
        /// Returns the version of the Feature definition, if Definition exists
        /// </summary>
        public System.Version DefinitionVersion
        {
            get
            {
                if (Definition == null)
                {
                    return null;
                }
                else
                {
                    return Definition.Version;
                }
            }
        }

        /// <summary>
        /// Defines, if a feature is faulty, e.g. has no corresponding definition
        /// </summary>
        public bool Faulty { get; private set; }

        /// <summary>
        /// Feature (definition) ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Folder Name of the Feature (Definition.Displayname)
        /// </summary>
        /// <remarks>The title is only retrieved in the Feature Definition --> Definition.GetTitle(culture)</remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Feature Parent of the activated feature
        /// </summary>
        public Interfaces.IFeatureParent Parent { get; private set; }

        /// <summary>
        /// Scope, the Feature is activated in
        /// </summary>
        /// /// <remarks>this 
        /// Overwrite of base class, because 
        /// scope is only retrieved from parent
        /// </remarks>
        public SPFeatureScope Scope
        {
            get
            {
                if (Parent == null)
                {
                    return SPFeatureScope.ScopeInvalid;
                }
                else
                {
                    return Parent.Scope;
                }
            }
            protected set { } // just for base class implementation
        }

        /// <summary>
        /// When was this Feature activated?
        /// </summary>
        public DateTime TimeActivated { get; private set; }

        /// <summary>
        /// Version of the activated Feature
        /// </summary>
        public System.Version Version { get; private set; }

        /// <summary>
        /// Generate ActivatedFeature
        /// </summary>
        /// <param name="spFeature"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static ActivatedFeature GetActivatedFeature(SPFeature spFeature, FeatureParent parent = null)
        {
            if (spFeature == null)
            {
                return null;
            }

            // create preliminary feature with default values ...
            ActivatedFeature af = new ActivatedFeature()
            {

                Id = Guid.Empty,
                Version = new Version("0.0.0.0"),
                Parent = parent,
                SharePointFeature = spFeature,
                Name = "undefined",
                Faulty = false
            };

            // in case of faulty features, many of these settings can not be set
            // therefore, every property is set in try / catch statement

            // ID
            try
            {
                af.Id = spFeature.DefinitionId;
            }
            catch (Exception ex)
            {
                if (af.Name.Equals("undefined"))
                {
                    af.Name = ex.Message;
                }
                af.Faulty = true;
            }

            // Version
            try
            {
                af.TimeActivated = spFeature.TimeActivated;
            }
            catch (Exception ex)
            {
                if (af.Name.Equals("undefined"))
                {
                    af.Name = ex.Message;
                }
                af.Faulty = true;
            }

            // Version
            try
            {
                af.Version = spFeature.Version;
            }
            catch (Exception ex)
            {
                if (af.Name.Equals("undefined"))
                {
                    af.Name = ex.Message;
                }
                af.Faulty = true;
            }

            // Parent
            try
            {
                af.Parent = parent == null ? FeatureParent.GetFeatureParent(spFeature) : parent;
            }
            catch (Exception ex)
            {
                if (af.Name.Equals("undefined"))
                {
                    af.Name = ex.Message;
                }
                af.Faulty = true;
            }

            // SharePointFeature is already set on initialization of af
            // Name
            try
            {
                var def = spFeature.Definition;

                if (def != null)
                {
                    af.Name = def.DisplayName;
                }
                else
                {
                    af.Name = string.Format(Common.Constants.Text.UndefinedActivatedFeature, af.Id);
                    af.Faulty = true;
                }
            }
            catch (Exception ex)
            {
                if (af.Name.Equals("undefined"))
                {
                    af.Name = ex.Message;
                }
                af.Faulty = true;
            }

            return af;
        }

        /// <summary>
        /// Factory to map SPFeatureCollections to ActivatedFeatures
        /// </summary>
        /// <param name="featureCollection"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, FeatureParent parent)
        {
            List<ActivatedFeature> activatedFeatures = new List<ActivatedFeature>();

            if (featureCollection != null)
            {
                foreach (SPFeature f in featureCollection)
                {
                    var af = MapSpFeatureToActivatedFeature(f, parent);
                    activatedFeatures.Add(af);
                }
            }

            return activatedFeatures;
        }

        private static ActivatedFeature MapSpFeatureToActivatedFeature(SPFeature feature, FeatureParent parent)
        {
            var af = GetActivatedFeature(feature, parent);

            return af;
        }
    }
}