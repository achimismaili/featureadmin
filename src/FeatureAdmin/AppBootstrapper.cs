using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using FeatureAdmin.ViewModels;

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
            builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
#else
            builder.RegisterType<Backends.Demo.Services.DemoDataService>().As<Core.Services.IDataService>().SingleInstance();
#endif

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
