using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages.Completed
{
    public class FarmFeatureDefinitionsLoaded : BaseTaskMessage
    {


        public FarmFeatureDefinitionsLoaded(Guid taskId, [NotNull] IEnumerable<FeatureDefinition> farmFeatureDefinitions)
            : base(taskId)
        {
            FarmFeatureDefinitions = farmFeatureDefinitions;
        }
        public IEnumerable<FeatureDefinition> FarmFeatureDefinitions { get; private set; }
    }
}
