using FeatureAdmin.Models;
using FeatureAdmin3.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin3.UI.Features
{
    class FeatureListViewModel : BindableBase
    {
        private IFeatureRepository repo;

        public FeatureListViewModel()
            :this(new FeatureRepository())
        { }

        public FeatureListViewModel(IFeatureRepository featureRepository)
        {
            repo = featureRepository;
        }

        private ObservableCollection<FeatureDefinition> featureDefinitions;
        public ObservableCollection<FeatureDefinition> FeatureDefinitions
        {
            get { return featureDefinitions; }
            set { SetProperty(ref featureDefinitions, value); }
        }

        public void LoadFeatures()
        {
            featureDefinitions = new ObservableCollection<FeatureDefinition>(
                repo.GetFeatureDefinitions());
        }
    }
}
