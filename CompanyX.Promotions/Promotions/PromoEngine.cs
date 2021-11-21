using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class PromoEngine : IPromoEngine
    {
        private readonly IEnumerable<IRule> _rules;

        public PromoEngine(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

        public decimal CalculateOrderTotal(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var remainingOrder = new Order(order);

            var orderTotal = 0m;

            foreach (var rule in _rules)
            {
                var ruleResult = rule.Apply(remainingOrder);
                
                orderTotal += ruleResult.RulePrice;
                remainingOrder.Subtract(ruleResult.SkusConsumed);
            }

            // TODO: Check if there is anything unexpected remaining on the order

            return orderTotal;
        }
    }
}
