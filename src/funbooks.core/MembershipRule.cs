using Funbooks.Interfaces;
using System.Linq;

namespace Funbooks.Core
{
    public class MembershipRule: IRuleChecker
    {
        private MembershipType typeOfMembership;
        private string match;

        public MembershipRule(MembershipType typeOfMembership)
        {
            this.typeOfMembership = typeOfMembership;
            match = "membership request " + typeOfMembership.ToString().ToLower();     
        }

        public bool ShouldApply(IPOReader reader)
        {
            return reader.Request.Any( x => x.Contains(match));
        }
    }
}