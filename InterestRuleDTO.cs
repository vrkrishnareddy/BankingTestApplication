namespace BankingGIC
{
    public class InterestRuleDTO
    {
        public string Date { get; set; }
        public string RuleId { get; set; }
        public decimal Rate { get; set; }

        public static InterestRuleDTO? Parse(string input)
        {
            var parts = input.Split(' ');
            return parts.Length == 3 && decimal.TryParse(parts[2], out var rate) && rate is > 0 and < 100
                ? new InterestRuleDTO { Date = parts[0], RuleId = parts[1], Rate = rate }
                : null;
        }
    }
}
