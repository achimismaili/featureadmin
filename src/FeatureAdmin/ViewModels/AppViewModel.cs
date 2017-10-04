using Caliburn.Micro;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        //private IActorRef loadFeatureDefinitionActorRef;
        //private IActorRef loadLocationActorRef;
        //private IActorRef featureToggleActorRef;

        private bool _isSettingsFlyoutOpen;
        private IEventAggregator eventAggregator;



        private string maus;

        public AppViewModel(IEventAggregator eventAggregator)
        {
            LocationList = new LocationListViewModel(eventAggregator);

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            Maus = "piep1";

            InitializeActors();
        }

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
         //loadLocationActorRef;
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
