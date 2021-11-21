using System;
using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class Order
    {
        private readonly Dictionary<string, int> _items;
        
        public Order(IEnumerable<SkuQuantity> items)
        {
            _items = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var item in items)
            {
                SetSkuQuantity(item.SkuId, item.UnitCount);
            }
        }

        public Order(Order order)
        {
            _items = new Dictionary<string, int>(order._items, StringComparer.InvariantCultureIgnoreCase);
        }

        public int GetSkuQuantity(string skuId)
        {
            _items.TryGetValue(skuId, out var quantity);
            return quantity;
        }

        public void SetSkuQuantity(string skuId, int quantity)
        {
            if (quantity > 0)
            {
                _items[skuId] = quantity;
            }
            else
            {
                _items.Remove(skuId);
            }
        }

        public void Subtract(IEnumerable<SkuQuantity> skuQuantities)
        {
            foreach (var skuQuantity in skuQuantities)
            {
                var originalQuantity = GetSkuQuantity(skuQuantity.SkuId);
                var newQuantity = originalQuantity - skuQuantity.UnitCount;
                SetSkuQuantity(skuQuantity.SkuId, newQuantity);
            }
        }
    }
}