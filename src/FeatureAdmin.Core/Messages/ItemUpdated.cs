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

        public ItemUpdated([NotNull] T item, bool reportToTaskManager = false)
        {
            Item = item;
            ReportToTaskManager = reportToTaskManager;
        }

        public T Item { get; private set; }

        public bool ReportToTaskManager { get; private set; }
    }
}
