namespace Funbooks.Interfaces
{
    public interface IRefererRetriever
    {
        IReferer RetrieveReferer(string refererName);
    }
}