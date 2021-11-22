using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class PromoEngine : IPromoEngine
    {
        private readonly IEnumerable<IRule> _rules;

        public PromoEngine(IEnumerable<IRule> rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public decimal CalculateOrderTotal(IOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var remainingOrder = order.Clone();

            var orderTotal = 0m;

            foreach (var rule in _rules)
            {
                var ruleResult = rule.Apply(remainingOrder);
                
                orderTotal += ruleResult.RulePrice;
                remainingOrder.Subtract(ruleResult.SkusConsumed);
            }

            if (!remainingOrder.IsEmpty())
            {
                throw new PromoEngineException("The order contains items that have not been processed");
            }

            return orderTotal;
        }
    }
}
