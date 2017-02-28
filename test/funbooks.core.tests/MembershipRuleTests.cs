using Xunit;
using Funbooks.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Funbooks.Core.Tests
{
    public class MembershipRuleTests
    {
        [Fact]
        public void RuleShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns(new List<string> {"membership request books"});
            var rule = new MembershipRule(MembershipType.Books);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }
    }
}