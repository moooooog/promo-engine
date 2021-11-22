using System.Collections.Generic;

namespace CompanyX.Promotions
{
    /// <summary>
    /// Represents an order for a set of SKUs
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// Gets the quantity of items requested for the specified SKU.
        /// </summary>
        /// <param name="skuId">Id of SKU.</param>
        /// <returns>The number of items of the SKU requested.</returns>
        int GetSkuQuantity(SkuId skuId);

        /// <summary>
        /// Sets the quantity of items required for the specified SKU.
        /// </summary>
        /// <param name="skuId">Id of SKU.</param>
        /// <param name="quantity">The number of items required. Overwrites any existing record for that SKU.</param>
        void SetSkuQuantity(SkuId skuId, int quantity);

        /// <summary>
        /// Reduces the quantities of the SKU items in the order by the amounts specified.
        /// </summary>
        /// <param name="itemsToSubtract">The amount of each of the SKUs to remove from the order. Can be null.</param>
        void Subtract(IEnumerable<SkuQuantity> itemsToSubtract);

        /// <summary>
        /// true if the order is empty; false if not.
        /// </summary>
        /// <returns>true if the order is empty; false if not.</returns>
        bool IsEmpty();

        /// <summary>
        /// Creates a duplicate of this order.
        /// </summary>
        /// <returns>A copy of the order.</returns>
        IOrder Clone();
    }
}