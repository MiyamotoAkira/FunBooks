namespace Funbooks.Interfaces
{
    public interface IBusinessRule
    {
        bool ShouldApply(IPOReader purchaseOrder);
        void Apply(IPOModifier purchaseOrder);
    }
}
