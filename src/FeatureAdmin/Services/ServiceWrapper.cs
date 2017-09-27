using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FeatureAdmin.Core.Models.Contracts;
using System.Collections.ObjectModel;
using FeatureAdmin.Core.DataServices.Contracts;
using System.ComponentModel.Composition;

namespace FeatureAdmin.Services
{
    public class ServiceWrapper : PropertyChangedBase, IServiceWrapper
    {
        private ObservableCollection<IActivatedFeature> activatedFeatures;
        private ObservableCollection<IFeatureDefinition> featureDefinitions;
        private ObservableCollection<ILocation> locations;

        private IDataService dataService;

        public ServiceWrapper(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public void LoadFarm()
        {
            ActivatedFeatures = new ObservableCollection<IActivatedFeature>(dataService.ActivatedFeatures);
            FeatureDefinitions = new ObservableCollection<IFeatureDefinition>(dataService.FeatureDefinitions);
            Locations = new ObservableCollection<ILocation>(dataService.Locations);
        }

        public ObservableCollection<IActivatedFeature> ActivatedFeatures
        {
            get { return activatedFeatures; }
            set
            {
                activatedFeatures = value;
                NotifyOfPropertyChange(() => ActivatedFeatures);

            }
        }
        public ObservableCollection<IFeatureDefinition> FeatureDefinitions
        {
            get { return featureDefinitions; }
            set
            {
                featureDefinitions = value;
                NotifyOfPropertyChange(() => FeatureDefinitions);

            }
        }
        public ObservableCollection<ILocation> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                NotifyOfPropertyChange(() => Locations);
            }
        }
    }
}
