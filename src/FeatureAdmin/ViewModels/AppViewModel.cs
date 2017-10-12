using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : PropertyChangedBase, IHaveDisplayName, Caliburn.Micro.IHandle<LoadCommand>
    {
        
        private readonly IEventAggregator eventAggregator;
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IActorRef taskManagerActorRef;
        private IActorRef viewModelSyncActorRef;
        public AppViewModel(IEventAggregator eventAggregator)
        {
            
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            FeatureDefinitionVm = new FeatureDefinitionViewModel(eventAggregator);

            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator);

            LocationVm = new LocationViewModel(eventAggregator);
            LocationListVm = new LocationListViewModel(eventAggregator);
            LogVm = new LogViewModel(eventAggregator);

            NavigationBarVm = new NavigationBarViewModel(eventAggregator);
            StatusBarVm = new StatusBarViewModel(eventAggregator);

            InitializeActors();

            Handle(new LoadCommand(SPLocation.GetDummyFarmForLoadCommand()));
        }
        public CommandViewModel CommandVm { get; private set; }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public FeatureDefinitionViewModel FeatureDefinitionVm { get; private set; }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationViewModel LocationVm { get; private set; }
        public LocationListViewModel LocationListVm { get; private set; }
        public LogViewModel LogVm { get; private set; }

        public NavigationBarViewModel NavigationBarVm { get; private set; }
        public StatusBarViewModel StatusBarVm { get; private set; }
        public void Handle(LoadCommand loadCommand)
        {
            taskManagerActorRef.Tell(new LoadLocationQuery(loadCommand.SPLocation));
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
    }
}
