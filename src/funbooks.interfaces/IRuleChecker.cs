namespace Funbooks.Interfaces
{
    public interface IRuleChecker
    {
        bool ShouldApply(IPOReader purchaseOrder);
    }
}