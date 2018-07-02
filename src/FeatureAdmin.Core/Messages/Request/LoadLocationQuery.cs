using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Core.Messages.Request
{
    public class LoadLocationQuery : BaseTaskMessage
    {
        public LoadLocationQuery(Guid taskId, [NotNull] Location location)
            : base(taskId)
        {
            Location = location;
        }
        public Location Location { get; private set; }
    }
}
