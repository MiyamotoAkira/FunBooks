using System.Collections.Generic;
using System.Linq;
using Xunit;
using Funbooks.Interfaces;
using Moq;

namespace Funbooks.Core.Tests
{
    public class PurchaseOrderTests
    {
        PurchaseOrder basicPO;
        Mock<ICustomerRetriever> moqCustomerRetriever;
        public PurchaseOrderTests()
        {
            moqCustomerRetriever = new Mock<ICustomerRetriever>();
            basicPO = new PurchaseOrder(string.Empty, moqCustomerRetriever.Object);    
        }
        [Theory]
        [MemberData(nameof(ListOfRules))]
        public void BusinessRulesShouldBeCheckedWhenProcessingPO(List<Mock<IBusinessRule>> rulesToApply) 
        {
            basicPO.AddRules(rulesToApply.Select(x => x.Object).ToList());
            basicPO.Process();
            rulesToApply.ForEach(x => x.Verify(y => y.ShouldApply(basicPO)));
        }

        [Fact]
        public void BusinessRulesShouldBeAppliedOnlyWhenTheCheckIsTrue() 
        {
            var counter = 0;
            var brTrue = new Mock<IBusinessRule>();
            brTrue.Setup(x => x.ShouldApply(It.IsAny<IPOReader>())).Returns(true);
            brTrue.Setup(x => x.Apply(It.IsAny<IPOModifier>())).Callback(() => counter++);
            var brFalse = new Mock<IBusinessRule>();
            brFalse.Setup(x => x.ShouldApply(It.IsAny<IPOReader>())).Returns(false);
            var rulesToApply =  new List<Mock<IBusinessRule>> {
                    brTrue,
                    brFalse,
                    brTrue
                };

            basicPO.AddRules(rulesToApply.Select(x => x.Object).ToList());
            basicPO.Process();
            Assert.Equal(2, counter);
        }

        public static IEnumerable<object []> ListOfRules ()
        {
            yield return new object[] { new List<Mock<IBusinessRule>>()};
            yield return new object[] { new List<Mock<IBusinessRule>> {new Mock<IBusinessRule>()}};
            yield return new object[] { 
                new List<Mock<IBusinessRule>> {
                    new Mock<IBusinessRule>(),
                    new Mock<IBusinessRule>(),
                    new Mock<IBusinessRule>()
                }
            };
        }

        [Fact]
        public void AddBook()
        {
            basicPO.AddBook("title");
            Assert.Equal(1,basicPO.Books.Count());
        }

        [Fact]
        public void AddVideo()
        {
            basicPO.AddVideo("title");
            Assert.Equal(1, basicPO.Video.Count());
        }

        [Fact]
        public void CreateShippingSlip()
        {
            basicPO.CreateShippingSlip();
            Assert.NotNull(basicPO.Slip);
        }

        [Fact]
        public void MembershipAdded()
        {
            var moqCustomer = new Mock<ICustomer>();
            var moqCR = new Mock<ICustomerRetriever>();
            moqCR.Setup(x => x.RetrieveCustomer(It.IsAny<int>())).Returns(moqCustomer.Object);
            var moqPO = new PurchaseOrder("Customer: 123456", moqCR.Object);    
            moqPO.AddMembership(MembershipType.Books);
            moqCustomer.Verify(x => x.AddMembership(MembershipType.Books));
        }
    }
}
