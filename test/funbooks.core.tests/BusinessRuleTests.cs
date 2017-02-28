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
        public void BusinessRule2ExecutesUpgradeMembership()
        {
            var ipoModifier = new Mock<IPOModifier>();
            var rule = BusinessRule.LoadFromString(BusinessRule2);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.UpgradeMembership());
        }

        [Fact]
        public void BusinessRule3ExecutesCreateShippingSlip()
        {
            var ipoModifier = new Mock<IPOModifier>();
            var rule = BusinessRule.LoadFromString(BusinessRule3);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.CreateShippingSlip());
        }

        [Fact]
        public void BusinessRule4ExecutesAddVideo()
        {
            var ipoModifier = new Mock<IPOModifier>();
            var rule = BusinessRule.LoadFromString(BusinessRule4);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.AddVideo("comprehensive first aid training"));
        }

        [Fact]
        public void BusinessRule5ExecutesMembershipActivate()
        {
            var ipoModifier = new Mock<IPOModifier>();
            var rule = BusinessRule.LoadFromString(BusinessRule5);
            rule.Apply(ipoModifier.Object);
            ipoModifier.Verify(x => x.GenerateCommission());
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

        [Fact]
        public void BusinessRule2ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("membership upgrade");
            var rule = BusinessRule.LoadFromString(BusinessRule2);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

        [Fact]
        public void BusinessRule3ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("video comprehensive first aid training");
            var rule = BusinessRule.LoadFromString(BusinessRule3);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

        [Fact]
        public void BusinessRule4ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("video comprehensive first aid training");
            var rule = BusinessRule.LoadFromString(BusinessRule4);
            var actual = rule.ShouldApply(ipoReader.Object);
            Assert.True(actual);
        }

        [Fact]
        public void BusinessRule5ShouldApplyIsTrueWhenPOContainsRule()
        {
            var ipoReader = new Mock<IPOReader>();
            ipoReader.Setup(x => x.Request).Returns("referer");
            var rule = BusinessRule.LoadFromString(BusinessRule5);
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
        rules:
            - membership request books
        actions:
            - membership activate books
        ";

        public const string BusinessRule2 = @"
        name: BR2
        rules:
            - membership upgrade
        actions:
            - membership activate upgrade
        ";

        public const string BusinessRule3 = @"
        name: BR3
        rules:
            - or:
                - book
                - video
        actions:
            - create shipping slip
        ";

        public const string BusinessRule4 = @"
        name: BR4
        rules:
            - video comprehensive first aid training
        actions:
            - add video comprehensive first aid training
        ";

        public const string BusinessRule5 = @"
        name: BR5
        rules:
            - referer
        actions:
            - generate comission
        ";
    }
}
