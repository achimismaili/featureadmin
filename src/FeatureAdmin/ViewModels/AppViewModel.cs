using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actor;
using FeatureAdmin.Actor.Actors;
using FeatureAdmin.Actor.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.ObjectModel;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IActorRef viewModelSyncActorRef;
        private IActorRef taskManagerActorRef;

        private bool _isSettingsFlyoutOpen;
        private readonly IEventAggregator eventAggregator;



        private string maus;

        public AppViewModel(IEventAggregator eventAggregator)
        {
            Locations = new ObservableCollection<Location>();
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            Maus = "piep1";

            LocationList = new LocationListViewModel(eventAggregator);

            InitializeActors();

            taskManagerActorRef.Tell( new LoadTaskMessage(Location.GetFarm(Guid.Empty)));
        }

        public ObservableCollection<Location> Locations;

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }

        public LocationListViewModel LocationList { get; }

        public string Maus
        {
            get { return maus; }
            set
            {
                maus = value;
                NotifyOfPropertyChange(() => Maus);
            }
        }

        public void OpenSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        private void InitializeActors()
        {
            //   loadFeatureDefinitionActorRef = ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new LoadActor())); 
            viewModelSyncActorRef = ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new ViewModelSyncActor(Locations)));
            
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
