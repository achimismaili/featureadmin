using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Tasks;
using FeatureAdmin.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using FeatureAdmin.Core;

namespace FeatureAdmin.Actors
{
    public class TaskManagerActor : ReceiveActor,
                Caliburn.Micro.IHandle<LoadTask>,
        Caliburn.Micro.IHandle<ClearItemsReady>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IEventAggregator eventAggregator;
        private readonly Dictionary<Guid, IActorRef> taskActors;

        public TaskManagerActor(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            taskActors = new Dictionary<Guid, IActorRef>();
        }

        /// <summary>
        /// Forward message from Caliburn to akka task actor
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ClearItemsReady message)
        {
            var taskActor = taskActors[message.TaskId];

            taskActor.Tell(message);
        }


        /// <summary>
        /// send load task to load task actor
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>in the future, this may be enhanced with a start
        /// location, so that it might not only have to be a full farm reload
        /// </remarks>
        public void Handle(LoadTask message)
        {
            Guid newId = Guid.NewGuid();
            IActorRef newTaskActor =
           Context.ActorOf(LoadTaskActor.Props(eventAggregator,
           message.Title, newId, message.StartLocation), newId.ToString());

           taskActors.Add(newId, newTaskActor);
        }
    }
}
