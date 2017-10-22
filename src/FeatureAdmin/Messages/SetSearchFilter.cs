using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Messages
{
    public class SetSearchFilter<T> where T : class
    {
        public SetSearchFilter(string searchQuery, Scope? searchScope)
           : this(searchQuery, searchScope, true, true)
        { }

        public SetSearchFilter(string searchQuery)
           : this(searchQuery, null, true, false)
        { }

        public SetSearchFilter(Scope? searchScope)
           : this(null, searchScope, false, true)
        { }

        private SetSearchFilter(string searchQuery, Scope? searchScope, bool setQuery, bool setScope)
        {
            SearchQuery = searchQuery;
            SearchScope = searchScope;
            SetQuery = SetQuery;
            SetScope = setScope;
        }

        public bool SetQuery { get; private set; }

        public bool SetScope { get; private set; }

        public string SearchQuery { get; private set; }

        public Scope? SearchScope { get; private set; }
    }
}
