using System.Collections.Generic;
using Xunit;
using Moq;
using Funbooks.Interfaces;

namespace Funbooks.Core.Rules.Tests
{
    public class UpgradeRuleTests
    {
        [Fact]
        public void BusinessRule2ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns(new List<string> {"membership upgrade"});
            var rule = new UpgradeRule();
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }
    }
}