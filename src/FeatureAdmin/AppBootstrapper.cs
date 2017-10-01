using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using FeatureAdmin.Core.Services;
using FeatureAdmin.ViewModels;

namespace FeatureAdmin
{
    public class AppBootstrapper : CaliburnMetroAutofacBootstrapper<ViewModels.AppViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<Backends.Services.DemoDataService>().As<IDataService>().SingleInstance();

            var assembly = typeof(AppViewModel).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<LocationListViewModel>().InstancePerLifetimeScope();
        }
    }
}
