using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class FeatureTaskActor : BaseTaskActor
        , Caliburn.Micro.IHandle<SettingsChanged>
    {
        public Dictionary<Guid, bool> jobsCompleted;

        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Dictionary<Guid, IActorRef> executingActors;
        private readonly IFeatureRepository repository;

        private bool elevatedPrivileges;
        private bool force;


        public FeatureTaskActor(
            IEventAggregator eventAggregator
            , IFeatureRepository repository
            , Guid taskId, bool elevatedPrivileges, bool force)
            : base(eventAggregator, taskId)
        {
            this.eventAggregator.Subscribe(this);

            executingActors = new Dictionary<Guid, IActorRef>();
            this.repository = repository;

            this.elevatedPrivileges = elevatedPrivileges;
            this.force = force;

            jobsCompleted = new Dictionary<Guid, bool>();

            Receive<FeatureToggleRequest>(message => HandleFeatureToggleRequest(message));
            Receive<FeatureDeactivationCompleted>(message => HandleFeatureDeactivationCompleted(message));
            Receive<FeatureActivationCompleted>(message => HandleFeatureActivationCompleted(message));
        }

        public override double PercentCompleted
        {
            get
            {
                double completed = jobsCompleted.Where(j => j.Value == true).Count();
                double total = jobsCompleted.Count();

                return completed / total;
            }
        }

        public override string StatusReport
        {
            get
            {
                return string.Format("'{0}' (ID: '{1}') - {2} of {3} task(s) completed, progress {4:F0}% \nelapsed time: {5}",
                    Title,
                    Id,
                    jobsCompleted.Where(j => j.Value == true).Count(),
                    jobsCompleted.Count(),
                    PercentCompleted * 100,
                    ElapsedTime
                    );
            }
        }

        /// <summary>
        /// Props provider
        /// </summary>
        /// <param name="eventAggregator">caliburn micro event aggregator</param>
        /// <param name="repository">feature repository</param>
        /// <param name="id">task id</param>
        /// <returns></returns>
        /// <remarks>
        /// see also https://getakka.net/articles/actors/receive-actor-api.html
        /// </remarks>
        public static Props Props(IEventAggregator eventAggregator, IFeatureRepository repository, Guid taskId, bool elevatedPrivileges, bool force)
        {
            return Akka.Actor.Props.Create(() => new FeatureTaskActor(eventAggregator, repository, taskId, elevatedPrivileges, force));
        }

        public void Handle(SettingsChanged message)
        {
            elevatedPrivileges = message.ElevatedPrivileges;
            force = message.Force;
        }

        private void HandleFeatureDeactivationCompleted([NotNull] FeatureDeactivationCompleted message)
        {
            throw new NotImplementedException();
        }

        private void HandleFeatureActivationCompleted([NotNull] FeatureActivationCompleted message)
        {
            throw new NotImplementedException();
        }

        private void HandleFeatureToggleRequest([NotNull] FeatureToggleRequest message)
        {
            _log.Debug("Entered HandleFeatureToggleRequest with Id " + Id.ToString());


            IEnumerable<Location> actionLocations;

            if (message.Activate)
            {
                // retrieve locations where to activate 
                actionLocations = repository.GetLocationsCanActivate(message.FeatureDefinition, message.Location);
            }
            else
            {
                // retrieve locations where to deactivate 
                actionLocations = repository.GetLocationsCanDeactivate(message.FeatureDefinition, message.Location);
            }


            // create Location actors and trigger feature actions

            foreach (Location l in actionLocations)
            {

                var toggleRequestForThisLocation = new FeatureToggleRequest(
                    message.FeatureDefinition
                    , l
                    , message.Activate
                    );

                if (!executingActors.ContainsKey(l.Id))
                {
                    IActorRef newLocationActor =
                        Context.ActorOf(Context.DI().Props<LocationActor>());

                    executingActors.Add(l.Id, newLocationActor);

                    newLocationActor.Tell(toggleRequestForThisLocation);
                }
                else
                {
                    executingActors[l.Id].Tell(toggleRequestForThisLocation);
                }
            }
        }
    }
}
