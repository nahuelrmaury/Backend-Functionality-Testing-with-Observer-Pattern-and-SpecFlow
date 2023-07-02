using BackendTests.Clients;
using BackendTests.Utils;
using System.Transactions;
using TechTalk.SpecFlow;

namespace BackendTests.Steps
{
    [Binding]
    public sealed class WalletSteps
    {
        private readonly WalletServiceClient _walletServiceClient;
        private readonly ChargeGenerator _chargeGenerator;
        private readonly UserServiceClient _userServiceClient;
        private readonly UserGenerator _userGenerator;
        private readonly StringGenerator _stringGenerator;

        private DataContext _context;

        public WalletSteps(DataContext context,
            WalletServiceClient walletServiceClient,
            ChargeGenerator chargeGenerator,
            UserServiceClient userServiceClient,
            UserGenerator userGenerator,
            StringGenerator stringGenerator
            )
        {
            _context = context;
            _userGenerator = userGenerator;
            _userServiceClient = userServiceClient;
            _stringGenerator = stringGenerator;
            _chargeGenerator = chargeGenerator;
            _walletServiceClient = walletServiceClient;
        }

        [Given(@"Create new user")]
        public async Task GivenNewUserIsCreated()
        {
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var firstRequest = _userGenerator.GenerateUser(firstName, lastName);
            var firstResponse = await _userServiceClient.CreateUser(firstRequest);
            _context.UserId = Convert.ToInt32(firstResponse.Content);
        }

        [When(@"Get balance")]
        public async Task WhenGetBalance()
        {
            _context.ChargeResponse = await _walletServiceClient.GetBalance(_context.UserId);
        }

        [Given(@"Get id from not existing user")]
        public void GivenGetRandomUserId()
        {
            _context.UserId = int.Parse(_stringGenerator.NumericString(7));
        }

        [When(@"Get transactions")]
        public async Task WhenGetTransactions()
        {
            _context.ChargeResponse = await _walletServiceClient.GetTransactions(_context.UserId);
        }

        [Given(@"Make charge to wallet")]
        public async Task GivenMakeChargeToWallet()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, 100000);
            var responseChargedWallet = await _walletServiceClient.ChargeWallet(requestCharge);
            _context.TransactionId = responseChargedWallet.Content.Substring(1, responseChargedWallet.Content.Length - 2);
        }

        [When(@"Revert transaction")]
        public async Task WhenRevertTransaction()
        {
            var responseRevertTransaction = await _walletServiceClient.RevertTransaction(_context.TransactionId);
        }

        [Given(@"Make user active")]
        public async Task GivenMakeUserActive()
        {
            var requestStatus = await _userServiceClient.UpdateUser(_context.UserId, true);
        }

        [When(@"Make charge to wallet")]
        public void WhenMakeChargeToWallet()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, 100000);
        }

        [When(@"Charge wallet with more than it already have")]
        public async Task WhenChargeWalletWithMoreThanItAlreadyHave()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, -100000 - 0.01);
            _context.ChargeResponse = await _walletServiceClient.ChargeWallet(requestCharge);
        }

        [When(@"Charge wallet with negative balance amount")]
        public async Task WhenChargeWalletWithNegativeBalanceAmount()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, -100000);
            _context.ChargeResponse = await _walletServiceClient.ChargeWallet(requestCharge);
        }

        [When(@"Make negative charge")]
        public async Task WhenMakeNegativeCharge()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, -30);
            _context.ChargeResponse = await _walletServiceClient.ChargeWallet(requestCharge);
        }

        [Given(@"Make charge to wallet plus ten")]
        public async Task GivenMakeChargeToWalletPlusTen()
        {
            var requestCharge = _chargeGenerator.ChargeWallet(_context.UserId, 10);
            _context.ChargeResponse = await _walletServiceClient.ChargeWallet(requestCharge);
        }

    }
}