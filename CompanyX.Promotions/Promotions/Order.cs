using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class Order
    {
        private readonly Dictionary<string, int> _items;

        public Order(IDictionary<string, int> items)
        {
            _items = new Dictionary<string, int>(items, StringComparer.InvariantCultureIgnoreCase);
        }

        public int GetSkuQuantity(string skuId)
        {
            _items.TryGetValue(skuId, out var quantity);
            return quantity;
        }
    }
}