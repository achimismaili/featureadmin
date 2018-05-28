using Akka.Actor;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models.Tasks;
using System;
using System.Collections.Generic;
using FeatureAdmin.Repository;
using FeatureAdmin.Core.Messages.Request;

namespace FeatureAdmin.Actors
{
    public class TaskManagerActor : ReceiveActor
               // ,Caliburn.Micro.IHandle<LoadTask>
               , Caliburn.Micro.IHandle<FeatureToggleRequest>
         , Caliburn.Micro.IHandle<SettingsChanged>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IEventAggregator eventAggregator;
        private readonly IFeatureRepository repository;
        private readonly Dictionary<Guid, IActorRef> taskActors;
        public TaskManagerActor(
            IEventAggregator eventAggregator
            , IFeatureRepository repository
            , bool elevatedPrivileges
            , bool force
            )
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            this.repository = repository;

            this.elevatedPrivileges = elevatedPrivileges;
            this.force = force;

            taskActors = new Dictionary<Guid, IActorRef>();

            Receive<LoadTask>(message => Handle(message));
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
        public void Handle(LoadTask message)
        {
            IActorRef newTaskActor =
            ActorSystemReference.ActorSystem.ActorOf(LoadTaskActor.Props(eventAggregator, repository,
           message.Title, message.Id, message.StartLocation), message.Id.ToString());

            taskActors.Add(message.Id, newTaskActor);
        }

        public void Handle(FeatureToggleRequest message)
        {
            IActorRef newTaskActor =
            ActorSystemReference.ActorSystem.ActorOf(
                FeatureTaskActor.Props(
                    eventAggregator
                    , repository
                    , message.TaskId
                    , elevatedPrivileges
                    , force
                    )
                    );

            taskActors.Add(message.TaskId, newTaskActor);

            // trigger feature toggle request
            newTaskActor.Tell(message);
        }

        public void Handle(SettingsChanged message)
        {
            elevatedPrivileges = message.ElevatedPrivileges;
            force = message.Force;
        }
    }
}
