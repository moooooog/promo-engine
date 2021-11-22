namespace CompanyX.Promotions
{
    /// <summary>
    /// A promotion calculation engine for determining the value of an <see cref="IOrder"/>
    /// </summary>
    public interface IPromoEngine
    {
        /// <summary>
        /// Calculates the total order value of an order.
        /// </summary>
        /// <param name="order">The order to be processed.</param>
        /// <returns>The total value of the order.</returns>
        decimal CalculateOrderTotal(IOrder order);
    }
}