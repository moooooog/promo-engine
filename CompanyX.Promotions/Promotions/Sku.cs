using System;

namespace CompanyX.Promotions
{
    /// <summary>
    /// Represents a Stock Keeping Unit.
    /// </summary>
    public class Sku
    {
        /// <summary>
        /// Id of the SKU.
        /// </summary>
        public SkuId Id { get; }

        /// <summary>
        /// The unit price of the SKU.
        /// </summary>
        public decimal UnitPrice { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the SKU.</param>
        /// <param name="unitPrice">The unit price of the SKU.</param>
        public Sku(SkuId id, decimal unitPrice)
        {
            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), unitPrice,
                    "The SKU unit price cannot be negative");
            }

            Id = id;
            UnitPrice = unitPrice;
        }
    }
}