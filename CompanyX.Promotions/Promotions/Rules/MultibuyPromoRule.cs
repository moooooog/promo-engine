using System;
using System.Collections.Generic;

namespace CompanyX.Promotions.Rules
{
    public class MultibuyPromoRule : IRule
    {
        private readonly string _skuId;
        private readonly int _unitCount;
        private readonly decimal _combinedPrice;

        public MultibuyPromoRule(string skuId, int unitCount, decimal combinedPrice)
        {
            if (string.IsNullOrWhiteSpace(skuId))
            {
                throw new ArgumentException("The SKU id must consist of non-whitespace characters", nameof(skuId));
            }

            if (unitCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount, "The UnitCount must be 1 or more");
            }

            if (combinedPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount,
                    "The CombinedPrice must not be negative");
            }

            _skuId = skuId;
            _unitCount = unitCount;
            _combinedPrice = combinedPrice;
        }

        public ApplyRuleResult Apply(Order remainingOrder)
        {
            var skuQuantity = remainingOrder.GetSkuQuantity(_skuId);

            var multibuyCount = skuQuantity / _unitCount;

            var skuTotal = multibuyCount * _combinedPrice;

            return new ApplyRuleResult
            {
                RulePrice = skuTotal,
                SkusConsumed = new List<SkuQuantity> {new SkuQuantity(_skuId, multibuyCount * _unitCount)}
            };
        }
    }
}