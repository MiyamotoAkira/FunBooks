using System;

namespace Funbooks.Interfaces
{
    public interface IBusinessRule
    {
        bool ShouldApply();
        void Apply();
    }
}
