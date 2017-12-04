using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.UIModels;
using System;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : IHaveDisplayName,
        Caliburn.Micro.IHandle<LoadItem<Location>>,
        Caliburn.Micro.IHandle<LoadItem<FeatureDefinition>>,
        Caliburn.Micro.IHandle<OpenWindow>
    {

        private readonly IEventAggregator eventAggregator;

        private readonly IWindowManager windowManager;

        private IActorRef taskManagerActorRef;
        private IActorRef viewModelSyncActorRef;
        public AppViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            DisplayName = "Feature Admin 3 for SharePoint 2013";

            this.windowManager = windowManager;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);



            // NavigationBarVm = new NavigationBarViewModel(eventAggregator);
            StatusBarVm = new StatusBarViewModel(eventAggregator);

            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator);

            LocationListVm = new LocationListViewModel(eventAggregator);

            InitializeActors();

            InitializeFarmLoad();
        }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }

        public StatusBarViewModel StatusBarVm { get; private set; }
        public string DisplayName { get; set; }

        public void Handle(LoadItem<Location> loadCommand)
        {
            taskManagerActorRef.Tell(new LoadLocationQuery(loadCommand.Item));
        }

        private void InitializeActors()
        {
            //   loadFeatureDefinitionActorRef = ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new LoadActor())); 
            viewModelSyncActorRef = ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new ViewModelSyncActor(eventAggregator)));

            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new TaskManagerActor(viewModelSyncActorRef)));
            //featureToggleActorRef;

            //_chartingActorRef =
            //        ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new LineChartingActor(PlotModel)));

            //    _stocksCoordinatorActorRef =
            //        ActorSystemReference.ActorSystem.ActorOf(
            //            Props.Create(() => new StocksCoordinatorActor(
            //                _chartingActorRef)), "StocksCoordinator");
        }

        public void Handle(LoadItem<FeatureDefinition> message)
        {
            taskManagerActorRef.Tell(new LoadFeatureDefinitionQuery());
        }

        public void Handle(OpenWindow message)
        {
            OpenWindow(message.ViewModel);
        }

        public void InitializeFarmLoad()
        {


            Handle(new LoadItem<FeatureDefinition>());
            Handle(new LoadItem<Location>(LocationFactory.GetDummyFarmForLoadCommand()));
        }

        public void OpenWindow(DetailViewModel viewModel)
        {
            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            this.windowManager.ShowWindow(viewModel, null, settings);
        }
    }
}
