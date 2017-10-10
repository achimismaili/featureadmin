using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Backends.Messages;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Actors
{
    public class LocationActor: ReceiveActor
        {
            private readonly IDataService dataService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public LocationActor(IDataService dataService)
            {
                this.dataService = dataService;

                Receive<LookUpLocationMessage>(message => LookupLocation(message));
            }

            private void LookupLocation(LookUpLocationMessage message)
            {
            _log.Debug("Entered LocationActor-LookupLocation");
            var location = dataService.ReLoadLocation(message.Location);

                Sender.Tell(new LookedUpLocationMessage(location));
            }
        }
    }
