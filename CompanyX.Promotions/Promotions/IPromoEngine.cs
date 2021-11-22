namespace CompanyX.Promotions
{
    public interface IPromoEngine
    {
        decimal CalculateOrderTotal(IOrder order);
    }
}