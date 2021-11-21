namespace CompanyX.Promotions
{
    public class SkuQuantity
    {
        public string SkuId { get; set; }

        public int UnitCount{ get; set; }

        public SkuQuantity(string skuId, int unitCount)
        {
            SkuId = skuId;
            UnitCount = unitCount;
        }
    }
}