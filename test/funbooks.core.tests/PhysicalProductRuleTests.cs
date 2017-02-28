using System.Collections.Generic;
using Xunit;
using Moq;
using Funbooks.Interfaces;

namespace Funbooks.Core.Tests
{
    public class PhysicalProductRuleTests
    {
        [Fact]
        public void BusinessRule3ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns(new List<string> {"video comprehensive first aid training"});
            var rule = new PhysicalProductRule();
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

    }
}