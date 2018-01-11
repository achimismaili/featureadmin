using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models;
using Caliburn.Micro;

namespace FeatureAdmin.ViewModels
{
    public class DetailViewModel : IHaveDisplayName
    {
        public DetailViewModel(string displayName, IEnumerable<KeyValuePair<string, string>> items)
        {
            DisplayName = string.Format("Detail view for {0}", displayName);
            Items = items;
        }

        public IEnumerable<KeyValuePair<string, string>> Items { get; private set; }

        public string DisplayName { get; set; }
    }
}
