namespace CompanyX.Promotions
{
    public interface IPromoEngine
    {
        decimal CalculateOrderTotal(Order order);
    }
}