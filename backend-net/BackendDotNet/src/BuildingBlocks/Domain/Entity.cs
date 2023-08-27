using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// (Marker) interface all domain entities must implement.
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// Base class for domain entities.
    /// It provides an implementation for equality.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Returns the properties of the entity that, when combined, identify the entity uniquely.
        /// </summary>
        /// <example>
        /// yield return PropertyA;
        /// yield return PropertyB;
        /// </example> 
        protected abstract IEnumerable<object> GetIdComponents();

        public override bool Equals(object obj)
        {
            Entity? other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return GetIdComponents().SequenceEqual(other.GetIdComponents());
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetIdComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }
    }
}