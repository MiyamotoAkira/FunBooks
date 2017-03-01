using System;
using System.Linq;
using System.Collections.Generic;
using Funbooks.Interfaces;
using Funbooks.Core.Rules;

namespace Funbooks.Core
{
    public class BusinessRule: IBusinessRule
    {
        private List<IRuleChecker> rules = new List<IRuleChecker>();
        private List<string> actions = new List<string>();

        public static BusinessRule LoadFromString(string ruleInString)
        {
            var lines = ruleInString.Split('\n');
            var br =  new BusinessRule();
            var ruleSelection = ExtractRules(lines);
            br.rules.AddRange(ruleSelection);

            var selection = ExtractActions(lines);
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

        private static IEnumerable<IRuleChecker> ExtractRules(IEnumerable<string> lines)
        {
            var extracted = ExtractLines(lines);

            var selection = extracted.Select<string, IRuleChecker>(x => 
            {
                if (x.StartsWith("membership upgrade"))
                {
                    return new UpgradeRule();
                }
                else if (x.StartsWith("membership request"))
                {
                    var extractedType = x.Replace("membership request", "").Trim();
                    extractedType = extractedType.Substring(0,1).ToUpper() + extractedType.Substring(1, extractedType.Length - 1);
                    var membershipType = (MembershipType)Enum.Parse(typeof(MembershipType), extractedType);
                    return new MembershipRule(membershipType);
                }
                else if (x.StartsWith("referer"))
                {
                    return new RefererRule();
                }
                else if (x.StartsWith("video"))
                {
                    var title = x.Replace("video", "").Trim();
                    return new ProductOrderedRule("video", title);
                }
                else if (x.StartsWith("physical product"))
                {
                    return new PhysicalProductRule();
                }
                else
                {
                    throw new Exception();
                }
            });

            return selection;
        }

        private static IEnumerable<string> ExtractLines(IEnumerable<string> lines)
        {
            var selection = lines.SkipWhile(x => !x.Contains("rules:"));
            selection = selection.Skip(1);
            selection = selection.TakeWhile(x => !x.Contains("actions:"));
            selection = selection.Select(x => x.Trim().Replace("- ", ""));
            return selection;
        }

        public bool ShouldApply(IPOReader purchaseOrder)
        {
            return rules.All(x => x.ShouldApply(purchaseOrder));
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