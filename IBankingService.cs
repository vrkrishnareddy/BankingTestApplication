namespace BankingGIC
{
    public interface IBankingService
    {
        void AddTransaction(TransactionDTO transaction);
        string PrintStatement(string input);
    }
}
