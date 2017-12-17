using Akka.Actor;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Tasks;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Actors
{
    public class TaskManagerActor : ReceiveActor
    {
        private readonly IEventAggregator eventAggregator;
        private IActorRef viewModelSyncActorRef;
        private readonly IActorRef featureDefinitionActor;
        private readonly Dictionary<Guid, IActorRef> locationActors;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Dictionary<Guid, AdminTask> tasks;

        public TaskManagerActor(IActorRef viewModelSyncActorRef, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            locationActors = new Dictionary<Guid, IActorRef>();
            tasks = new Dictionary<Guid, AdminTask>();
            this.viewModelSyncActorRef = viewModelSyncActorRef;

            featureDefinitionActor =
                    Context.ActorOf(
                        Props.Create(() => new FeatureDefinitionManagerActor(viewModelSyncActorRef)),
                                     "FeatureDefinitionManagerActor");


            Receive<NewTask>(message => HandleNewTask(message));

            Receive<ItemUpdated<Location>>(message => LocationUpdated(message));

           // Receive<LoadFeatureDefinitionQuery>(message => LoadFeatureDefinitions(message));
        }

        private void HandleNewTask(NewTask message)
        {
            var logNewTask = new Messages.LogMessage(Core.Models.Enums.LogLevel.Information,
                string.Format("Started '{1}' (ID: '{0}')", message.Task.Id, message.Task.Title)
                );

            // log new task started
            eventAggregator.BeginPublishOnUIThread(logNewTask);

            // initiate status bar to 0 percent progress
            var initialStatus = new Messages.ProgressMessage(message.Task);
            eventAggregator.BeginPublishOnUIThread(initialStatus);

            // add new task to queue
            tasks.Add(message.Task.Id, message.Task);

            // delegate tasks and todos depending on task type
            switch (message.TaskType)
            {
                case Core.Models.Enums.TaskType.Load:
                    HandleTaskOfTypeLoad(message.Task);
                    break;
                case Core.Models.Enums.TaskType.Act:
                    break;
                case Core.Models.Enums.TaskType.Update:
                    break;
                case Core.Models.Enums.TaskType.Uninstall:
                    break;
                default:
                    var logUnknownTaskType = new Messages.LogMessage(Core.Models.Enums.LogLevel.Error,
                string.Format("Canceling due to unknown task type, task '{1}' (ID: '{0}')", message.Task.Id, message.Task.Title)
                );
                    eventAggregator.BeginPublishOnUIThread(logUnknownTaskType);
                    tasks.Remove(message.Task.Id);
                    break;
            }
        }
            private void HandleTaskOfTypeLoad(AdminTask task)
        {
            var logNewTask = new Messages.LogMessage(Core.Models.Enums.LogLevel.Information,
               string.Format("{2}% @Task '{1}' (ID: '{0}')", task.Id, task.Title, task.PercentCompleted)
               );

            // clean all feature definition and location collections
            // subtask?

            // load feature definitions


            // load locations
        }






        private void LoadFeatureDefinitions(LoadFeatureDefinitionQuery message)
        {
            featureDefinitionActor.Tell(message);
        }

        private void LoadTask(LoadLocationQuery message)
        {
            _log.Debug("Entered TaskManager-LoadTask");
            if (message == null || message.Location == null)
            {
                _log.Error("LoadTask message or location was null");
                return;
            }

            var locationId = message.Location.Id;

            bool locationActorNeedsCreating = !locationActors.ContainsKey(locationId);

            if (locationActorNeedsCreating)
            {
                IActorRef newLocationActor =
                    Context.ActorOf(
                        Props.Create(() => new LocationManagerActor(viewModelSyncActorRef, locationId)),
                                     locationId.ToString());

                locationActors.Add(locationId, newLocationActor);

                // newLocationActor.Tell(message);
            }

            locationActors[locationId].Tell(message);
        }

        /// <summary>
        /// This message is received from a location manager actor when child location is loaded. 
        /// this method will send a load task to the correctly responsible actor 
        /// </summary>
        /// <param name="message"></param>
        private void LocationUpdated(ItemUpdated<Location> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            LoadTask(new LoadLocationQuery(message.Item));
        }
    }
}
