using FA.Models;
using FA.UI.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models.Interfaces;

namespace FA.UI.Features
{
    public class FeatureViewModel : ViewModelBase, IFeatureViewModel
    {
        public FeatureViewModel(IFeatureDefinition fd)
        {
            Feature = fd;
        }
        public IFeatureDefinition Feature { get; private set; }

    }
}
