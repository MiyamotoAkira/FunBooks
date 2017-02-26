using Funbooks.Interfaces;

namespace Funbooks.Core
{
    public class BusinessRule: IBusinessRule
    {
        public static BusinessRule LoadFromString(string ruleInString)
        {
            return new BusinessRule();
        }

        public bool ShouldApply()
        {
            return false;
        }

        public void Apply()
        {

        }
    }
}