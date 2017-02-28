namespace Funbooks.Interfaces
{
    public interface IBusinessRule : IRuleChecker
    {
        void Apply(IPOModifier purchaseOrder);
    }
}
