namespace CompanyX.Promotions
{
    /// <summary>
    /// A rule to be used in calculating the value of an <see cref="IOrder"/>.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Applies the rule to the remaining items in the order that have not yet been accounted for.
        /// </summary>
        /// <param name="remainingOrder">The order containing items that have not yet been processed by the <see cref="IPromoEngine"/></param>
        /// <returns>Information regarding the result of applying the rule.</returns>
        ApplyRuleResult Apply(IOrder remainingOrder);
    }
}