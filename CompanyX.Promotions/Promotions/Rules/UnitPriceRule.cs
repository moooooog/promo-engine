using System;
using System.Collections.Generic;

namespace CompanyX.Promotions.Rules
{
    public class UnitPriceRule : IRule
    {
        private readonly Sku _sku;

        public UnitPriceRule(Sku sku)
        {
            _sku = sku ?? throw new ArgumentNullException(nameof(sku));
        }

        public ApplyRuleResult Apply(IOrder remainingOrder)
        {
            var skuQuantity = remainingOrder.GetSkuQuantity(_sku.Id);
            var skuTotal = skuQuantity * _sku.UnitPrice;

            return new ApplyRuleResult
            {
                RulePrice = skuTotal,
                SkusConsumed = new List<SkuQuantity> {new SkuQuantity(_sku.Id, skuQuantity)}
            };
        }
    }
}