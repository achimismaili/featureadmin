using System;

namespace FeatureAdmin.Core.Models
{
    /// <summary>
    /// Wrapper class to indicate if items should be shown as active
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ActiveIndicator<T> where T : class
    {
        public ActiveIndicator(T item, bool active)
        {
            Item = item;
            Active = active;
        }

        public T Item { get; private set; }

        public bool Active { get; private set; }

    }
}
