using Caliburn.Micro;
using FeatureAdmin.Core.DataServices.Contracts;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen
    {
        public IServiceWrapper DataService;
        public LocationListViewModel(IServiceWrapper dataService)
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
