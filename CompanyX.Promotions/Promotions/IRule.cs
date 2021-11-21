namespace CompanyX.Promotions
{
    public interface IRule
    {
        ApplyRuleResult Apply(Order remainingOrder);
    }
}