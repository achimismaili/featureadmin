using Autofac;
using FA.SharePoint;
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

            //builder.RegisterType<FeatureRepository>()
            //  .As<IFeatureRepository>();

            //builder.RegisterType<NavigationViewModel>()
            //  .As<INavigationViewModel>();

            //builder.RegisterType<FriendDataProvider>()
            //  .As<IFriendDataProvider>();

            //builder.RegisterType<NavigationDataProvider>()
            //  .As<INavigationDataProvider>();

            //builder.RegisterType<FileDataService>()
            //  .As<IDataService>();

            return builder.Build();
    }
  }
}
