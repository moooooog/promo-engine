using System;

namespace CompanyX.Promotions
{
    public class SkuQuantity
    {
        public string SkuId { get; }

        public int UnitCount { get; }

        public SkuQuantity(string skuId, int unitCount)
        {
            if (string.IsNullOrWhiteSpace(skuId))
            {
                throw new ArgumentException("The SKU id must consist of non-whitespace characters", nameof(skuId));
            }

            if (unitCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount, "The UnitCount cannot be negative");
            }

            SkuId = skuId;
            UnitCount = unitCount;
        }
    }
}