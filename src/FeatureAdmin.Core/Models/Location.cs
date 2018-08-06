using FeatureAdmin.Core.Models.Enums;
using System;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class Location 
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

        [IgnoreDuringEquals]
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

        [IgnoreDuringEquals]
        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        [IgnoreDuringEquals]
        public string ParentId { get;  }

        public string Url { get;  }

        [IgnoreDuringEquals]
        public int ChildCount { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}: {1}, URL:'{2}'",
                this.Scope,
                this.DisplayName,
                this.Url);
        }
    }
}