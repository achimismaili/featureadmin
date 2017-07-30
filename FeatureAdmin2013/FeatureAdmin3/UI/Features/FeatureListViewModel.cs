using FeatureAdmin.Models;
using FeatureAdmin3.Repository;
using FeatureAdmin3.UI.Common;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin3.UI.Features
{
    public interface IFeatureListViewModel : IBindableBase
    {
        void Load();
    }

    class FeatureListViewModel : BindableBase, 
        IFeatureListViewModel
    {
        private IFeatureRepository repo;
        private IEventAggregator _eventAggregator;
        public FeatureListViewModel()
            :this(new FeatureRepository())
        {
        }

        public FeatureListViewModel(IFeatureRepository featureRepository)
        {
            repo = featureRepository;
            featureDefinitions = new ObservableCollection<FeatureItemViewModel>();
        }

        private ObservableCollection<FeatureItemViewModel> featureDefinitions;
        public ObservableCollection<FeatureItemViewModel> FeatureDefinitions
        {
            get { return featureDefinitions; }
            set { SetProperty(ref featureDefinitions, value); }
        }

        public void Load()
        {
            featureDefinitions.Clear();
            foreach (var f in repo.GetFeatureDefinitions())
            {
                featureDefinitions.Add(new FeatureItemViewModel(
                  f.Id, f.Name, _eventAggregator));
            }
        }
    }
}
