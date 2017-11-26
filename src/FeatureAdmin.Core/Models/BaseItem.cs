using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public abstract class BaseItem<T> : IBaseItem
    {
        public BaseItem()
        {
            this.activatedFeatures = new List<T>();
        }
        [IgnoreDuringEquals]
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }

        protected List<T> activatedFeatures;

        [IgnoreDuringEquals]
        public IReadOnlyCollection<T> ActivatedFeatures
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
        public void ToggleActivatedFeature(T feature, bool add)
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
    }
}
