using System;

namespace FeatureAdmin.Core.Models
{
    public abstract class BaseEquatable<T> : IEquatable<T> where T : class
    {

        public bool Equals(T other)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // Return true if the comparison of implementationj matches
            return EqualsInternal(other);
        }

        protected abstract bool EqualsInternal( T right);

        public override bool Equals(object right)
        {
            return !object.ReferenceEquals((object)null, right) && (object.ReferenceEquals((object)this, right) || this.GetType() == right.GetType() && EqualsInternal((T)right));
        }

        public abstract override int GetHashCode();
    }
}
