using FA.Models;
using FA.UI.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI.Features
{
    public class FeatureViewModel : ViewModelBase, IFeatureViewModel
    {
        public FeatureDefinition Feature { get; private set; }
    }
}
