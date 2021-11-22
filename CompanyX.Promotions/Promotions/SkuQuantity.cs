using System;

namespace CompanyX.Promotions
{
    /// <summary>
    /// Basic entity for storing a quantity of a specific SKU.
    /// </summary>
    public class SkuQuantity
    {
        /// <summary>
        /// The id of the SKU.
        /// </summary>
        public SkuId SkuId { get; }

        /// <summary>
        /// The quantity of units of the SKU.
        /// </summary>
        public int UnitCount { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="skuId">The id of the SKU.</param>
        /// <param name="unitCount">The quantity of units of the SKU.</param>
        public SkuQuantity(SkuId skuId, int unitCount)
        {
            SkuId = skuId ?? throw new ArgumentNullException(nameof(skuId));

            if (unitCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount, "The UnitCount cannot be negative");
            }

            UnitCount = unitCount;
        }
    }
}