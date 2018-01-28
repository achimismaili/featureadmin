using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class ActionOptionsUpdate
    {
        public ActionOptionsUpdate(bool canActivate, bool canDeactivate, bool canUpgrade)
        {
            CanActivate = canActivate;
            CanDeactivate = canDeactivate;
            CanUpgrade = canUpgrade;
        }

        public bool CanActivate { get; private set; }
        public bool CanDeactivate { get; private set; }
        public bool CanUpgrade { get; private set; }
    }
}
