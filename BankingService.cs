using System.Transactions;

namespace BankingGIC
{
    public class BankingService : IBankingService
    {
        private static readonly Dictionary<string, List<TransactionDTO>> AccountTransactions = new();
        private static readonly Dictionary<string, decimal> AccountBalances = new();

        public void AddTransaction(TransactionDTO transaction)
        {
            // Initialize the account balance if it's the first transaction
            if (!AccountTransactions.ContainsKey(transaction.Account))
            {
                AccountTransactions[transaction.Account] = new List<TransactionDTO>();
                AccountBalances[transaction.Account] = 0; // Initial balance is 0
            }

            AccountTransactions[transaction.Account].Add(transaction);
            AccountBalances[transaction.Account] = transaction.Balance; // Update balance with new transaction balance
        }

        public string PrintStatement(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length != 2) return "Invalid input format. Please enter in <Account> <YearMonth> format.";

            string account = parts[0];
            string yearMonth = parts[1];

            if (!AccountTransactions.ContainsKey(account)) return "Account not found.";

            var transactions = AccountTransactions[account]
                .Where(t => t.Date.StartsWith(yearMonth)) // Filter transactions by month
                .OrderBy(t => t.Date)
                .ToList();

            decimal runningBalance = 0;
            string statement = $"Account: {account}\n";
            statement += "| Date     | Txn Id      | Type | Amount | Balance |\n";

            foreach (var txn in transactions)
            {
                runningBalance = txn.Balance; // Update running balance
                statement += $"| {txn.Date} | {txn.TransactionId} | {txn.Type}    | {txn.Amount:F2} | {runningBalance:F2} |\n";
            }

            // Apply interest (for simplicity, we assume interest is applied at the end of the month)
            decimal interest = CalculateInterest(account, yearMonth);
            runningBalance += interest;
            statement += $"| {yearMonth}30 |             | I    | {interest:F2} | {runningBalance:F2} |\n";

            return statement;
        }

        private decimal CalculateInterest(string account, string yearMonth)
        {
            // Example interest calculation logic
            var transactions = AccountTransactions[account]
                .Where(t => t.Date.StartsWith(yearMonth)) // Filter by month
                .OrderBy(t => t.Date)
                .ToList();

            decimal totalInterest = 0;
            DateTime? lastDate = null;
            decimal lastBalance = 0;

            foreach (var txn in transactions)
            {
                if (lastDate != null)
                {
                    int daysBetween = (DateTime.ParseExact(txn.Date, "yyyyMMdd", null) - lastDate.Value).Days;
                    decimal interestRate = 2.0m; // Default interest rate (this could be dynamic)
                    totalInterest += (lastBalance * interestRate / 100) * daysBetween / 365;
                }

                lastDate = DateTime.ParseExact(txn.Date, "yyyyMMdd", null);
                lastBalance = txn.Balance;
            }

            return totalInterest;
        }

        public static decimal GetBalance(string account)
        {
            return AccountBalances.ContainsKey(account) ? AccountBalances[account] : 0;
        }
    }

}
