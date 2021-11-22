using System.Collections.Generic;

namespace CompanyX.Promotions
{
    public interface IOrder
    {
        int GetSkuQuantity(string skuId);

        void SetSkuQuantity(string skuId, int quantity);
        
        void Subtract(IEnumerable<SkuQuantity> itemsToSubtract);
        
        bool IsEmpty();
        
        IOrder Clone();
    }
}