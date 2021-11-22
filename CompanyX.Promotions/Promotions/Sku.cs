using System;

namespace CompanyX.Promotions
{
    public class Sku
    {
        public SkuId Id { get; }

        public decimal UnitPrice { get; }

        public Sku(SkuId id, decimal unitPrice)
        {
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