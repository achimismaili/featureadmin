using Caliburn.Metro.Core;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using System.ComponentModel.Composition;
using MahApps.Metro;

namespace FeatureAdmin
{
    public class AppWindowManager : MetroWindowManager
    {
        public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
        {
            if (windowIsView)
            {
                return view as Views.MainWindowContainer;
            }

            return new Views.MainWindowContainer
            {
                Content = view
            };
        }
    }
}
