using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// Base class for domain objects that are distinguishable only by the state of their properties.
    /// They do not require an identity or identity tracking.
    /// The subclasses should be immutable.
    /// </summary>
    /// <typeparam name="T">
    /// Marker type. Should be used like this: <example>public class Money : ValueObject{Money}</example> .
    /// </typeparam>
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        /// <summary>
        /// Returns the properties of the value object that must be equal for 2 value objects to be equal.
        /// </summary>
        /// <example>
        /// yield return PropertyA;
        /// yield return PropertyB;
        /// </example> 
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            var valueObject = obj as ValueObject<T>;

            if (ReferenceEquals(valueObject, null))
                return false;

            if (GetType() != obj.GetType())
                return false;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}