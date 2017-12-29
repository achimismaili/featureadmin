using Akka.Actor;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using FeatureAdmin.UIModels;
using System;

namespace FeatureAdmin.ViewModels
{

    public class AppViewModel : IHaveDisplayName,
        Caliburn.Micro.IHandle<NewTask>,
        Caliburn.Micro.IHandle<OpenWindow>,
        Caliburn.Micro.IHandle<ClearItemsReady>
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

            LogVm = new LogViewModel(eventAggregator);

            InitializeActors();

            InitializeFarmLoad();

        }

        public FeatureDefinitionListViewModel FeatureDefinitionListVm { get; private set; }

        public LocationListViewModel LocationListVm { get; private set; }

        public LogViewModel LogVm { get; private set; }

        public StatusBarViewModel StatusBarVm { get; private set; }

        public string DisplayName { get; set; }

        private void InitializeActors()
        {
            taskManagerActorRef = ActorSystemReference.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new TaskManagerActor(eventAggregator)));
        }

        public void Handle(NewTask message)
        {
            taskManagerActorRef.Tell(message);
        }

        public void Handle(OpenWindow message)
        {
            OpenWindow(message.ViewModel);
        }

        public void ReLoad()
        {
            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleReload);
        }
        public void InitializeFarmLoad()
        {
            TriggerFarmLoadTask(Common.Constants.Tasks.TaskTitleInitialLoad);
        }

        private void TriggerFarmLoadTask(string taskTitle) { 
            var initialLoadTask = new Core.Models.Tasks.AdminTaskItems(taskTitle, Common.Constants.Tasks.PreparationStepsForLoad);

            Handle(new NewTask(initialLoadTask, Core.Models.Enums.TaskType.Load));
            //Handle(new LoadItem<FeatureDefinition>());
            //Handle(new LoadItem<Location>(LocationFactory.GetDummyFarmForLoadCommand()));
        }


        public void OpenWindow(DetailViewModel viewModel)
        {
            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            this.windowManager.ShowWindow(viewModel, null, settings);
        }

        public void Handle(ClearItemsReady message)
        {
            taskManagerActorRef.Tell(message);
        }
    }
}
