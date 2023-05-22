using BackendTests.Clients;
using BackendTests.Utils;
using NUnit.Framework;
using System.Net;
using System.Xml.Linq;


namespace BackendTests
{
    public class WalletServiceTests
    {
        private readonly WalletServiceClient _walletServiceClient = new WalletServiceClient();
        private readonly ChargeGenerator _chargeGenerator = new ChargeGenerator();
        private readonly UserServiceClient _userServiceClient = new UserServiceClient();
        private readonly UserGenerator _userGenerator = new UserGenerator();
        private readonly StringGenerator _stringGenerator = new StringGenerator();

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void PostCondition()
        {
        }

        [Test]
        public async Task T01_GetBalance_CreateNewUserAndGetBalance_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);

            /* action */
            var responseGetBalance = await _walletServiceClient.GetBalance(userId);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T02_GetBalance_GetBalanceFromNotActiveUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);

            /* action */
            var responseGetBalance = await _walletServiceClient.GetBalance(userId);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T03_GetBalance_GetBalanceFromNotExsistingUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var randomUserId = int.Parse(_stringGenerator.NumericString(5));

            /* action */
            var responseGetBalance = await _walletServiceClient.GetBalance(randomUserId);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T04_GetBalance_NoTransactions_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);
            var requestStatus = await _userServiceClient.UpdateUser(userId, true);

            /* action */
            var responseGetBalance = await _walletServiceClient.GetBalance(userId);
            var responseTransactions = await _walletServiceClient.GetTransactions(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(responseGetBalance.Body, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task T05_GetBalance_GetBalanceRequestAfterRevert_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);
            var requestStatus = await _userServiceClient.UpdateUser(userId, true);
            var requestCharge = _chargeGenerator.ChargeWallet(userId, 100000);
            var responseChargedWallet = await _walletServiceClient.ChargeWallet(requestCharge);
            var transactionId = responseChargedWallet.Content.Substring(1, responseChargedWallet.Content.Length - 2);

            /* action */
            var responseRevertTransaction = await _walletServiceClient.RevertTransaction(transactionId);
            var responseGetBalance = await _walletServiceClient.GetBalance(userId);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T06_GetBalance_GetBalanceRequestAfterRevert_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);
            var requestStatus = await _userServiceClient.UpdateUser(userId, true);
            var requestCharge = _chargeGenerator.ChargeWallet(userId, 100000);
            var responseChargedWallet = await _walletServiceClient.ChargeWallet(requestCharge);
            var transactionId = responseChargedWallet.Content.Substring(1, responseChargedWallet.Content.Length - 2);

            /* action */
            var responseRevertTransaction = await _walletServiceClient.RevertTransaction(transactionId);
            var responseGetBalance = await _walletServiceClient.GetBalance(userId);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T07_ChargeWallet_ToNotActiveUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);
            var requestCharge = _chargeGenerator.ChargeWallet(userId, 100000);

            /* action */
            var responseGetBalance = await _walletServiceClient.ChargeWallet(requestCharge);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T08_ChargeWalletWithBalanceN_ChargeN_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreatedUser = await _userServiceClient.CreateUser(request);
            var userId = int.Parse(responseCreatedUser.Content);
            var requestCharge = _chargeGenerator.ChargeWallet(userId, 100000);

            /* action */
            var responseGetBalance = await _walletServiceClient.ChargeWallet(requestCharge);

            /* assert */
            Assert.That(responseGetBalance.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }


    }
}
