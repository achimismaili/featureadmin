using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.ViewModels;
using System.Reflection;

namespace FeatureAdmin
{
    public class AppBootstrapper : CaliburnMetroAutofacBootstrapper<AppViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<Repository.FeatureRepository>().As<Core.Repository.IFeatureRepository>().SingleInstance();

#if (SP2013)
            Backend SelectedBackend = Backend.SP2013;
#elif (DEMO)
            Backend SelectedBackend = Backend.DEMO;
#else
            Backend SelectedBackend = BackendSelector.EvaluateBackend();
#endif

            switch (SelectedBackend)
            {
                //case Backend.SP2007:
                //    break;
                //case Backend.SP2010:
                //    break;
                case Backend.SP2013:
                    builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
                    break;
                case Backend.SP2016:
                    builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
                    break;
                case Backend.SP2019:
                    builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
                    break;
                case Backend.DEMO:
                default:
                    builder.RegisterType<Backends.Demo.Services.DemoDataService>().As<Core.Services.IDataService>().SingleInstance();
                    break;
            }

            var assembly = typeof(AppViewModel).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
                .AsSelf()
                .SingleInstance();

            //builder.RegisterType<FeatureDefinitionListViewModel>().SingleInstance();
            // builder.RegisterType<NavigationBarViewModel>().InstancePerRequest();
        }
    }
}
