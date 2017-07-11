using FeatureAdminForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdminForm.ActivatedFeatures
{
    public class ActivatedFeatureListViewModel : BindableBase
    {
        private IFeatureRepository repo;

        public ActivatedFeatureListViewModel(IFeatureRepository repository)
        {
            repo = repository;
        }
    }
}
