using FeatureAdmin3.Repository;
using FeatureAdmin3.UI.Common;
using FeatureAdmin3.UI.Details;
using FeatureAdmin3.UI.Features;
using FeatureAdmin3.UI.Log;
using FeatureAdmin3.UI.Parents;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin3.UI
{
    public class MainWindowViewModel : BindableBase
    {
        private DetailsViewModel detailsViewModel = new DetailsViewModel();
        private IFeatureListViewModel featureListViewModel = new FeatureListViewModel();
        private LogListViewModel logListViewModel = new LogListViewModel();
        private IParentsListViewModel parentsListViewModel = new ParentsListViewModel();

        private IBindableBase currentViewModel;
        public IBindableBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public MainWindowViewModel()
        {
            NavCommand = new DelegateCommand<string>(OnNav);
        }

        public DelegateCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "features":
                    CurrentViewModel = featureListViewModel;
                    break;
                case "parents":
                default:
                    CurrentViewModel = parentsListViewModel;
                    break;
            }
        }

        public void Load()
        {
            var repo = new FeatureRepository();
            repo.Init();
            parentsListViewModel.Load();
            featureListViewModel.Load();
        }
    }
}
