using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public interface IOrder
    {
        int GetSkuQuantity(SkuId skuId);

        void SetSkuQuantity(SkuId skuId, int quantity);
        
        void Subtract(IEnumerable<SkuQuantity> itemsToSubtract);
        
        bool IsEmpty();
        
        IOrder Clone();
    }
}