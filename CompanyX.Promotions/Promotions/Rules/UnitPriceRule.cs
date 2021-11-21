using System.Collections.Generic;

namespace CompanyX.Promotions.Rules
{
    public class UnitPriceRule : IRule
    {
        public Sku Sku { get; }

        public UnitPriceRule(Sku sku)
        {
            Sku = sku;
        }

        public ApplyRuleResult Apply(Order remainingOrder)
        {
            var skuQuantity = remainingOrder.GetSkuQuantity(Sku.Id);
            var skuTotal = skuQuantity * Sku.UnitPrice;

            return new ApplyRuleResult
            {
                RulePrice = skuTotal,
                SkusConsumed = new List<SkuQuantity> {new SkuQuantity(Sku.Id, skuQuantity)}
            };
        }
    }
}