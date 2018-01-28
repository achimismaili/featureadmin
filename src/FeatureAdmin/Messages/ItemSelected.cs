using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class ItemSelected<T> where T : class, IBaseItem
    {
        public ItemSelected(T item)
        {
            this.Item = item;
        }

        public T Item { get; }
    }
}
