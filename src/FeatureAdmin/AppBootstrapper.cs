using Caliburn.Metro;
using Caliburn.Micro;
using FeatureAdmin.Core.DataServices.Contracts;
using FeatureAdmin.ViewModels;

namespace FeatureAdmin
{
    public class AppBootstrapper : CaliburnMetroCompositionBootstrapper<ViewModels.AppViewModel>
    {
        private SimpleContainer container;

        protected override void Configure()
        {
            base.Configure();

            container = new SimpleContainer();

            container.Instance(container);

            container
                .Singleton<IWindowManager, WindowManager>()
                  .Singleton<IServiceWrapper, Service.ServiceWrapper>()
                .Singleton<IDataService, DataServices.Demo.DemoDataService>()
                .Singleton<IEventAggregator, EventAggregator>();

            container
                .PerRequest<Core.Models.Contracts.IActivatedFeature, Core.Models.ActivatedFeature>()
                .PerRequest<Core.Models.Contracts.IFeatureDefinition, Core.Models.FeatureDefinition>()
                .PerRequest<Core.Models.Contracts.ILocation, Core.Models.Location>()
                .PerRequest<LocationListViewModel>()
                ;

        }
    }
}
