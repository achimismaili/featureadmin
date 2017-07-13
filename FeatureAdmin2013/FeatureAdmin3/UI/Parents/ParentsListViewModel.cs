using FeatureAdmin.Models;
using FeatureAdmin3.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin3.UI.Parents
{
    class ParentsListViewModel : BindableBase
    {
        private IFeatureRepository repo;

        public ParentsListViewModel()
            :this(new FeatureRepository())
        {}

        public ParentsListViewModel(IFeatureRepository featureRepository)
        {
            repo = featureRepository;
        }

        private ObservableCollection<FeatureParent> parents;
        public ObservableCollection<FeatureParent> Parents
        {
            get { return parents; }
            set { SetProperty(ref parents, value); }
        }

        public void LoadParents()
        {
            parents = new ObservableCollection<FeatureParent>(
                repo.GetParents());
        }
    }
}
