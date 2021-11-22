using System;

namespace CompanyX.Promotions
{
    /// <summary>
    /// A Stock Keeping Unit (SKU) id.
    /// Encapsulates any validation and comparison rules.
    /// e.g. A SKU is not case-sensitive.
    /// </summary>
    public class SkuId : IEquatable<SkuId>
    {
        private readonly string _id;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">String representation of the id.</param>
        public SkuId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("The id must consist of non-whitespace characters", nameof(id));
            }

            _id = id;
        }

        /// <summary>
        /// Allow implicit conversion of a string to a SKU id (for caller convenience).
        /// </summary>
        /// <param name="id">String representation of the id.</param>
        public static implicit operator SkuId(string id) => new SkuId(id);

        public override string ToString() => _id;

        public bool Equals(SkuId other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(_id, other._id, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }
            
            return Equals((SkuId) obj);
        }

        public override int GetHashCode()
        {
            return _id != null ? _id.ToUpperInvariant().GetHashCode() : 0;
        }
    }
}