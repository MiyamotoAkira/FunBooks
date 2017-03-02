using System.Collections.Generic;
using System.Linq;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class PurchaseOrder: IPOProcessor, IPOModifier, IPOReader, IOrder, ISlipProvider
    {
        List<IBusinessRule> rules = new List<IBusinessRule>();
        List<string> books = new List<string>();
        List<string> videos = new List<string>();
        ICustomerRetriever customerRetriever;
        ICustomer customer;

        IRefererRetriever refererRetriever;
        IReferer referer;
        double total;

        public IEnumerable<string> Request {get; private set;}
        public PurchaseOrder(string request, ICustomerRetriever customerRetriever, IRefererRetriever refererRetriever)
        {
            Request = request.Split('\n');
            Books = books;
            Video = videos;
            this.customerRetriever = customerRetriever;
            this.refererRetriever = refererRetriever;
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            RetrieveTotal();
            RetrieveCustomer();
            RetrieveReferer();
        }

        private void RetrieveTotal()
        {
            var requestTotal = Request.FirstOrDefault(x => x.StartsWith("Total:"));
            if (!string.IsNullOrWhiteSpace(requestTotal))
            {
                requestTotal = requestTotal.Replace("Total:", "").Trim();
                total = int.Parse(requestTotal);
            }
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

        private void RetrieveReferer()
        {

            var refererName = Request.FirstOrDefault(x => x.StartsWith("Referer:"));
            if (!string.IsNullOrWhiteSpace(refererName))
            {
                refererName = refererName.Replace("Referer:", "").Trim();
                referer = refererRetriever.RetrieveReferer(refererName);
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
            customer.UpgradeMembership();
            return this;
        }

        public IPOModifier CreateShippingSlip()
        {
            Slip = new ShippingSlip();
            return this;
        }

        public IPOModifier GenerateCommission()
        {
            referer.GenerateCommission(total, 5);
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
