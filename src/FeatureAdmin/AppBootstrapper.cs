using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
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
            builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
#elif (DEMO)
            builder.RegisterType<Backends.Demo.Services.DemoDataService>().As<Core.Services.IDataService>().SingleInstance();
#else
            string pathSp2013 = @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.dll";

            if (System.IO.File.Exists(pathSp2013))
            {
                System.Console.WriteLine("SharePoint 2013 assembly found");
                builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<Core.Services.IDataService>().SingleInstance();
            }
            else
            {
                System.Console.WriteLine("No SharePoint installation found, starting demo mode ...");
                builder.RegisterType<Backends.Demo.Services.DemoDataService>().As<Core.Services.IDataService>().SingleInstance();
            }

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
