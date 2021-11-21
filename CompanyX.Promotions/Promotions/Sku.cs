using System;

namespace CompanyX.Promotions
{
    public class Sku
    {
        public string Id { get; }

        public decimal UnitPrice { get; }

        public Sku(string id, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("The SKU id must consist of non-whitespace characters", nameof(id));
            }

            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), unitPrice,
                    "The SKU unit price cannot be negative");
            }

            Id = id;
            UnitPrice = unitPrice;
        }
    }
}