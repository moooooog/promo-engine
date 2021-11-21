﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyX.Promotions
{
    public class Order
    {
        private readonly Dictionary<string, int> _items;

        public Order(IEnumerable<SkuQuantity> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            _items = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var item in items)
            {
                SetSkuQuantity(item.SkuId, item.UnitCount);
            }
        }

        public Order(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _items = new Dictionary<string, int>(order._items, StringComparer.InvariantCultureIgnoreCase);
        }

        public int GetSkuQuantity(string skuId)
        {
            _items.TryGetValue(skuId, out var quantity);
            return quantity;
        }

        public void SetSkuQuantity(string skuId, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity cannot be negative");
            }

            if (quantity > 0)
            {
                _items[skuId] = quantity;
            }
            else
            {
                _items.Remove(skuId);
            }
        }

        public void Subtract(IEnumerable<SkuQuantity> itemsToSubtract)
        {
            if (itemsToSubtract != null)
            {
                var nonZeroItemsToSubtract = itemsToSubtract.Where(item => item.UnitCount > 0);
                foreach (var itemToSubtract in nonZeroItemsToSubtract)
                {
                    var originalQuantity = GetSkuQuantity(itemToSubtract.SkuId);
                    var newQuantity = originalQuantity - itemToSubtract.UnitCount;
                    SetSkuQuantity(itemToSubtract.SkuId, newQuantity);
                }
            }
        }

        public bool IsEmpty() => !_items.Any(item => item.Value > 0);
    }
}