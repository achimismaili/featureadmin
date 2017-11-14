using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages
{
    public class ItemUpdated<T> where T : class
    {
        public object i;

        public ItemUpdated(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }
}
