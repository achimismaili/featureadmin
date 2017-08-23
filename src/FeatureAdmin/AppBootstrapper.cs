using Caliburn.Metro;
using Caliburn.Micro;
using FeatureAdmin.Core.Contracts.Repositories;

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
                //  .Singleton<ISharePointRepositoryRead, SharePointRepositoryRead>()
                //.Singleton<ISharePointRepositoryCommand, SharePointRepositoryCommand>();
                .Singleton<IEventAggregator, EventAggregator>();

              }
    }
}
