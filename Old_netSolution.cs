using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTestApplication
{

        class Program1
        {
            static void Main1(string[] args)
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
                    if (choice == "Q") break;

                    switch (choice)
                    {
                        case "T":
                            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format:");
                            string transactionInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(transactionInput))
                            {
                                bankingService.AddTransaction(TransactionDTO.Parse(transactionInput));
                            }
                            break;

                        case "I":
                            Console.WriteLine("Please enter interest rules in <Date> <RuleId> <Rate in %> format:");
                            string interestInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(interestInput))
                            {
                                interestService.AddInterestRule(InterestRuleDTO.Parse(interestInput));
                            }
                            break;

                        case "P":
                            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>:");
                            string statementInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(statementInput))
                            {
                                bankingService.PrintStatement(statementInput);
                            }
                            break;
                    }
                }
            }
        }

        public interface IBankingService
        {
            void AddTransaction(TransactionDTO transaction);
            void PrintStatement(string input);
        }

        public class BankingService : IBankingService
        {
            private readonly List<TransactionDTO> _transactions = new List<TransactionDTO>();

            public void AddTransaction(TransactionDTO transaction)
            {
                _transactions.Add(transaction);
                Console.WriteLine("Transaction recorded successfully.");
            }

            public void PrintStatement(string input)
            {
                Console.WriteLine("Printing statement for: " + input);
            }
        }

        public class TransactionDTO
        {
            public string Date { get; set; }
            public string Account { get; set; }
            public char Type { get; set; }
            public decimal Amount { get; set; }

            public static TransactionDTO Parse(string input)
            {
                var parts = input.Split(' ');
                return new TransactionDTO
                {
                    Date = parts[0],
                    Account = parts[1],
                    Type = char.ToUpper(parts[2][0]),
                    Amount = decimal.Parse(parts[3])
                };
            }
        }

        public interface IInterestService
        {
            void AddInterestRule(InterestRuleDTO rule);
        }

        public class InterestService : IInterestService
        {
            private readonly List<InterestRuleDTO> _interestRules = new List<InterestRuleDTO>();

            public void AddInterestRule(InterestRuleDTO rule)
            {
                _interestRules.RemoveAll(r => r.Date == rule.Date);
                _interestRules.Add(rule);
                Console.WriteLine("Interest rule added successfully.");
            }
        }

        public class InterestRuleDTO
        {
            public string Date { get; set; }
            public string RuleId { get; set; }
            public decimal Rate { get; set; }

            public static InterestRuleDTO Parse(string input)
            {
                var parts = input.Split(' ');
                return new InterestRuleDTO
                {
                    Date = parts[0],
                    RuleId = parts[1],
                    Rate = decimal.Parse(parts[2])
                };
            }
        }

        [TestFixture]
        public class BankingTests
        {
            private IBankingService _bankingService;
            private IInterestService _interestService;

            [SetUp]
            public void Setup()
            {
                _bankingService = new BankingService();
                _interestService = new InterestService();
            }

            [Test]
            public void Test_AddTransaction()
            {
                var transaction = new TransactionDTO { Date = "20230626", Account = "AC001", Type = 'D', Amount = 100.00m };
                Assert.DoesNotThrow(() => _bankingService.AddTransaction(transaction));
            }

            [Test]
            public void Test_AddInterestRule()
            {
                var rule = new InterestRuleDTO { Date = "20230615", RuleId = "RULE03", Rate = 2.20m };
                Assert.DoesNotThrow(() => _interestService.AddInterestRule(rule));
            }
        }
    }
