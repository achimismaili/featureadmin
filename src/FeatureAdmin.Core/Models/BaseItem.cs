using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public abstract class BaseItem : IBaseItem
    {
        public BaseItem()
        {
            this.activatedFeatures = new List<ActivatedFeature>();
        }
        [IgnoreDuringEquals]
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }

        protected List<ActivatedFeature> activatedFeatures;

        [IgnoreDuringEquals]
        public IReadOnlyCollection<ActivatedFeature> ActivatedFeatures
        {
            get
            {
                return activatedFeatures.AsReadOnly();
            }
        }

        /// <summary>
        /// adds or removes an activated feature 
        /// </summary>
        /// <param name="feature">feature Guid</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>location with changed activatedfeatures list</returns>
        public void ToggleActivatedFeature(ActivatedFeature feature, bool add)
        {
            var exists = activatedFeatures.Contains(feature);

            if (add && !exists )
            {
                activatedFeatures.Add(feature);
            }
            else if (!add && exists)
            {
                activatedFeatures.Remove(feature);
            }
        }
        public abstract List<KeyValuePair<string, string>> GetAsPropertyList();
        protected List<KeyValuePair<string, string>> GetAsPropertyList(bool activatedfeaturesGuidAsLocation)
        {
            string activatedFeatures = "";
            int fCounter = 1;
            foreach (ActivatedFeature f in ActivatedFeatures)
            {
                activatedFeatures += string.Format("{0}. {1}: '{2}'\n",
                    fCounter++,
                    activatedfeaturesGuidAsLocation ? "Location Id" : "Feature Id",
                    activatedfeaturesGuidAsLocation ? f.LocationId.ToString() : f.FeatureId.ToString());
            }

            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(DisplayName),DisplayName),
                new KeyValuePair<string, string>(nameof(Id),Id.ToString()),
                new KeyValuePair<string, string>(nameof(Scope), Scope.ToString()),
                new KeyValuePair<string, string>(nameof(ActivatedFeatures), activatedFeatures)
            };
        }
    }
}
