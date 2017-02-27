using System.Collections.Generic;
using Xunit;
using Funbooks.Interfaces;
using Moq;

namespace Funbooks.Core.Tests
{
    public class BusinessRuleTests
    {
        [Theory]
        [MemberData(nameof(Rules))]
        public void LoadBusinessRule(string businessRule) 
        {
            var rule = BusinessRule.LoadFromString(businessRule);
            Assert.NotNull(rule);
        }

        [Fact]
        public void BusinessRule1ExecutesMembershipActivate()
        {
            var ipoModifier = new Mock<IPOModifier>();
            var rule = BusinessRule.LoadFromString(BusinessRule1_Book);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.AddMembership(MembershipType.Books));
        }

        [Fact]
        public void BusinessRule1ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("membership request Books");
            var rule = BusinessRule.LoadFromString(BusinessRule1_Book);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

        public static IEnumerable<object []> Rules
        {
            get 
            {
                yield return new object [] {BusinessRule1_Book};
            }
        }

        public const string BusinessRule1_Book = @"
        name: BR1
        rule:
            - membership request Books
        action:
            - membership activate Books
        ";
    }
}
