using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Messages.Request
{
    public class LoadTask
    {
        public LoadTask(Guid id, string title, [NotNull] Location startLocation, bool? elevatedPrivileges = null)
        {
            Id = id;
            Title = title;
            StartLocation = startLocation;
            ElevatedPrivileges = elevatedPrivileges;
        }

        public Guid Id { get; }
        public Location StartLocation { get; }
        public string Title { get; }

        // From UI, elevated privileges setting is not required, therefore set to null if not set
        public bool? ElevatedPrivileges { get; }
    }
}
