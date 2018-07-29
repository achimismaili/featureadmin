using System;
using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Core.Services;
using FeatureAdmin.Messages;

namespace FeatureAdmin.Actors
{
    /// <summary>
    /// class to convert a SharePoint location and its children to SPLocation objects
    /// </summary>
    public class FeatureDefinitionActor : BaseActor
    {
        private readonly IDataService dataService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public FeatureDefinitionActor(IDataService dataService)
        {
            this.dataService = dataService;

            Receive<LoadFeatureDefinitionQuery>(message => LoadFarmFeatures(message));
            Receive<DeinstallationRequest>(message => DeinstallFeatureDefinition(message));
        }

        private void DeinstallFeatureDefinition(DeinstallationRequest message)
        {
            _log.Debug("Entered LoadFarmFeatures");

            if (!TaskCanceled)
            {
                    var errorMsg = dataService.Uninstall(message.FeatureDefinition);

                    if (string.IsNullOrEmpty(errorMsg))
                    {
                        Sender.Tell(new Core.Messages.Completed.DeinstallationCompleted(
                        message.TaskId,
                        message.FeatureDefinition.UniqueIdentifier));
                    }
                    else
                    {
                        var cancelationMsg = new CancelMessage(
                                                    message.TaskId,
                                                    errorMsg,
                                                    true
                                                    );

                        Sender.Tell(cancelationMsg);
                    }
            }
        }

        private void LoadFarmFeatures(LoadFeatureDefinitionQuery message)
        {
            _log.Debug("Entered LoadFarmFeatures");

            if (!TaskCanceled)
            {
                try
                {
                    var farmFeatureDefinitions = dataService.LoadFarmFeatureDefinitions();

                    if (farmFeatureDefinitions == null)
                    {
                        _log.Error("Farm Feature Definitions not found!");
                    }
                    Sender.Tell(new Core.Messages.Completed.FarmFeatureDefinitionsLoaded(
                        message.TaskId,
                        farmFeatureDefinitions));

                    //TODO: maybe check, if feature definitions from here have more data, than from activated features ...

                }
                catch (Exception ex)
                {
                    var cancelationMsg = new CancelMessage(
                                                message.TaskId,
                                                "Error loading feature definitions.",
                                                true,
                                                ex
                                                );

                    Sender.Tell(cancelationMsg);
                }
            }
        }

        /// <summary>
        /// Props provider
        /// </summary>
        /// <param name="dataService">SharePoint data service</param>
        /// <returns></returns>
        /// <remarks>
        /// see also https://getakka.net/articles/actors/receive-actor-api.html
        /// </remarks>
        public static Props Props(IDataService dataService)
        {
            return Akka.Actor.Props.Create(() => new FeatureDefinitionActor(
                   dataService));
        }

        protected override void ReceiveCancelMessage(CancelMessage message)
        {
            CancelationMessage = message.CancelationMessage;
        }
    }
}
