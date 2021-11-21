namespace CompanyX.Promotions
{
    public class Sku
    {
        public string Id { get; set; }

        public decimal UnitPrice { get; set; }

        public Sku(string id, decimal unitPrice)
        {
            Id = id;
            UnitPrice = unitPrice;
        }
    }
}