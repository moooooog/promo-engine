using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyX.Promotions.Rules
{
    /// <summary>
    /// Rule for applying a multibuy promotion to items in an <see cref="IOrder"/>.
    /// </summary>
    public class MultibuyPromoRule : IRule
    {
        // The items (SKU ids and the quantity) that make up a multibuy promotion.
        private readonly List<SkuQuantity> _items;

        // The combine price of the items in the promotion.
        private readonly decimal _combinedPrice;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items">The items (SKU ids and the quantity) that make up a multibuy promotion.</param>
        /// <param name="combinedPrice">The combine price of the items in the promotion.</param>
        public MultibuyPromoRule(IEnumerable<SkuQuantity> items, decimal combinedPrice)
        {
            var itemsList = items?.ToList();
            if (itemsList == null || !itemsList.Any())
            {
                throw new ArgumentException("A rule must contain at least one item", nameof(items));
            }

            if (itemsList.Any(item => item.UnitCount < 1))
            {
                throw new ArgumentOutOfRangeException(nameof(SkuQuantity.UnitCount),
                    "All UnitCounts must be 1 or more");
            }

            if (itemsList.GroupBy(item => item.SkuId).Any(group => group.Count() > 1))
            {
                throw new ArgumentException("The sku ids must be unique");
            }

            if (combinedPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(combinedPrice), combinedPrice,
                    "The CombinedPrice must not be negative");
            }

            _items = itemsList;
            _combinedPrice = combinedPrice;
        }

        /// <summary>
        /// Constructor for creating a multibuy rule that relates to a single SKU (for caller convenience).
        /// </summary>
        /// <param name="skuId">The single SKU id that applies to the promotion.</param>
        /// <param name="unitCount">The number of items making up a multibuy.</param>
        /// <param name="combinedPrice">The combined price of the items in the promotion.</param>
        public MultibuyPromoRule(string skuId, int unitCount, decimal combinedPrice)
            : this(new[] {new SkuQuantity(skuId, unitCount)}, combinedPrice)
        {
        }

        /// <summary>
        /// Constructor for creating a multibuy rule that relates to a single SKU (for caller convenience).
        /// </summary>
        /// <param name="item">The item (SKU id and the quantity) that make up a multibuy promotion.</param>
        /// <param name="combinedPrice">The combined price of the items in the promotion.</param>
        public MultibuyPromoRule(SkuQuantity item, decimal combinedPrice)
            : this(new[] {item}, combinedPrice)
        {
        }

        public ApplyRuleResult Apply(IOrder remainingOrder)
        {
            // Determine the number of times this promotion rule can be applied to the remaining order.
            // If we check for each SKU, it will be the smaller of those.
            var itemMultibuyCounts = _items
                .Select(item => remainingOrder.GetSkuQuantity(item.SkuId) / item.UnitCount);

            var overallMultibuyCount = itemMultibuyCounts.Min();

            var skuTotal = overallMultibuyCount * _combinedPrice;

            return new ApplyRuleResult
            {
                RulePrice = skuTotal,
                SkusConsumed = _items
                    .Select(item => new SkuQuantity(item.SkuId, overallMultibuyCount * item.UnitCount))
            };
        }
    }
}