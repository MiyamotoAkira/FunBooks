using System.Collections.Generic;
using System.Linq;
using Xunit;
using Funbooks.Interfaces;
using Funbooks.Core;
using Moq;

namespace Funbooks.Core.Tests
{
    public class PurchaseOrderTests
    {
        [Theory]
        [MemberData(nameof(ListOfRules))]
        public void BusinessRulesShouldBeCalledWhenProcessingPO(List<Mock<IBusinessRule>> rulesToApply) 
        {
            var order = new PurchaseOrder();
            order.AddRules(rulesToApply.Select(x => x.Object).ToList());
            order.Process();
            rulesToApply.ForEach(x => x.Verify(y => y.ShouldApply()));
        }

        public static IEnumerable<object []> ListOfRules
        {
            get {
                yield return new object[] { new List<Mock<IBusinessRule>>()};
                yield return new object[] { new List<Mock<IBusinessRule>> {new Mock<IBusinessRule>()}};
                yield return new object[] { 
                    new List<Mock<IBusinessRule>> {
                        new Mock<IBusinessRule>(),
                        new Mock<IBusinessRule>(),
                        new Mock<IBusinessRule>()
                        }};
            }
        }
    }
}
