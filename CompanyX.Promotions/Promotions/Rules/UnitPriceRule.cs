using System;
using System.Collections.Generic;

namespace CompanyX.Promotions.Rules
{
    /// <summary>
    /// Rule for applying the unit price of a single SKU to the items in an <see cref="IOrder"/>.
    /// </summary>
    public class UnitPriceRule : IRule
    {
        private readonly Sku _sku;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sku">The SKU containing it's unit price.</param>
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