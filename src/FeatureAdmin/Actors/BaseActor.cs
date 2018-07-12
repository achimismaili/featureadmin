using Akka.Actor;
using FeatureAdmin.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actors
{
    public abstract class BaseActor : ReceiveActor
    {
        /// <summary>
        /// This property is used for canceling a task
        /// </summary>
        protected string CancelationMessage { get; set; }

        protected bool TaskCanceled
        {
            get
            {
                return !string.IsNullOrEmpty(CancelationMessage);
            }
        }
        public BaseActor()
        {
            CancelationMessage = null;
            Receive<CancelMessage>(message => ReceiveCancelMessage(message));
        }

        protected abstract void ReceiveCancelMessage(CancelMessage message);
    }
}
