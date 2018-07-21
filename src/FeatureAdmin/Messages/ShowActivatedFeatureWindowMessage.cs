using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class ShowActivatedFeatureWindowMessage
    {
        public bool ShowWindow { get; set; }

        public ShowActivatedFeatureWindowMessage(bool showWindow = true)
        {
            ShowWindow = showWindow;
        }
    }
}
