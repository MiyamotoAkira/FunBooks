using System.Linq;
using System.Collections.Generic;
using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class BusinessRule: IBusinessRule
    {
        private List<string> rules = new List<string>();
        private List<string> actions = new List<string>();

        public static BusinessRule LoadFromString(string ruleInString)
        {
            var lines = ruleInString.Split('\n');
            var br =  new BusinessRule();
            var selection = lines.SkipWhile(x => !x.Contains("rules:"));
            selection = selection.Skip(1);
            selection = selection.TakeWhile(x => !x.Contains("actions:"));
            selection = selection.Select(x => x.Replace("    - ", ""));
            br.rules.AddRange(selection);

            var selection2 = lines.SkipWhile(x => !x.Contains("action:"));
            selection2 = selection2.Skip(1);
            selection2 = selection2.Select(x => x.Replace("    - ", ""));
            br.actions.AddRange(selection2);
            return br;
        }

        public bool ShouldApply(IPOReader purchaseOrder)
        {
            return rules.All(purchaseOrder.Request.Contains);
        }

        public void Apply(IPOModifier purchaseOrder)
        {
            actions.ForEach(x => {
                if (x.Contains("membership activate Books"))
                {
                    purchaseOrder.AddMembership(MembershipType.Books);
                }
            }
            );
        }
    }
}