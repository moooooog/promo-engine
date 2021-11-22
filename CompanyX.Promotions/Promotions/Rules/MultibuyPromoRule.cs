using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyX.Promotions.Rules
{
    public class MultibuyPromoRule : IRule
    {
        private readonly List<SkuQuantity> _items;
        private readonly decimal _combinedPrice;

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

            if (itemsList.GroupBy(item => item.SkuId.ToLowerInvariant()).Any(group => group.Count() > 1))
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

        public MultibuyPromoRule(string skuId, int unitCount, decimal combinedPrice)
            : this(new[] {new SkuQuantity(skuId, unitCount)}, combinedPrice)
        {
        }

        public MultibuyPromoRule(SkuQuantity item, decimal combinedPrice)
            : this(new[] {item}, combinedPrice)
        {
        }

        public ApplyRuleResult Apply(IOrder remainingOrder)
        {
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