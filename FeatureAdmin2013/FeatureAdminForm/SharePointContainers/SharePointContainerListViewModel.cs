using FeatureAdminForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdminForm.SharePointContainers
{
    public class SharePointContainerListViewModel : BindableBase
    {
        private IFeatureRepository repo;

        public SharePointContainerListViewModel(IFeatureRepository repository)
        {
            repo = repository;
        }
    }
}
