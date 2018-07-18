using System;
using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Common;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : Conductor<Screen>.Collection.OneActive
        , Caliburn.Micro.IHandle<ConfirmationRequest>
        , Caliburn.Micro.IHandle<OpenWindow<ActivatedFeature>>
        , Caliburn.Micro.IHandle<OpenWindow<FeatureDefinition>>
        , Caliburn.Micro.IHandle<OpenWindow<Location>>
        , Caliburn.Micro.IHandle<ProgressMessage>
    {

        private readonly IEventAggregator eventAggregator;
        private Guid recentLoadTask;
        private readonly IWindowManager windowManager;
        private Akka.Actor.IActorRef taskManagerActorRef;
        IFeatureRepository repository;
        IDataService dataService;
        // private IActorRef viewModelSyncActorRef;
        public AppViewModel(
            IWindowManager windowManager,
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService)
        {
            // Load settings at the very beginning, so that they are up to date
            LoadSettings();

            DisplayName = "Feature Admin 3 for SharePoint 2013";

            this.windowManager = windowManager;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            this.repository = repository;
            this.dataService = dataService;
            StatusBarVm = new StatusBarViewModel(eventAggregator);

            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator, repository);

            LocationListVm = new LocationListViewModel(eventAggregator, repository);
            UpgradeListVm = new UpgradeListViewModel(eventAggregator, repository);
            CleanupListVm = new CleanupListViewModel(eventAggregator, repository);

            Items.Add(LocationListVm);
            Items.Add(UpgradeListVm);
            Items.Add(CleanupListVm);

            ActivateItem(LocationListVm);            

            ActivatedFeatureVm = new ActivatedFeatureViewModel(eventAggregator, repository);

            LogVm = new LogViewModel(eventAggregator);

            InitializeActors();

            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleInitialLoad);
        }

        public ActivatedFeatureViewModel ActivatedFeatureVm { get; private set; }
        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }
        public UpgradeListViewModel UpgradeListVm { get; private set; }
        public CleanupListViewModel CleanupListVm { get; private set; }
        public LogViewModel LogVm { get; private set; }

        public StatusBarViewModel StatusBarVm { get; private set; }
        public void Handle<T>(OpenWindow<T> message) where T : class
        {
            throw new System.Exception("Todo - convert dto to object and then detail view ...");
            // OpenWindow(message.ViewModel);
        }

        public void Handle(ProgressMessage message)
        {
            if (message.Progress >= 1d && message.TaskId == recentLoadTask)
            {
                CanReLoad = true;
            }
        }

        private void TriggerFarmLoadTask(string taskTitle)
        {
            CanReLoad = false;
            recentLoadTask = Guid.NewGuid();
            taskManagerActorRef.Tell(new Core.Messages.Request.LoadTask(recentLoadTask, taskTitle, Core.Factories.LocationFactory.GetDummyFarmForLoadCommand()));

            // fyi - to do this via eventAggregator would also allow to trigger reload from other viewModels, e.g. also for single locations, 
            // but at least the initial farm load cannot be triggered by eventaggregator, because it is not listening at that 
            // early point in time, so all triggers from this viewModel directly call the actor ...
            // eventAggregator.PublishOnUIThread(new LoadTask(taskTitle, Core.Factories.LocationFactory.GetDummyFarmForLoadCommand()));
        }

        public void OpenWindow(DetailViewModel viewModel)
        {
            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            this.windowManager.ShowWindow(viewModel, null, settings);
        }

        public void Handle(ConfirmationRequest message)
        {
            DialogViewModel dialogVm = new DialogViewModel(eventAggregator, message);

            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            settings.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
            settings.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            // settings.Title = "window title";
            // settings.Icon = new BitmapImage(new Uri("pack://application:,,,/MyApplication;component/Assets/myicon.ico"));

            this.windowManager.ShowDialog(dialogVm, null, settings);
        }

        public bool CanReLoad { get; private set; }

        private bool elevatedPrivileges;
        private bool force;
        public bool ElevatedPrivileges
        {
            get
            {
                return elevatedPrivileges;
            }
            set
            {
                elevatedPrivileges = value;
                UpdateSettings();
            }
        }
        public bool Force
        {
            get
            {
                return force;
            }
            set
            {
                force = value;
                UpdateSettings();
            }
        }


        public void ReLoad()
        {
            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleReload);
        }

        private void InitializeActors()
        {
            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(
                Akka.Actor.Props.Create(() =>
                new Actors.Tasks.TaskManagerActor(
                    eventAggregator,
                    repository,
                    dataService,
                    elevatedPrivileges,
                    force)));
        }
        public void Handle(OpenWindow<ActivatedFeature> message)
        {
            OpenWindow(message.ViewModel.ToDetailViewModel());
        }

        public void Handle(OpenWindow<FeatureDefinition> message)
        {
            var vm = message.ViewModel;
            var activatedFeatures = repository.GetActivatedFeatures(vm);
            OpenWindow(vm.ToDetailViewModel(activatedFeatures));
        }

        public void Handle(OpenWindow<Location> message)
        {
            var vm = message.ViewModel;
            var activatedFeatures = repository.GetActivatedFeatures(vm);
            OpenWindow(vm.ToDetailViewModel(activatedFeatures));
        }

        private void UpdateSettings()
        {
            var settings = new Core.Messages.Completed.SettingsChanged(elevatedPrivileges, force);

            eventAggregator.PublishOnUIThread(settings);

            // Handling Application Settings in WPF, see https://msdn.microsoft.com/en-us/library/a65txexh(v=vs.140).aspx
            Properties.Settings.Default.elevatedPrivileges = elevatedPrivileges;
            Properties.Settings.Default.force = force;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            // Handling Application Settings in WPF, see https://msdn.microsoft.com/en-us/library/a65txexh(v=vs.140).aspx
            elevatedPrivileges = Properties.Settings.Default.elevatedPrivileges;
            force = Properties.Settings.Default.force;
        }
    }
}
