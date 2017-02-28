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
            var selection = ExtractRules(lines);
            br.rules.AddRange(selection);

            selection = ExtractActions(lines);
            br.actions.AddRange(selection);
            return br;
        }

        private static IEnumerable<string> ExtractActions(IEnumerable<string> lines)
        {
            var selection = lines.SkipWhile(x => !x.Contains("actions:"));
            selection = selection.Skip(1);
            selection = selection.Select(x => x.Trim().Replace("- ", ""));
            return selection;
        }

        private static IEnumerable<string> ExtractRules(IEnumerable<string> lines)
        {
            var selection = lines.SkipWhile(x => !x.Contains("rules:"));
            selection = selection.Skip(1);
            selection = selection.TakeWhile(x => !x.Contains("actions:"));
            selection = selection.Select(x => x.Trim().Replace("- ", ""));
            return selection;
        }
        public bool ShouldApply(IPOReader purchaseOrder)
        {
            return rules.All(purchaseOrder.Request.Contains);
        }

        public void Apply(IPOModifier purchaseOrder)
        {
            actions.ForEach(x => {
                if (x.Contains("membership activate books"))
                {
                    purchaseOrder.AddMembership(MembershipType.Books);
                }
                else if (x.Contains("add video"))
                {
                    purchaseOrder.AddVideo(x.Replace("add video", "").Trim());
                }
                else if (x.Contains("membership activate upgrade"))
                {
                    purchaseOrder.UpgradeMembership();
                }
                else if (x.Contains("create shipping slip"))
                {
                    purchaseOrder.CreateShippingSlip();
                }
                else if (x.Contains("generate comission"))
                {
                    purchaseOrder.GenerateCommission();
                }
            });
        }
    }
}