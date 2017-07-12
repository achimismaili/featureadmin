using FeatureAdmin3.UI.Common;
using FeatureAdmin3.UI.Details;
using FeatureAdmin3.UI.Features;
using FeatureAdmin3.UI.Log;
using FeatureAdmin3.UI.Parents;
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
        private FeatureListViewModel featureListViewModel = new FeatureListViewModel();
        private LogListViewModel logListViewModel = new LogListViewModel();
        private ParentsListViewModel parentsListViewModel = new ParentsListViewModel();

        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
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
            NavCommand = new RelayCommand<string>(OnNav);
        }

        public RelayCommand<string> NavCommand { get; private set; }

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

    }
}
