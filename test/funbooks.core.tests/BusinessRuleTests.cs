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
            var rule = BusinessRule.LoadFromString(BusinessRule1);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.AddMembership(MembershipType.Books));
        }

        [Fact]
        public void BusinessRule1ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("membership request books");
            var rule = BusinessRule.LoadFromString(BusinessRule1);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

        public static IEnumerable<object []> Rules
        {
            get 
            {
                yield return new object [] {BusinessRule1};
            }
        }

        public const string BusinessRule1 = @"
        name: BR1
        rule:
            - membership request books
        action:
            - membership activate books
        ";

        public const string BusinessRule2 = @"
        name: BR2
        rule:
            - membership upgrade
        action:
            - membership activate upgrade
        ";

        public const string BusinessRule3 = @"
        name: BR3
        rule:
            - or:
                - book
                - video
        action:
            - create shipping slip
        ";

        public const string BusinessRule4 = @"
        name: BR4
        rule:
            - video comprehensive first aid training
        action:
            - add video comprehensive first aid training
        ";

        public const string BusinessRule5 = @"
        name: BR5
        rule:
            - referer
        action:
            - generate comission
        ";
    }
}
