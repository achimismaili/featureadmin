using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public abstract class BaseItem<T> : IBaseItem
    {
        public BaseItem()
        {
            this.activatedFeatures = new List<T>();
        }

        public IReadOnlyCollection<T> ActivatedFeatures
        {
            get
            {
                return activatedFeatures.AsReadOnly();
            }
        }

        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }
        protected List<T> activatedFeatures { get; set; }

        /// <summary>
        /// adds or removes an activated feature 
        /// </summary>
        /// <param name="feature">feature Guid</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>location with changed activatedfeatures list</returns>
        public void ToggleActivatedFeature(T feature, bool add)
        {
            if (add)
            {
                activatedFeatures.Add(feature);
            }
            else
            {
                activatedFeatures.Remove(feature);
            }
        }
    }
}
