using System.Collections.Generic;
using Xunit;

namespace Funbooks.Core.Tests
{
    public class BusinessRuleTests
    {
        [Theory]
        [MemberData(nameof(Rule))]
        public void LoadBusinessRule(string businessRule) 
        {
            var rule = BusinessRule.LoadFromString(businessRule);
            Assert.NotNull(rule);
        }

        public static IEnumerable<object []> Rule
        {
            get 
            {
                yield return new object [] {BusinessRule1};
            }
        }

        public const string BusinessRule1 = @"
        name: BR1
        rule:
            - membership request X
        action:
            - membership activate X
        ";

    }
}
