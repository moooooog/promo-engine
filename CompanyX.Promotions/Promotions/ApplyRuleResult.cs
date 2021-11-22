using System.Collections.Generic;

namespace CompanyX.Promotions
{
    /// <summary>
    /// Contains information pertaining the the result of applying a rule in an <see cref="IPromoEngine"/>.
    /// </summary>
    public class ApplyRuleResult
    {
        /// <summary>
        /// The price of the items that make up the rule.
        /// If the rule can be applied multiple times then this will be the total price of applying the rule multiple times.
        /// </summary>
        public decimal RulePrice { get; set; }

        /// <summary>
        /// The SKUs consumed when applying the rule as many times as possible within the remaining order.
        /// </summary>
        public IEnumerable<SkuQuantity> SkusConsumed { get; set; }
    }
}