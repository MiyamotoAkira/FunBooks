using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class PhysicalProductRule : IRuleChecker
    {
        public bool ShouldApply(IPOReader reader)
        {
            return reader.Request.Any(x => x.StartsWith("video") || x.StartsWith("book"));
        }
    }
}