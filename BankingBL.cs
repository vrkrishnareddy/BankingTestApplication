namespace BankingGIC.BankingGIC
{
    public static class BankingBL
    {

        public static string HandleInterestRuleInput(IInterestService interestService)
        {
            Console.WriteLine("Please enter interest rules in <Date> <RuleId> <Rate in %> format:");
            if (Console.ReadLine() is { } interestInput && !string.IsNullOrWhiteSpace(interestInput) && InterestRuleDTO.Parse(interestInput) !=null)
            {
                interestService.AddInterestRule(InterestRuleDTO.Parse(interestInput));
                return "Interest rule added successfully.";
            }
            else
                return "Please enter valid format/No interest input provided.";

        }

        public  static string HandleStatementPrint(IBankingService bankingService)
        {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>:");
            if (Console.ReadLine() is { } statementInput && !string.IsNullOrWhiteSpace(statementInput))
            {
                Console.WriteLine(bankingService.PrintStatement(statementInput));
                return "Statement printed successfully.";
            }
            return "No statement input provided.";
        }

        public  static string HandleTransactionInput(IBankingService bankingService)
        {
            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format:");
            if (Console.ReadLine() is { } transactionInput && !string.IsNullOrWhiteSpace(transactionInput)
               && TransactionDTO.Parse(transactionInput) != null)
            {
                bankingService.AddTransaction(TransactionDTO.Parse(transactionInput));
                return "Transaction recorded successfully.";
            }
            else
                return "Please enter valid format/No transaction input provided.";
        

           // return "No transaction input provided.";
        }
    }
}