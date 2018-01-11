using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.ViewModels
{
    public class DetailViewModel 
    {
        public DetailViewModel(string title, IEnumerable<KeyValuePair<string, string>> items)
        {
            Title = title;
            Items = items;
        }

        public DetailViewModel(FeatureDefinition featureDefinition)
        {

        }

        public DetailViewModel(Location location)
        {

        }

        public DetailViewModel(ActivatedFeature activatedFeature)
        {

        }

        public string Title { get; private set; }

        public IEnumerable<KeyValuePair<string, string>> Items { get; private set; }
    }
}
