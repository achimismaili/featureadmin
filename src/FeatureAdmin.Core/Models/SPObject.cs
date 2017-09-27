using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Core.Models
{
    public class SPObject
    {
        private SPObject()
        {

        }

        public static SPObject GetSPObject(object obj, SPObjectType type, Scope scope)
        {
            return new SPObject()
            {
                Object = obj,
                Type = type,
                Scope = scope
            };
        }

        public object Object { get; set; }

        public SPObjectType Type { get; set; }

        public Scope Scope { get; set; }
    }
}
