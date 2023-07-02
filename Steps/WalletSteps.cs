using BackendTests.Clients;
using BackendTests.Utils;
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

    }
}