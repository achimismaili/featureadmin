using FeatureAdmin.Core.Models;
using FeatureAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Common
{
    public static class DetailViewModelConverters
    {
        private static string PropertiesToString(Dictionary<string, string> properties)
        {
            var propString = string.Empty;

            if (properties != null)
            {
                foreach (var p in properties)
                {
                    propString += string.Format("'{0}' : '{1}'\n", p.Key, p.Value);
                }
            }

            return propString;
        }

        public static DetailViewModel ToDetailViewModel (this ActivatedFeature vm)
        {
            string displayName = vm.DisplayName;

            var items = new List<KeyValuePair<string,string>>();


            items.Add(new KeyValuePair<string, string>(nameof(vm.DisplayName), vm.DisplayName));
            items.Add(new KeyValuePair<string, string>(nameof(vm.FeatureId),vm.FeatureId.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.LocationId),vm.LocationId.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Definition.Scope), vm.Definition.Scope.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.TimeActivated),vm.TimeActivated.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Version),vm.Version.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Faulty),vm.Faulty.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(Properties), PropertiesToString(vm.Properties)));

            var dvm = new DetailViewModel(displayName, items);

            return dvm;
        }

        public static DetailViewModel ToDetailViewModel(this FeatureDefinition vm, IEnumerable<ActivatedFeature> activatedFeatures)
        {
            string displayName = vm.DisplayName;

            if (activatedFeatures == null)
            {
                activatedFeatures = new List<ActivatedFeature>();
            }

            var items = new List<KeyValuePair<string, string>>();

            items.Add(new KeyValuePair<string, string>(nameof(vm.DisplayName), vm.DisplayName));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Id), vm.Id.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Scope), vm.Scope.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Title), vm.Title));
            items.Add(new KeyValuePair<string, string>("Times Activated in Farm", activatedFeatures.Count().ToString()));
            items.Add(ConvertActivatedFeatures(activatedFeatures,true));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Name), vm.Name));
            items.Add(new KeyValuePair<string, string>(nameof(vm.CompatibilityLevel), vm.CompatibilityLevel.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Description), vm.Description));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Hidden), vm.Hidden.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.SolutionId), vm.SolutionId.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.UIVersion), vm.UIVersion.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Version), vm.Version == null ? string.Empty : vm.Version.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.SandBoxedSolutionLocation), vm.SandBoxedSolutionLocation.HasValue ? vm.SandBoxedSolutionLocation.Value.ToString() : string.Empty));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Properties), PropertiesToString(vm.Properties)));


            var dvm = new DetailViewModel(displayName, items);

            return dvm;
        }

        public static DetailViewModel ToDetailViewModel(this Location vm, IEnumerable<ActivatedFeature> activatedFeatures)
        {
            string displayName = vm.DisplayName;

            if (activatedFeatures == null)
            {
                activatedFeatures = new List<ActivatedFeature>();
            }

            var items = new List<KeyValuePair<string, string>>();
            items.Add(new KeyValuePair<string, string>(nameof(vm.DisplayName), vm.DisplayName));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Id), vm.Id.ToString()));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Scope), vm.Scope.ToString()));
            items.Add(new KeyValuePair<string, string>("# of active features", activatedFeatures.Count().ToString()));
            items.Add(ConvertActivatedFeatures(activatedFeatures, false));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Parent), string.Format("Location Id: {0}", vm.Parent.ToString())));
            items.Add(new KeyValuePair<string, string>(nameof(vm.Url), vm.Url));
            items.Add(new KeyValuePair<string, string>(nameof(vm.ChildCount), vm.ChildCount.ToString()));

            var dvm = new DetailViewModel(displayName, items);

            return dvm;
        }

        private static KeyValuePair<string, string> ConvertActivatedFeatures(IEnumerable<ActivatedFeature> activatedFeatures, bool forFeatureDefinition)
        {
            StringBuilder features = new StringBuilder();
            int fCounter = 1;
            foreach (ActivatedFeature f in activatedFeatures)
            {
                features.Append( string.Format("{0}. {1}: '{2}'\n",
                    fCounter++,
                    forFeatureDefinition ? "Location Id" : "Feature Id",
                    forFeatureDefinition ? f.LocationId.ToString() : f.FeatureId.ToString()));
            }

            string keyValue = forFeatureDefinition ? "Total activated in farm" : "Active features in this location";

            return (new KeyValuePair<string, string>(keyValue, features.ToString()));
            }
    }
}
