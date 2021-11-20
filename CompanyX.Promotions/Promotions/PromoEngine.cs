using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class PromoEngine : IPromoEngine
    {
        private readonly List<Sku> _skus = new List<Sku>
        {
            new Sku {Id = "A", UnitPrice = 50},
            new Sku {Id = "B", UnitPrice = 30},
            new Sku {Id = "C", UnitPrice = 20},
            new Sku {Id = "D", UnitPrice = 15}
        };

        public decimal CalculateOrderTotal(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var orderTotal = 0m;

            foreach (var sku in _skus)
            {
                var skuQuantity = order.GetItemQuantity(sku.Id);
                var skuTotal = skuQuantity * sku.UnitPrice;
                orderTotal += skuTotal;
            }

            return orderTotal;
        }
    }
}
