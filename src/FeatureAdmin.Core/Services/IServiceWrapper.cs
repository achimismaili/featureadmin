using FeatureAdmin.Core.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.DataServices.Contracts
{
    public interface IServiceWrapper
    {
        ObservableCollection<IActivatedFeature> ActivatedFeatures { get; }

        ObservableCollection<IFeatureDefinition> FeatureDefinitions { get; }
        ObservableCollection<ILocation> Locations { get; }
    }
}
