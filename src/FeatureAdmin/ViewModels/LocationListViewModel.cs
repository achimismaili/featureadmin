using Caliburn.Micro;
using FeatureAdmin.Core.DataServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen
    {
        public IServiceWrapper DataService;
        public LocationListViewModel()
        {
          //  DataService = dataService;
        }

    }
}
