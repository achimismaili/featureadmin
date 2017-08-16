using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    public class Location : IEquatable<Location>
    {
        public Location()
        {
            this.Template = new TemplateInfo();
        }

        public Guid Id { get; set; }
        public Guid ContentDatabaseId = Guid.Empty;
        private SPFeatureScope _Scope = SPFeatureScope.ScopeInvalid;
        public SPFeatureScope Scope
        {
            get { return _Scope; }
            set
            {
                _Scope = value;
                ScopeAbbrev = ScopeAbbrevConverter.ScopeToAbbrev(value);
            }
        }
        public string ScopeAbbrev { get; private set; } // more legible scope names -- see ScopeAbbrevConverter
        public string Url { get; set; } // server relative for site collections
        public string Name { get; set; }
        public string Access { get; set; } // for ReadOnly or ReadLocked sites
        public TemplateInfo Template { get; set; }

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
        public bool Equals(Location other)
        {
            return null != other && Id == other.Id
                && ContentDatabaseId == other.ContentDatabaseId;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }
        public override int GetHashCode()
        {
            return ContentDatabaseId.GetHashCode() ^ Id.GetHashCode();
        }
    }
}
