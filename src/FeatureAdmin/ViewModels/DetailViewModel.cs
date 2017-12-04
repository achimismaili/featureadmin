using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.ViewModels
{
    public class DetailViewModel 
    {
        private IBaseItem model;

        public DetailViewModel(IBaseItem model)
        {
            this.model = model;
        }


        public string DisplayName { get { return model.DisplayName; } }

        public Guid Id { get { return model.Id; } }

        public Scope Scope { get { return model.Scope; } }

        public Dictionary<string, string> Details { get { return model.Details; } }
    }
}
