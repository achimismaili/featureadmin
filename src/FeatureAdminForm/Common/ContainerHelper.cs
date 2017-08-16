using FeatureAdminForm.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdminForm.Common
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IFeatureRepository, FeatureRepository>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
