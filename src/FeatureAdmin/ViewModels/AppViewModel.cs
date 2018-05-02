using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Messages;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : IHaveDisplayName,
        Caliburn.Micro.IHandle<OpenWindow>
    {

        private readonly IEventAggregator eventAggregator;

        private readonly IWindowManager windowManager;

        private Akka.Actor.IActorRef taskManagerActorRef;
        // private IActorRef viewModelSyncActorRef;
        public AppViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            DisplayName = "Feature Admin 3 for SharePoint 2013";

            this.windowManager = windowManager;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);


            StatusBarVm = new StatusBarViewModel(eventAggregator);

            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator);

            LocationListVm = new LocationListViewModel(eventAggregator);

            ActivatedFeatureVm = new ActivatedFeatureViewModel(eventAggregator);

            LogVm = new LogViewModel(eventAggregator);

            InitializeActors();

            InitializeFarmLoad();

        }

        public string DisplayName { get; set; }

        public ActivatedFeatureViewModel ActivatedFeatureVm { get; private set; }
        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }

        public LogViewModel LogVm { get; private set; }

        public StatusBarViewModel StatusBarVm { get; private set; }
        public void Handle(OpenWindow message)
        {
            OpenWindow(message.ViewModel);
        }

        public void InitializeFarmLoad()
        {
            // Commented out, because calling via Caliburn Eventbus is too early on application start ... 
            // would have to find a way to verify, the actor is already listening at that time ...
            // TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleInitialLoad);

            taskManagerActorRef.Tell(
                new LoadTask(Common.Constants.Tasks.TaskTitleInitialLoad,
                Core.Factories.LocationFactory.GetDummyFarmForLoadCommand())
                );
        }

        public void OpenWindow(DetailViewModel viewModel)
        {
            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            this.windowManager.ShowWindow(viewModel, null, settings);
        }

        public void OpenDialog(string title, string message)
        {
            DialogViewModel dialogVm = new DialogViewModel(title, message);

            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            settings.ResizeMode = System.Windows.ResizeMode.NoResize;
            settings.Width = 300;
            settings.Height = 200;
            // settings.Title = "window title";
            // settings.Icon = new BitmapImage(new Uri("pack://application:,,,/MyApplication;component/Assets/myicon.ico"));

            this.windowManager.ShowDialog(dialogVm, null, settings);
        }


        public void ReLoad()
        {
            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleReload);
        }

        private void InitializeActors()
        {
            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new TaskManagerActor(eventAggregator)));
        }
        private void TriggerFarmLoadTask(string taskTitle)
        {

            eventAggregator.PublishOnUIThread(new LoadTask(taskTitle, Core.Factories.LocationFactory.GetDummyFarmForLoadCommand()));
        }
    }
}
