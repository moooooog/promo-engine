using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public class Order
    {
        private readonly Dictionary<char, int> _items;

        public Order(Dictionary<char, int> items)
        {
            _items = items;
        }
    }
}