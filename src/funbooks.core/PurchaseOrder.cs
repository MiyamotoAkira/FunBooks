using System.Collections.Generic;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class PurchaseOrder
    {
        List<IBusinessRule> rules = new List<IBusinessRule>();

        public PurchaseOrder()
        {

        }

        public void AddRules(List<IBusinessRule> rulesToAdd)
        {
            this.rules.AddRange(rulesToAdd);
        }

        public void Process()
        {
            foreach(var rule in rules)
            {
                if (rule.ShouldApply())
                {
                    rule.Apply();
                }
            }            
        }
    }
}
