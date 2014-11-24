using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public class Location
    {
        public Guid Id { get; set; }
        public Guid ContentDatabaseId = Guid.Empty;
        public SPFeatureScope Scope = SPFeatureScope.ScopeInvalid;
        public string Url { get; set; } // server relative for site collections
        public string Name { get; set; }

        public void Clear()
        {
            Id = Guid.Empty;
            ContentDatabaseId = Guid.Empty;
            Scope = SPFeatureScope.ScopeInvalid;
            Url = null;
            Name = null;
        }

        public static void Clear(Location location)
        {
            if (location != null)
            {
                location.Clear();
            }
        }

        public static bool IsLocationEmpty(Location location)
        {
            return (location == null || location.Id == Guid.Empty);
        }

        /// <summary>
        /// Unique key for object
        /// Suffixes content databse id when appropriate, for uniqueness
        /// </summary>
        public string Key
        {
            get
            {
                string key = this.Id.ToString();
                if (ContentDatabaseId != Guid.Empty)
                {
                    key += ContentDatabaseId.ToString();
                }
                return key;
            }
        }

        public override string ToString()
        {
            if (Scope == SPFeatureScope.Farm)
            {
                return Name;
            }
            return Name + ": " + Url;
        }
    }
}
