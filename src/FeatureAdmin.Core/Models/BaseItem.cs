using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Models
{
    public abstract class BaseItem
    {
        public Guid Id { get; protected set; }

        public string DisplayName { get; protected set; }

        public Scope Scope { get; protected set; }

    }
}
