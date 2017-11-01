using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.UIModels
{
    public interface IWorkSpace
    {
        string DisplayName { get; }

        void Show();
    }
}
