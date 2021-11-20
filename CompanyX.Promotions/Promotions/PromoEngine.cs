using System;

namespace CompanyX.Promotions
{
    public class PromoEngine : IPromoEngine
    {
        public decimal CalculateOrderTotal(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var quantityA = order.GetItemQuantity("A");
            if (quantityA > 0)
            {
                return 50m;
            }

            return 0m;
        }
    }
}
