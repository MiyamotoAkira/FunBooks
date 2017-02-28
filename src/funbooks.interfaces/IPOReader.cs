using System.Collections.Generic;

namespace Funbooks.Interfaces
{
    public interface IPOReader
    {
        IEnumerable<string> Request {get;}   
    }
}  