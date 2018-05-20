using System;
using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.Repository;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : IHaveDisplayName
        ,Caliburn.Micro.IHandle<OpenWindow<ActivatedFeature>>
        ,Caliburn.Micro.IHandle<OpenWindow<FeatureDefinition>>
        ,Caliburn.Micro.IHandle<OpenWindow<Location>>
    {

        private readonly IEventAggregator eventAggregator;

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
        public void Handle<T>(OpenWindow<T> message) where T : class
        {
            throw new System.Exception("Todo - convert dto to object and then detail view ...");
            // OpenWindow(message.ViewModel);
        }

        public void InitializeFarmLoad()
        {
            // repository.Reload(Common.Constants.Tasks.TaskTitleInitialLoad);

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
            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new TaskManagerActor(eventAggregator, repository)));
        }
        private void TriggerFarmLoadTask(string taskTitle)
        {

            eventAggregator.PublishOnUIThread(new LoadTask(taskTitle, Core.Factories.LocationFactory.GetDummyFarmForLoadCommand()));
        }

        public void Handle(OpenWindow<ActivatedFeature> message)
        {
            throw new NotImplementedException();
        }

        public void Handle(OpenWindow<FeatureDefinition> message)
        {
            throw new NotImplementedException();
        }

        public void Handle(OpenWindow<Location> message)
        {
            throw new NotImplementedException();
        }
    }
}
