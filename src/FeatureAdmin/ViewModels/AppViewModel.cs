using System;
using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Common;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : Screen, IHaveDisplayName
        ,Caliburn.Micro.IHandle<OpenWindow<ActivatedFeature>>
        ,Caliburn.Micro.IHandle<OpenWindow<FeatureDefinition>>
        ,Caliburn.Micro.IHandle<OpenWindow<Location>>
        ,Caliburn.Micro.IHandle<ProgressMessage>
    {

        private readonly IEventAggregator eventAggregator;
        private Guid recentLoadTask;
        private readonly IWindowManager windowManager;
        private Akka.Actor.IActorRef taskManagerActorRef;
        IFeatureRepository repository;
        
        // private IActorRef viewModelSyncActorRef;
        public AppViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IFeatureRepository repository)
        {
            DisplayName = "Feature Admin 3 for SharePoint 2013";

            this.windowManager = windowManager;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            this.repository = repository;

            StatusBarVm = new StatusBarViewModel(eventAggregator);

            FeatureDefinitionListVm = new FeatureDefinitionListViewModel(eventAggregator, repository);

            LocationListVm = new LocationListViewModel(eventAggregator, repository);

            ActivatedFeatureVm = new ActivatedFeatureViewModel(eventAggregator, repository);

            LogVm = new LogViewModel(eventAggregator);

            InitializeActors();

            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleInitialLoad);
        }

        public string DisplayName { get; set; }

        public ActivatedFeatureViewModel ActivatedFeatureVm { get; private set; }
        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }

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
            taskManagerActorRef.Tell(new LoadTask(recentLoadTask, taskTitle, Core.Factories.LocationFactory.GetDummyFarmForLoadCommand()));

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

        public bool CanReLoad { get; private set; }

        public void ReLoad()
        {
            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleReload);
        }

        private void InitializeActors()
        {
            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new TaskManagerActor(eventAggregator, repository)));
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
    }
}
