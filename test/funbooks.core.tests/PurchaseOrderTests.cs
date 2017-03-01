using System.Collections.Generic;
using System.Linq;
using Xunit;
using Funbooks.Interfaces;
using Moq;

namespace Funbooks.Core.Tests
{
    public class PurchaseOrderTests
    {
        [Theory]
        [MemberData(nameof(ListOfRules))]
        public void BusinessRulesShouldBeCheckedWhenProcessingPO(List<Mock<IBusinessRule>> rulesToApply) 
        {
            var order = new PurchaseOrder(string.Empty);
            order.AddRules(rulesToApply.Select(x => x.Object).ToList());
            order.Process();
            rulesToApply.ForEach(x => x.Verify(y => y.ShouldApply(order)));
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

            var order = new PurchaseOrder(string.Empty);
            order.AddRules(rulesToApply.Select(x => x.Object).ToList());
            order.Process();
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
            var purchaseOrder = new PurchaseOrder(string.Empty);
            purchaseOrder.AddBook("title");
            Assert.Equal(1,purchaseOrder.Books.Count());
        }

        [Fact]
        public void AddVideo()
        {
            var purchaseOrder = new PurchaseOrder(string.Empty);
            purchaseOrder.AddVideo("title");
            Assert.Equal(1,purchaseOrder.Video.Count());
        }

    }
}
