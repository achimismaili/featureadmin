using Caliburn.Metro;
using Caliburn.Micro;
using FeatureAdmin.Core.Repositories.Contracts;


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
                //  .Singleton<IRepositoryRead, DemoRepositoryRead>()
                //.Singleton<IRepositoryCommand, DemoRepositoryCommand>();
                .Singleton<IEventAggregator, EventAggregator>();

        }
    }
}
