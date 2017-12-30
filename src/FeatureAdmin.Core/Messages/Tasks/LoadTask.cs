using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class LoadTask
    {
        public LoadTask(string title, Location startLocation = null)
        {
            Title = title;
            StartLocation = startLocation;
        }

        public Location StartLocation { get; }
        public string Title { get; }
    }
}
