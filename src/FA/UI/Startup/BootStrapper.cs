using Autofac;
using FA.Models;
using FA.Models.Interfaces;
using FA.SharePoint;
using FA.UI.Features;
using FA.UI.Locations;
using Prism.Events;

namespace FA.UI.Startup
{
    public class BootStrapper
  {
    public IContainer BootStrap()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<EventAggregator>()
        .As<IEventAggregator>().SingleInstance();

      //builder.RegisterType<MessageDialogService>()
      //  .As<IMessageDialogService>();

      builder.RegisterType<MainWindow>().AsSelf();
      builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<FeatureViewModel>()
              .As<IFeatureViewModel>();

            builder.RegisterType<FeaturesListViewModel>()
            .As<IFeaturesListViewModel>();

            builder.RegisterType<FeaturesRepository>()
             .As<IFeaturesRepository>();

            builder.RegisterType<FeatureDefinition>()
             .As<IFeatureDefinition>();

            builder.RegisterType<ActivatedFeature>()
             .As<IActivatedFeature>();

            builder.RegisterType<LocationViewModel>()
              .As<ILocationViewModel>();
            
            builder.RegisterType<LocationsListViewModel>()
              .As<ILocationsListViewModel>();

            builder.RegisterType<LocationsRepository>()
             .As<ILocationsRepository>();

            builder.RegisterType<FeatureParent>()
              .As<IFeatureParent>();

            return builder.Build();
    }
  }
}
