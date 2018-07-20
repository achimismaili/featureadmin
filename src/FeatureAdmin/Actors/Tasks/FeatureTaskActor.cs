using Akka.Actor;
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
        /// <summary>
        /// null is not completed, true is completed successfully and false is failed to complete
        /// </summary>
        public Dictionary<Guid, bool> jobsCompleted;
        public int jobsTotal = 0;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IDataService dataService;
        private readonly Dictionary<Guid, IActorRef> executingActors;
        private readonly IFeatureRepository repository;
        private List<FeatureToggleRequest> featureToggleRequestsToBeConfirmed;
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
            Receive<DeactivateFeaturesRequest>(message => HandleDeactivateFeaturesRequest(message));
            Receive<FeatureToggleRequest>(message => HandleFeatureToggleRequest(message));
            Receive<UpgradeFeaturesRequest>(message => HandleUpgradeFeaturesRequest(message));
            Receive<FeatureDeactivationCompleted>(message => HandleFeatureDeactivationCompleted(message));
            Receive<FeatureActivationCompleted>(message => HandleFeatureActivationCompleted(message));
            Receive<FeatureUpgradeCompleted>(message => HandleFeatureUpgradeCompleted(message));
        }

        private void HandleFeatureUpgradeCompleted(FeatureUpgradeCompleted message)
        {
            _log.Debug("Entered HandleFeatureUpgradeCompleted with Id " + Id.ToString());

            throw new NotImplementedException();
        }

        private void HandleUpgradeFeaturesRequest(UpgradeFeaturesRequest message)
        {
            _log.Debug("Entered HandleUpgradeFeaturesRequest with Id " + Id.ToString());

            throw new NotImplementedException();
        }

        private void HandleDeactivateFeaturesRequest(DeactivateFeaturesRequest message)
        {
            _log.Debug("Entered HandleDeactivateFeaturesRequest with Id " + Id.ToString());

            throw new NotImplementedException();
        }

        public override double PercentCompleted
        {
            get
            {
                // double completedSuccess = jobsCompleted.Where(j => j.Value == true).Count();
                double totalCompleted = jobsCompleted.Count();
                return totalCompleted / jobsTotal;
            }
        }

        public override string StatusReport
        {
            get
            {
                return string.Format(
                    "'{0}' (ID: '{1}') - {2} of {3} (de)activation(s) completed successfully, progress {4:F0}% \nelapsed time: {5}",
                    Title,
                    Id,
                    jobsCompleted.Where(j => j.Value == true).Count(),
                    jobsTotal,
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
        public static Props Props(
            IEventAggregator eventAggregator,
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

        protected override void HandleCancelation(CancelMessage cancelMessage)
        {
            _log.Debug("Entered HandleCancelation with Id " + Id.ToString());

            foreach (var featureActor in executingActors.Values)
            {
                featureActor.Tell(cancelMessage);
            }
        }

        private void HandleConfirmation(Confirmation message)
        {
            _log.Debug("Entered HandleConfirmation with Id " + Id.ToString());

            if (!TaskCanceled)
            {
                jobsTotal = featureToggleRequestsToBeConfirmed.Count;

                Start = DateTime.Now;

                // start status progress bar
                SendProgress();

                foreach (FeatureToggleRequest ftr in featureToggleRequestsToBeConfirmed)
                {

                    var locationId = ftr.Location.Id;

                    // create Location actors and trigger feature actions

                    if (!executingActors.ContainsKey(locationId))
                    {
                        IActorRef newLocationActor = Context.ActorOf(
                            LocationActor.Props(dataService),
                            locationId.ToString()
                            );

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

        private void HandleFeatureActivationCompleted([NotNull] FeatureActivationCompleted message)
        {
            _log.Debug("Entered HandleFeatureActivationCompleted with Id " + Id.ToString());

            bool success = true;
            repository.AddActivatedFeature(message.ActivatedFeature);

            jobsCompleted.Add(message.LocationReference, success);

            SendProgress();
        }

        private void HandleFeatureDeactivationCompleted([NotNull] FeatureDeactivationCompleted message)
        {
            _log.Debug("Entered HandleFeatureDeactivationCompleted with Id " + Id.ToString());

            bool success = true;
            repository.RemoveActivatedFeature(message.FeatureId, message.LocationReference);

            jobsCompleted.Add(message.LocationReference, success);

            SendProgress();
        }
        private void HandleFeatureToggleRequest([NotNull] FeatureToggleRequest message)
        {
            _log.Debug("Entered HandleFeatureToggleRequest with Id " + Id.ToString());

            if (!TaskCanceled)
            {

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
                        string.Format("Please confirm to {0} feature \n\n{1} \n\nacross location \n\n{2}\n\n\nFeatureAdmin found '{3}' location(s) where to {0}.",
                            action,
                            message.FeatureDefinition.ToString(),
                            message.Location.ToString(),
                            targetLocations.Count()),
                        Id
                        );


                eventAggregator.PublishOnUIThread(confirmRequest);
            }
        }
    }
}
