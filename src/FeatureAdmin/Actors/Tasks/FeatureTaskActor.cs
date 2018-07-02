using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using Caliburn.Micro;
using FeatureAdmin.Actors;
using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Messages;
using FeatureAdmin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Core.Models.Tasks
{


    public class FeatureTaskActor : BaseTaskActor
    {
        public Dictionary<Guid, bool> jobsCompleted;

        private List<FeatureToggleRequest> featureToggleRequestsToBeConfirmed;


        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Dictionary<Guid, IActorRef> executingActors;
        private readonly IFeatureRepository repository;
        private readonly IDataService dataService;

        public FeatureTaskActor(
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService,
            Guid taskId)
            : base(eventAggregator, taskId)
        {
            this.eventAggregator.Subscribe(this);

            executingActors = new Dictionary<Guid, IActorRef>();
            this.repository = repository;
            this.dataService = dataService;
            jobsCompleted = new Dictionary<Guid, bool>();

            Receive<Confirmation>(message => HandleConfirmation(message));
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
        public static Props Props(IEventAggregator eventAggregator, 
            IFeatureRepository repository,
            IDataService dataService,
            Guid taskId)
        {
            return Akka.Actor.Props.Create(() => 
            new FeatureTaskActor(
                eventAggregator, 
                repository,
                dataService,
                taskId));
        }

        private void HandleFeatureDeactivationCompleted([NotNull] FeatureDeactivationCompleted message)
        {
            bool success;

            if (string.IsNullOrEmpty(message.Error))
            {
                success = true;
                repository.RemoveActivatedFeature(message.FeatureId, message.LocationReference);
            }
            else
            {
                success = false;
            }

            jobsCompleted.Add(message.LocationReference, success);

            SendProgress();
        }

        private void HandleFeatureActivationCompleted([NotNull] FeatureActivationCompleted message)
        {
            bool success;

            if (string.IsNullOrEmpty(message.Error))
            {
                success = true;
                repository.AddActivatedFeature(message.ActivatedFeature);
            }
            else
            {
                success = false;
            }

            jobsCompleted.Add(message.LocationReference, success);

            SendProgress();
        }

        private void HandleFeatureToggleRequest([NotNull] FeatureToggleRequest message)
        {
            _log.Debug("Entered HandleFeatureToggleRequest with Id " + Id.ToString());

            Title = message.Title;

            IEnumerable<Location> targetLocations;

            if (message.Activate)
            {
                // retrieve locations where to activate 
                targetLocations = repository.GetLocationsCanActivate(message.FeatureDefinition, message.Location);
            }
            else
            {
                // retrieve locations where to deactivate 
                targetLocations = repository.GetLocationsCanDeactivate(message.FeatureDefinition, message.Location);
            }

            featureToggleRequestsToBeConfirmed = new List<FeatureToggleRequest>();
            
            foreach (Location l in targetLocations)
            {

                var toggleRequestForThisLocation = new FeatureToggleRequest(
                    message.FeatureDefinition
                    , l
                    , message.Activate
                    , message.Force
                    , message.ElevatedPrivileges
                    );
                featureToggleRequestsToBeConfirmed.Add(toggleRequestForThisLocation);
            }

            var action = message.Activate ? "activate" : "deactivate";

            var confirmRequest = new ConfirmationRequest(
                    Title,
                    string.Format("Please confirm to {0} feature {1} in location {2}\nFeatureAdmin found '{3}' location(s) where to {0}.", 
                        action,
                        message.FeatureDefinition.ToString(), 
                        message.Location.ToString(),
                        targetLocations.Count()),
                    Id
                    );


            eventAggregator.PublishOnUIThread(confirmRequest);
        }

        private void HandleConfirmation(Confirmation message)
        {
            foreach (FeatureToggleRequest ftr in featureToggleRequestsToBeConfirmed)
            {

                var locationId = ftr.Location.Id;

                // create Location actors and trigger feature actions

                if (!executingActors.ContainsKey(locationId))
                {
                    

                    IActorRef newLocationActor =
                        // Context.ActorOf(Context.DI().Props<LocationActor>());
                        ActorSystemReference.ActorSystem.ActorOf(LocationActor.Props(
           dataService), locationId.ToString());


                    executingActors.Add(locationId, newLocationActor);

                    newLocationActor.Tell(ftr);
                }
                else
                {
                    executingActors[ftr.Location.Id].Tell(ftr);
                }
            }
        }
    }
}
