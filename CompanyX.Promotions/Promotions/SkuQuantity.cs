using System;

namespace CompanyX.Promotions
{
    public class SkuQuantity
    {
        public SkuId SkuId { get; }

        public int UnitCount { get; }

        public SkuQuantity(SkuId skuId, int unitCount)
        {
            SkuId = skuId ?? throw new ArgumentNullException(nameof(skuId));

            if (unitCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitCount), unitCount, "The UnitCount cannot be negative");
            }

            UnitCount = unitCount;
        }
    }
}