using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Tasks
{
    public class LoadTask
    {
        public LoadTask(Guid id, string title, Location startLocation = null)
        {
            Id = id;
            Title = title;
            StartLocation = startLocation;
        }

        public Guid Id { get; }
        public Location StartLocation { get; }
        public string Title { get; }
    }
}
