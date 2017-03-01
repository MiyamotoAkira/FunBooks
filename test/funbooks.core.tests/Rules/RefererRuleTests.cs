using System.Collections.Generic;
using Xunit;
using Moq;
using Funbooks.Interfaces;

namespace Funbooks.Core.Rules.Tests
{
    public class RefererRuleTests
    {
        [Fact]
        public void BusinessRule4ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns(new List<string> {"referer"});
            var rule = new RefererRule();
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }
    }
}