namespace Funbooks.Interfaces
{
    public interface ICustomerRetriever
    {
        ICustomer RetrieveCustomer(int customerId);
    }
}