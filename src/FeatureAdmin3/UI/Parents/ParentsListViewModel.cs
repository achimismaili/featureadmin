using FeatureAdmin.Models;
using FeatureAdmin3.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin3.UI.Common;
using Prism.Events;

namespace FeatureAdmin3.UI.Parents
{
    public interface IParentsListViewModel  : IBindableBase
    {
        void Load();
    }
    class ParentsListViewModel : BindableBase, IParentsListViewModel
    {
        private IFeatureRepository repo;
        private IEventAggregator _eventAggregator;

        public ParentsListViewModel()
            :this(new FeatureRepository())
        {
        }

        public ParentsListViewModel(IFeatureRepository featureRepository)
        {
            repo = featureRepository;
            parents = new ObservableCollection<ParentItemViewModel>();

        }

        private ObservableCollection<ParentItemViewModel> parents;
        public ObservableCollection<ParentItemViewModel> Parents
        {
            get { return parents; }
            set { SetProperty(ref parents, value); }
        }

        public void Load()
        {
            parents.Clear();
            foreach (var p in repo.GetParents())
            {
                parents.Add(new ParentItemViewModel(
                  p.Id, p.Url, _eventAggregator));
            }
        }
    }
}
