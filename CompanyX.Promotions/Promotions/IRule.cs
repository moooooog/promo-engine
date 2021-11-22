namespace CompanyX.Promotions
{
    public interface IRule
    {
        ApplyRuleResult Apply(IOrder remainingOrder);
    }
}