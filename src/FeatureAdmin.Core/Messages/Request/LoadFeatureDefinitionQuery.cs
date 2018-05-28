using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Messages.Request
{
    public class LoadFeatureDefinitionQuery : BaseTaskMessage
    {
        public LoadFeatureDefinitionQuery(Guid taskId) : base(taskId)
        {
        }
    }
}
