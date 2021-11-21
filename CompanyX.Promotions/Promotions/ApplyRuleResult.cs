using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class ApplyRuleResult
    {
        public decimal RulePrice { get; set; }

        public IEnumerable<SkuQuantity> SkusConsumed { get; set; }
    }
}