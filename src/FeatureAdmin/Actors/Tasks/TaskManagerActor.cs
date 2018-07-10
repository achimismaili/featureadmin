using Akka.Actor;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Core.Models.Tasks;
using System;
using System.Collections.Generic;
using FeatureAdmin.Core.Repository;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Messages;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Actors.Tasks
{
    public class TaskManagerActor : ReceiveActor
               // ,Caliburn.Micro.IHandle<LoadTask>
               , Caliburn.Micro.IHandle<FeatureToggleRequest>
         , Caliburn.Micro.IHandle<SettingsChanged>
        , Caliburn.Micro.IHandle<Confirmation>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IDataService dataService;
        private readonly IEventAggregator eventAggregator;
        private readonly IFeatureRepository repository;
        private readonly Dictionary<Guid, IActorRef> taskActors;
        public TaskManagerActor(
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService,
            bool elevatedPrivileges,
            bool force
            )
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            this.repository = repository;
            this.dataService = dataService;
            this.elevatedPrivileges = elevatedPrivileges;
            this.force = force;

            taskActors = new Dictionary<Guid, IActorRef>();

            Receive<LoadTask>(message => Receive(message));
        }

        private bool elevatedPrivileges { get; set; }
        private bool force { get; set; }
        /// <summary>
        /// send load task to load task actor
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>in the future, this may be enhanced with a start
        /// location, so that it might not only have to be a full farm reload
        /// </remarks>
        public void Receive(LoadTask message)
        {
            IActorRef newTaskActor =
            Context.ActorOf(LoadTaskActor.Props(
                eventAggregator, 
                repository,
                dataService,
                message.Id), message.Id.ToString());

            taskActors.Add(message.Id, newTaskActor);

            var requestWithCurrentSettings = message.GetWithUpdatedSettings(elevatedPrivileges);

            // trigger feature toggle request
            newTaskActor.Tell(requestWithCurrentSettings);
        }

        public void Handle(FeatureToggleRequest message)
        {
            // as this comes from WPF, no akka context available here yet.
            IActorRef newTaskActor =
            ActorSystemReference.ActorSystem.ActorOf(
                FeatureTaskActor.Props(
                    eventAggregator,
                    repository,
                    dataService,
                    message.TaskId
                    ));

            taskActors.Add(message.TaskId, newTaskActor);

            var requestWithCorrectSettings = message.GetWithUpdatedSettings(force, elevatedPrivileges);

            // trigger feature toggle request
            newTaskActor.Tell(requestWithCorrectSettings);
        }

        public void Handle(SettingsChanged message)
        {
            elevatedPrivileges = message.ElevatedPrivileges;
            force = message.Force;
        }

        /// <summary>
        /// Handles confirmation from a dialog box and forwards to the waiting task
        /// </summary>
        /// <param name="message"></param>
        public void Handle([NotNull] Confirmation message)
        {
            if (taskActors.ContainsKey(message.TaskId))
            {
                taskActors[message.TaskId].Tell(message);
            }
            else
            {
                eventAggregator.PublishOnUIThread(
                    new Messages.LogMessage(Core.Models.Enums.LogLevel.Error,
                    string.Format("Internal error. Confirmed task with task id {0} was not found anymore!",message.TaskId)
                    ));
            }
        }
    }
}
