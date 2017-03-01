using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core.Rules
{
    public class UpgradeRule : IRuleChecker
    {
        public bool ShouldApply(IPOReader reader)
        {
            return reader.Request.Any(x => x.StartsWith("membership upgrade"));
        }
    }
}