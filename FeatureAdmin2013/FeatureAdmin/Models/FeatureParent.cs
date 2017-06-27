using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models
{
    public class FeatureParent
    {
        public string DisplayName { get; }
        public Guid Id { get; }

        public string Url { get; }

        public FeatureParent(string displayName, string url, Guid id)
        {
            DisplayName = displayName;
            Url = url;
            Id = id;
        }
    }
}
