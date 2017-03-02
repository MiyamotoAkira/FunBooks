using System.Collections.Generic;
using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class PurchaseOrder: IPOModifier, IPOReader, IOrder, ISlipProvider
    {
        List<IBusinessRule> rules = new List<IBusinessRule>();
        List<string> books = new List<string>();
        List<string> videos = new List<string>();
        ICustomerRetriever customerRetriever;
        ICustomer customer;

        public IEnumerable<string> Request {get; private set;}
        public PurchaseOrder(string request, ICustomerRetriever customerRetriever)
        {
            Request = request.Split('\n');
            Books = books;
            Video = videos;
            this.customerRetriever = customerRetriever;
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            RetrieveCustomer();
        }

        private void RetrieveCustomer()
        {

            var requestCustomerId = Request.FirstOrDefault(x => x.StartsWith("Customer:"));
            if (!string.IsNullOrWhiteSpace(requestCustomerId))
            {
                requestCustomerId = requestCustomerId.Replace("Customer:", "").Trim();
                var customerId = int.Parse(requestCustomerId);
                customer = customerRetriever.RetrieveCustomer(customerId);
            }
        }

        public IEnumerable<string> Books {get; private set;}

        public IEnumerable<string> Video {get; private set;}

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
            customer.AddMembership(membershipType);
            return this;
        }

        public IPOModifier UpgradeMembership()
        {
            return this;
        }

        public IPOModifier CreateShippingSlip()
        {
            Slip = new ShippingSlip();
            return this;
        }

        public IPOModifier GenerateCommission()
        {
            return this;
        }

        public IPOModifier AddBook(string title)
        {
            books.Add(title);
            return this;
        }

        public IPOModifier AddVideo(string title)
        {
            videos.Add(title);
            return this;
        }

        public ShippingSlip Slip {get; private set;}
    }
}
