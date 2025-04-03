namespace BankingGIC
{
    public class InterestService : IInterestService
    {
        private readonly List<InterestRuleDTO> _interestRules = new();

        public void AddInterestRule(InterestRuleDTO rule)
        {
            _interestRules.RemoveAll(r => r.Date == rule.Date);
            _interestRules.Add(rule);
        }
    }
}
