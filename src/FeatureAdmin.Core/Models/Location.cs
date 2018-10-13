using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Serializable]
    public class Location : IEquatable<Location>
    {
        public Location(Guid id,
            string displayName,
            string parentId,
            Scope scope,
            [NotNull] string url,
            int childCount = 0,
            Guid? databaseId = null,
            LockState lockState = LockState.NotLocked)
        {
            Id = id;
            DisplayName = displayName == null ? string.Empty : displayName;
            ParentId = parentId;
            Scope = scope;
            Url = url == null ? string.Empty : url;
            ChildCount = childCount;
            DataBaseId = databaseId;
            LockState = lockState;

            UniqueId = id.ToString();

            // if there is a database id, add it with a space to the UniqueId
            if (databaseId.HasValue) // && (scope == Scope.Web || scope == Scope.Site ))
            {
                UniqueId += Common.Constants.MagicStrings.GuidSeparator.ToString() + databaseId.Value;
            }

        }

        public string DisplayName { get; protected set; }

        /// <summary>
        /// SharePoint ID is a guid for all locations like farm, web app, site collection and web
        /// </summary>
        /// <remarks>
        /// unfortunately, it is not unique in a farm, as e.g. restored site collections contain webs with same guid
        /// </remarks>
        public Guid Id { get; protected set; }

        /// <summary>
        /// A really unique ID for all locations based on the SpId and for site collections and web also on web app and database id
        /// </summary>
        public string UniqueId { get; protected set; }

        public Guid? DataBaseId { get; protected set; }

        public LockState LockState { get; protected set; }

        public Scope Scope { get; protected set; }

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        public string ParentId { get; }

        public string Url { get; }

        public int ChildCount { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location other)
        {
            return other != null &&
                   DisplayName == other.DisplayName &&
                   UniqueId == other.UniqueId &&
                   LockState == other.LockState &&
                   Scope == other.Scope &&
                   ParentId == other.ParentId &&
                   Url == other.Url &&
                   ChildCount == other.ChildCount;
        }

        public override int GetHashCode()
        {
            var hashCode = 1789934933;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UniqueId);
            hashCode = hashCode * -1521134295 + LockState.GetHashCode();
            hashCode = hashCode * -1521134295 + Scope.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParentId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + ChildCount.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}: {1}, URL:'{2}'",
                this.Scope,
                this.DisplayName,
                this.Url);
        }

        public static bool operator ==(Location location1, Location location2)
        {
            return EqualityComparer<Location>.Default.Equals(location1, location2);
        }

        public static bool operator !=(Location location1, Location location2)
        {
            return !(location1 == location2);
        }
    }
}