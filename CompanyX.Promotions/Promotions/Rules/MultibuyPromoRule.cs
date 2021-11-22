using System;
using System.Collections.Generic;

namespace CompanyX.Promotions.Rules
{
    public class MultibuyPromoRule : IRule
    {
        private readonly SkuQuantity _item;
        private readonly decimal _combinedPrice;

        public MultibuyPromoRule(string skuId, int unitCount, decimal combinedPrice)
            : this(new SkuQuantity(skuId, unitCount), combinedPrice)
        {
        }

        public MultibuyPromoRule(SkuQuantity item, decimal combinedPrice)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.UnitCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(SkuQuantity.UnitCount), item.UnitCount,
                    "The UnitCount must be 1 or more");
            }

            if (combinedPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(combinedPrice), combinedPrice,
                    "The CombinedPrice must not be negative");
            }

            _item = item;
            _combinedPrice = combinedPrice;
        }

        public ApplyRuleResult Apply(Order remainingOrder)
        {
            var skuQuantity = remainingOrder.GetSkuQuantity(_item.SkuId);

            var multibuyCount = skuQuantity / _item.UnitCount;

            var skuTotal = multibuyCount * _combinedPrice;

            return new ApplyRuleResult
            {
                RulePrice = skuTotal,
                SkusConsumed = new List<SkuQuantity> {new SkuQuantity(_item.SkuId, multibuyCount * _item.UnitCount)}
            };
        }
    }
}