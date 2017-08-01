using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FA.Models
{
    public class FeatureDefinition : Interfaces.IFeatureDefinition
    {
        /// <summary>
        /// a list of all activated features based on this definition
        /// </summary>
        public List<ActivatedFeature> ActivatedFeatures { get; set; }

        /// <summary>
        /// Returns the version of the Feature definition, if Definition exists
        /// </summary>
        public int CompatibilityLevel
        {
            get
            {
                if (Definition == null)
                {
                    return 0;
                }
                else
                {
#if (SP2013)
                    {
                        return Definition.CompatibilityLevel;
                    }
#else
            {
                return 0; // inapplicable
            }
#endif
                }
            }
        }

        /// <summary>
        /// corresponding SharePoint SPFeatureDefinition to this class instance
        /// </summary>
        public SPFeatureDefinition Definition { get; private set; }

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
        /// Scope of the Feature
        /// </summary>
        public SPFeatureScope Scope { get; private set; }

        public string GetTitle
        {
            get
            {
                if (Definition == null)
                {
                    return "undefined";
                }
                else
                {
                    return Definition.GetTitle(System.Threading.Thread.CurrentThread.CurrentCulture);
                }
            }
        }

        public IEnumerable<KeyValuePair<string, string>> AdditionalProperties
        {
            get
            {
                if (Definition == null)
                {
                    return null;
                }
                else
                {
                    var props = new List<KeyValuePair<string, string>>();
                    try
                    {
                        props.Add(new KeyValuePair<string, string>("SolutionId", Definition.SolutionId.ToString()));
                        props.Add(new KeyValuePair<string, string>("ReceiverAssembly", Definition.ReceiverAssembly));
                        props.Add(new KeyValuePair<string, string>("ReceiverClass", Definition.ReceiverClass));
                        props.Add(new KeyValuePair<string, string>("UIVersion", Definition.UIVersion));
                        props.Add(new KeyValuePair<string, string>("UpgradeReceiverAssembly", Definition.UpgradeReceiverAssembly));
                        props.Add(new KeyValuePair<string, string>("UpgradeReceiverClass", Definition.UpgradeReceiverClass));
                        props.Add(new KeyValuePair<string, string>("AutoActivateInCentralAdmin", Definition.AutoActivateInCentralAdmin.ToString()));
                        props.Add(new KeyValuePair<string, string>("ActivateOnDefault", Definition.ActivateOnDefault.ToString()));
                        props.Add(new KeyValuePair<string, string>("RootDirectory", Definition.RootDirectory));
                        props.Add(new KeyValuePair<string, string>("Hidden", Definition.Hidden.ToString()));
                        props.Add(new KeyValuePair<string, string>("ActivationDependencies", Definition.ActivationDependencies.ToString()));
                    }
                    catch
                    {
                        // TODO log
                    }
                    return props;
                }
            }
        }

    /// <summary>
    /// Get a feature definition based on an activated feature, even from a faulty one
    /// </summary>
    /// <param name="feature">an activated Feature</param>
    /// <returns>A FeatureDefinition with current feature already added</returns>
    /// <remarks>
    /// ActivatedFeature is better than SPFeature, because it already handled the faulty feature issues
    /// </remarks>
        public static FeatureDefinition GetFeatureDefinition(ActivatedFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException("Parameter 'feature'");
            }

            var fd = new FeatureDefinition()
            {
                Id = feature.Id,
                Name = feature.Name,
                Scope = feature.Scope,
                Faulty = feature.Faulty,
                Definition = feature.Definition
            };
               
            fd.ActivatedFeatures.Add(feature);

            return fd;
        }

        public static FeatureDefinition GetFeatureDefinition(IEnumerable<ActivatedFeature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException("Parameter 'features'");
            }

            var fd = GetFeatureDefinition(features.First());
            fd.ActivatedFeatures.AddRange(features);

            return fd;
        }

        public static FeatureDefinition GetFeatureDefinition(SPFeatureDefinition definition,
            IEnumerable<ActivatedFeature> activatedFeatures = null)
        {
            if (definition == null)
            {
                throw new ArgumentNullException("Parameter definition must not be null!");
            }

            var fd = new FeatureDefinition()
            {
                Definition = definition,
                Id = definition.Id,
                Name = definition.DisplayName,
                Scope = definition.Scope,
                Faulty = false
            };

            if (activatedFeatures != null)
            {
                fd.ActivatedFeatures.AddRange(activatedFeatures);
            }

            return fd;       
        }

        /// <summary>
        /// Maps a list of SPFeatureDefinitions To FeatureDefinitions
        /// </summary>
        /// <param name="definitions">list of SPFeatureDefinitions</param>
        /// <returns>list of FeatureDefinitions</returns>
        public static List<FeatureDefinition> GetFeatureDefinition(SPFeatureDefinitionCollection definitions)
        {
            if (definitions == null)
            {
                throw new ArgumentNullException("Parameter definitions must not be null!");
            }

            List<FeatureDefinition> fDefs = new List<FeatureDefinition>();

            foreach (SPFeatureDefinition spfd in definitions)
            {
                var fd = GetFeatureDefinition(spfd);
                fDefs.Add(fd);
            }

            return fDefs;
        }

        private FeatureDefinition()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
        }
    }
}
