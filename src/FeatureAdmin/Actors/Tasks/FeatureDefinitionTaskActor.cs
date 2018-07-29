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
    public class FeatureDefinitionTaskActor : BaseTaskActor
    {

        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IDataService dataService;

        private bool force;
        private bool elevatedPrivileges;

        // actor to deactivate left over active features
        private readonly IActorRef subTaskActor;

        // actor to uninstall definition
        private readonly IActorRef featureDefinitionActor;
        private readonly IFeatureRepository repository;
        private IEnumerable<ActivatedFeatureSpecial> FeatureDeactivations;
        private FeatureDefinition FeatureDefinitionToUninstall;
        public FeatureDefinitionTaskActor(
            IEventAggregator eventAggregator,
            IFeatureRepository repository,
            IDataService dataService,
            Guid taskId)
            : base(eventAggregator, taskId)
        {
            this.eventAggregator.Subscribe(this);

            this.repository = repository;
            this.dataService = dataService;

            subTaskActor = Context.ActorOf(
               FeatureTaskActor.Props(
                   eventAggregator,
                   repository,
                   dataService,
                   Id,
                   true),
               "FeatureTaskActor-" + Id);

            featureDefinitionActor = Context.ActorOf(
                FeatureDefinitionActor.Props(dataService), "FeatureDefinitionActor-" + Id);

            FeatureDeactivations = new List<ActivatedFeatureSpecial>();

            Receive<Confirmation>(message => HandleConfirmation(message));
            Receive<DeinstallationRequest>(message => HandleDeinstallationRequest(message));
            Receive<DeinstallationCompleted>(message => HandleDeinstallationCompleted(message));
            Receive<ProgressMessage>(message => HandleActivatedFeatureDeactivationCompleted(message));
        }

        private void HandleActivatedFeatureDeactivationCompleted(ProgressMessage message)
        {
            finishedSteps++;
            SendProgress();
            UninstallDefinition();
        }

        private void HandleDeinstallationRequest(DeinstallationRequest message)
        {
            _log.Debug("Entered HandleDeinstallationRequest with Id " + Id.ToString());

            if (!TaskCanceled)
            {
                FeatureDefinitionToUninstall = message.FeatureDefinition;

                Title = message.Title;

                string mentionForce = string.Empty;

                // these settings are only required for feature deactivation, not needed for uninstall
                // in case definition has scope invalid, feature deactivation is only possible with force
                if (FeatureDefinitionToUninstall.Scope == Enums.Scope.ScopeInvalid)
                {
                    force = true;
                    mentionForce = "\n\nfyi - As this definition has an invalid scope, feature deactivation will be done with 'force' flag enabled, no mater what is set in feature admin UI.";
                }
                else
                {
                    force = message.Force.Value;
                }
                elevatedPrivileges = message.ElevatedPrivileges.Value;

                // retrieve activated features with this feature definition in this farm 
                var featuresToDeactivate = repository.GetActivatedFeatures(message.FeatureDefinition);

                if (featuresToDeactivate.Any())
                {
                    FeatureDeactivations = repository.GetAsActivatedFeatureSpecial(featuresToDeactivate);

                    var confirmRequest = new DecisionRequest(
                           Title,
                           string.Format(
                               "You requested to uninstall the feature definition\n\n{0}\n\nThere are still features activated in this farm. Count: {1} \n\n" +
                               "It is recommended to deactivate these features before uninstalling the definition. Should activated features first be deactivated?\n" +
                               "(If you click 'No', the deinstallation of feature definition will start without activating any active features based on this definition before --> not recommended.{2}",
                               message.FeatureDefinition.ToString(),
                               featuresToDeactivate.Count(),
                               mentionForce
                               ),
                           Id
                           );

                    eventAggregator.PublishOnUIThread(confirmRequest);
                }
                else
                {
                    var confirmRequest = new ConfirmationRequest(
                           Title,
                           string.Format(
                               "Uninstall of feature definition\n\n{0}\n\nThere were no activated features found in this farm.\n\n",
                               message.FeatureDefinition.ToString()
                               ),
                           Id
                           );

                    eventAggregator.PublishOnUIThread(confirmRequest);
                }
            }
        }

        private int totalSteps = 1;
        private int finishedSteps = 0;
        public override double PercentCompleted
        {
            get
            {
                return finishedSteps / totalSteps;
            }
        }

        public override string StatusReport
        {
            get
            {
                return string.Format(
                    "'{0}' (TaskID: '{1}') - deinstallation completed successfully\nelapsed time: {2}",
                    Title,
                    Id,
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
            new FeatureDefinitionTaskActor(
                eventAggregator,
                repository,
                dataService,
                taskId));
        }

        protected override void HandleCancelation(CancelMessage cancelMessage)
        {
            _log.Debug("Entered HandleCancelation with Id " + Id.ToString());


            featureDefinitionActor.Tell(cancelMessage);

            subTaskActor.Tell(cancelMessage);

        }

        private void HandleConfirmation(Confirmation message)
        {
            _log.Debug("Entered HandleConfirmation with Id " + Id.ToString());

            // start status progress bar
            SendProgress();

            if (!TaskCanceled)
            {
                Start = DateTime.Now;

                if (message.Decision)
                {
                    totalSteps++;
                    DeactivateFeatures();
                }
                else
                {
                    UninstallDefinition();
                }

            }
        }

        private void UninstallDefinition()
        {
            if (!TaskCanceled)
            {
                // force and elevatedprivileges are irrelevant for feature definition uninstall
                // they are only available for qualifying feature deactivation of active features before the uninstallation
                var fdQuery = new DeinstallationRequest(FeatureDefinitionToUninstall);

                featureDefinitionActor.Tell(fdQuery);
            }
        }

        private void DeactivateFeatures()
        {
            var featureDeactivationRequest = new DeactivateFeaturesRequest(
                FeatureDeactivations,
                force,
                elevatedPrivileges
                );

            subTaskActor.Tell(featureDeactivationRequest);
        }

        private void HandleDeinstallationCompleted([NotNull] DeinstallationCompleted message)
        {
            _log.Debug("Entered HandleDeinstallationCompleted with Id " + Id.ToString());

            repository.RemoveFeatureDefinition(message.DefinitionUniqueIdentifier);

            finishedSteps++;

            SendProgress();
        }
    }
}
