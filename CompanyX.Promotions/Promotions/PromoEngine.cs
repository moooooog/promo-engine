using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    /// <summary>
    /// Implementation of a promotion calculation engine.
    /// Rules will be applied in the order that they are supplied.
    /// Each rule will be applied as many times as possible with the remaining items in the order.
    /// </summary>
    public class PromoEngine : IPromoEngine
    {
        // The rules to be applied (in order of application)
        private readonly IEnumerable<IRule> _rules;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rules">The rules to be applied to each order (in order of application).</param>
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

            // Take a copy of the order - we will remove items as each rule is applied
            var remainingOrder = order.Clone();

            var orderTotal = 0m;

            foreach (var rule in _rules)
            {
                var ruleResult = rule.Apply(remainingOrder);

                orderTotal += ruleResult.RulePrice;
                remainingOrder.Subtract(ruleResult.SkusConsumed);
            }

            // Hopefully there is nothing left unaccounted for in the order after all the rules have been applied.
            if (!remainingOrder.IsEmpty())
            {
                throw new PromoEngineException("The order contains items that have not been processed");
            }

            return orderTotal;
        }
    }
}