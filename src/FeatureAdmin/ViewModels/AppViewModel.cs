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

    public class AppViewModel : Conductor<IWorkSpace>.Collection.OneActive, 
        IHaveDisplayName, 
        Caliburn.Micro.IHandle<LoadItem<Location>>,
        Caliburn.Micro.IHandle<LoadItem<FeatureDefinition>>
    {
        
        private readonly IEventAggregator eventAggregator;

        private IActorRef taskManagerActorRef;
        private IActorRef viewModelSyncActorRef;
        public AppViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "Feature Admin 3 for SharePoint 2013";

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            NavigationBarVm = new NavigationBarViewModel(eventAggregator);
            StatusBarVm = new StatusBarViewModel(eventAggregator);

            ActivationVm = new WorkSpaces.ActivationViewModel(eventAggregator);
            UpgradeVm = new WorkSpaces.UpgradeViewModel(eventAggregator);

            ActivateItem(ActivationVm);

            

            Items.Add(UpgradeVm);

            InitializeActors();

            InitializeFarmLoad();

            
        }

        public WorkSpaces.ActivationViewModel ActivationVm { get; set; }
        public WorkSpaces.UpgradeViewModel UpgradeVm { get; set; }
        public CommandViewModel CommandVm { get; private set; }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }
       
        public NavigationBarViewModel NavigationBarVm { get; private set; }
        public StatusBarViewModel StatusBarVm { get; private set; }
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

        public void InitializeFarmLoad()
        {
            

                Handle(new LoadItem<FeatureDefinition>());
            Handle(new LoadItem<Location>(LocationFactory.GetDummyFarmForLoadCommand()));
        }
    }
}
