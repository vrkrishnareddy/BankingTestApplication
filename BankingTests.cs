using NUnit.Framework;


namespace BankingGIC
{
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
