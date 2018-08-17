namespace FeatureAdmin.Core.Models
{
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
