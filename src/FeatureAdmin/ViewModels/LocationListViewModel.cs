using Caliburn.Micro;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen
    {
        public IDataService DataService;
        public LocationListViewModel(IDataService dataService)
        {
            DataService = dataService;
            Maus = "piep";
        }

        private string maus;
        public string Maus
        {
            get { return maus; }
            set
            {
                maus = value;
                NotifyOfPropertyChange(() => Maus);
            }
        }
    }
}
