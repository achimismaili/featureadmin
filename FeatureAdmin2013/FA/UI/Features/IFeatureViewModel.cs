using FA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.UI.Features
{
    public interface IFeatureViewModel
    {
        FeatureDefinition Feature { get; }
    }
}
