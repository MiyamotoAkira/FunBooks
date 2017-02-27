using System.Collections.Generic;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class PurchaseOrder: IPOModifier, IPOReader
    {
        List<IBusinessRule> rules = new List<IBusinessRule>();

        public string Request {get; private set;}
        public PurchaseOrder(string request)
        {
            Request = request;
        }

        public void AddRules(List<IBusinessRule> rulesToAdd)
        {
            this.rules.AddRange(rulesToAdd);
        }

        public void Process()
        {
            foreach(var rule in rules)
            {
                if (rule.ShouldApply(this))
                {
                    rule.Apply(this);
                }
            }            
        }

        public IPOModifier AddMembership(MembershipType membershipType)
        {
            return this;
        }

        public IPOModifier UpgradeMembership()
        {
            return this;
        }

        public IPOModifier CreateShippingSlip()
        {
            return this;
        }

        public IPOModifier GenerateCommission()
        {
            return this;
        }

        public IPOModifier AddBook()
        {
            return this;
        }

        public IPOModifier AddVideo()
        {
            return this;
        }
    }
}
