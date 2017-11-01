using FeatureAdmin.Core.Models;

namespace FeatureAdmin.Messages
{
    public class LoadItem<T> where T : class
    {
        public LoadItem()
        {
            Item = null;
        }
        public LoadItem(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }
}
