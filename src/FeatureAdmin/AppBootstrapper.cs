using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using FeatureAdmin.Core.DataServices.Contracts;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.ViewModels;

namespace FeatureAdmin
{
    public class AppBootstrapper : CaliburnMetroAutofacBootstrapper<ViewModels.AppViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            // builder.RegisterType<Service.ServiceWrapper>().As<IServiceWrapper>().SingleInstance();
            // builder.RegisterType<DataServices.Demo.DemoDataService>().As<IDataService>().SingleInstance();

            //builder.RegisterType<ActivatedFeature>().As<IActivatedFeature>().InstancePerDependency();
            //builder.RegisterType<FeatureDefinition>().As<IFeatureDefinition>().InstancePerDependency();
            //builder.RegisterType<Location>().As<ILocation>().InstancePerDependency();

            var assembly = typeof(AppViewModel).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
                .AsSelf()
                .SingleInstance();
        }
    }
}
