using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class Confirmation 
    {
        /// <summary>
        /// How did the confirmation end?
        /// </summary>
        /// <param name="taskId">task id, the confirmation was requested for</param>
        /// <param name="decision">if it was a decision, true = yes, false = no</param>
        public Confirmation(Guid taskId, bool decision = false)
        {
            TaskId = taskId;
            Decision = decision;
        }

        public Guid TaskId { get; }

        // true = yes, false = no
        public bool Decision { get; }

    }
}
