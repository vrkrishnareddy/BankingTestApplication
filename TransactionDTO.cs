namespace BankingGIC
{
    public class TransactionDTO
    {
        public string Date { get; set; }
        public string Account { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public decimal Balance { get; set; }

        public static TransactionDTO Parse(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length == 4 && parts[2] is "D" or "W" or "d" or "w" && decimal.TryParse(parts[3], out var amount))
            {
                string account = parts[1];
                decimal previousBalance = BankingService.GetBalance(account);

                decimal newBalance = parts[2].ToUpper() switch
                {
                    "D" => previousBalance + amount,
                    "W" when previousBalance >= amount => previousBalance - amount,
                    "W" => throw new InvalidOperationException("Insufficient balance."),
                    _ => previousBalance
                };

                return new TransactionDTO
                {
                    Date = parts[0],
                    Account = account,
                    Type = char.ToUpper(parts[2][0]),
                    Amount = amount,
                    TransactionId = GenerateTransactionId(parts[0]),
                    Balance = newBalance
                };
            }
            return null;
        }

        private static string GenerateTransactionId(string date)
        {
            return $"{date}-{new Random().Next(1, 100):D2}"; // Unique ID logic
        }
    }
}
