using System.Collections.Generic;

namespace Funbooks.Interfaces
{
    public interface IOrder
    {
        IEnumerable<string> Books {get;}

        IEnumerable<string> Video {get;}
    }
}