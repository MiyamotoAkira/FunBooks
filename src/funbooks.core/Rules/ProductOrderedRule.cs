using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core.Rules
{
    public class ProductOrderedRule : IRuleChecker
    {
        private string match;

        public ProductOrderedRule(string productType, string title)
        {
            match = $"{productType} {title}";     
        }

        public bool ShouldApply(IPOReader reader)
        {
            return reader.Request.Any( x => x.Contains(match));
        }
    }
}