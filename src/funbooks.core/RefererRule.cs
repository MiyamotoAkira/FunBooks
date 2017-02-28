using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class RefererRule :IRuleChecker
    {
        public bool ShouldApply(IPOReader reader)
        {
            return reader.Request.Any(x => x.StartsWith("referer"));
        }
    }
}