using BankingGIC.BankingGIC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingGIC
{
    class Program
    {
        static void Main(string[] args)
        {
            IBankingService bankingService = new BankingService();
            IInterestService interestService = new InterestService();

            while (true)
            {
                Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
                Console.WriteLine("[T] Input transactions");
                Console.WriteLine("[I] Define interest rules");
                Console.WriteLine("[P] Print statement");
                Console.WriteLine("[Q] Quit");
                Console.Write("> ");

                string choice = Console.ReadLine()?.Trim().ToUpper();

                string result = choice switch
                {
                    "T" => BankingBL.HandleTransactionInput(bankingService),
                    "I" => BankingBL.HandleInterestRuleInput(interestService),
                    "P" => BankingBL.HandleStatementPrint(bankingService),
                    "Q" => "QUIT",
                    _ => "Invalid option. Please try again."
                };

                if (result == "QUIT") return;
                Console.WriteLine(result);
            }
        }
    }
}
