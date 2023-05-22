using NUnit.Framework;
using System.Net;
using BackendTests.Utils;
using BackendTests.Clients;


namespace BackendTests
{
    public class UserServiceTests
    {
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
        public async Task T01_CreatUser_CreateWithEmptyFields_StatusCodeIsOk()
        {
            /* action */
            var request = _userGenerator.GenerateUser("", "");
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T02_CreatUser_CreateWithNullFields_StatusCodeIsInternalServerError()
        {
            /* action */
            var request = _userGenerator.GenerateUser(null, null);
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T03_CreatUser_CreateWithDigitsInFields_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.NumericString(10);
            var lastName = _stringGenerator.NumericString(10);

            /* action */
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T04_CreatUser_CreateWithSpecialCharsInFields_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.SpecialCharsString(10);
            var lastName = _stringGenerator.SpecialCharsString(10);

            /* action */
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(1)]
        [TestCase(101)]
        public async Task T05_CreatUser_CreateWithDiferentSymbolLengthInFields_StatusCodeIsOk(int length)
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(length);
            var lastName = _stringGenerator.AlphabetString(length);

            /* action */
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T06_CreatUser_CreateWithUpperCaseInFields_StatusCodeIsOk()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10).ToUpper();
            var lastName = _stringGenerator.AlphabetString(10).ToUpper();

            /* action */
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var response = await _userServiceClient.CreateUser(request);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task T07_CreateTwoUsers_CompareContentOfNewUsers_ReturningValueIsAutoIncremented()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var compareFirstName = _stringGenerator.AlphabetString(10);
            var compareLastName = _stringGenerator.AlphabetString(10);
            var firstRequest = _userGenerator.GenerateUser(firstName, lastName);
            var secondRequest = _userGenerator.GenerateUser(compareFirstName, compareLastName);

            /* action */
            var firstResponse = await _userServiceClient.CreateUser(firstRequest);
            var secondResponse = await _userServiceClient.CreateUser(secondRequest);
            int firstUserId = int.Parse(firstResponse.Content);
            int secondUserId = int.Parse(secondResponse.Content);

            /* assert */
            Assert.That(secondUserId - firstUserId, Is.EqualTo(1));
        }

        public async Task T08_CreateTwoUsers_DeleteLastUserCreatedAndCreateNewUser_ReturningValueIsAutoIncremented()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var firstRequest = _userGenerator.GenerateUser(firstName, lastName);
            var firstResponse = await _userServiceClient.CreateUser(firstRequest);
            int firstUserId = int.Parse(firstResponse.Content);

            /* action */
            var firstDeleteRequest = await _userServiceClient.DeleteUser(firstUserId);
            var compareFirstName = _stringGenerator.AlphabetString(10);
            var compareLastName = _stringGenerator.AlphabetString(10);
            var secondRequest = _userGenerator.GenerateUser(compareFirstName, compareLastName);
            var secondResponse = await _userServiceClient.CreateUser(secondRequest);
            int secondUserId = int.Parse(secondResponse.Content);

            /* assert */
            Assert.That(secondUserId - firstUserId, Is.EqualTo(1));
        }

        [Test]
        public async Task T09_GetUserStatus_NotExistingUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var randomUserId = int.Parse(_stringGenerator.NumericString(5));

            /* action */
            var response = await _userServiceClient.ReadUser(randomUserId);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T10_GetUserStatus_ValidUser_DefaultStatusIsFalse()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);

            /* action */
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.That(responseRead.Content, Is.EqualTo("false"));
        }

        [Test]
        public async Task T11_GetUserStatus_ChangeUserStatus_DefaultStatusIsChangedAndFalse()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseUpdate = await _userServiceClient.UpdateUser(userId, true);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, false);
            var responseUpdatedStatus = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseUpdatedStatus.Content, Is.Not.EqualTo(responseRead.Content));
                Assert.That(responseRead.Content, Is.EqualTo("true"));
                Assert.That(responseUpdatedStatus.Content, Is.EqualTo("false"));
            });
        }

        [Test]
        public async Task T12_GetUserStatus_ChangeUserStatus_DefaultFalseStatusIsChangedToTrue()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseUpdatedStatus = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseUpdatedStatus.Content, Is.Not.EqualTo(responseRead.Content));
                Assert.That(responseRead.Content, Is.EqualTo("false"));
                Assert.That(responseUpdatedStatus.Content, Is.EqualTo("true"));
            });
        }

        [Test]
        public async Task T13_SetUserStatus_NotExistingUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var randomUserId = int.Parse(_stringGenerator.NumericString(5));

            /* action */
            var response = await _userServiceClient.UpdateUser(randomUserId, true);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task T14_SetUserStatus_ChangeUserStatusOneTime_StatusChangeFromFalseToTrue()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseUpdatedStatus = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseRead.Content, Is.EqualTo("false"));
                Assert.That(responseUpdatedStatus.Content, Is.EqualTo("true"));
            });
        }

        [Test]
        public async Task T15_SetUserStatus_ChangeUserStatusTwoTimes_StatusChangeFromFalseToTrueAndFalseAgain()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseReadStatus = await _userServiceClient.ReadUser(userId);
            var responseUpdateStatusAgain = await _userServiceClient.UpdateUser(userId, false);
            var responseReadStatusAgain = await _userServiceClient.ReadUser(userId);


            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseRead.Content, Is.EqualTo("false"));
                Assert.That(responseReadStatus.Content, Is.EqualTo("true"));
                Assert.That(responseReadStatusAgain.Content, Is.EqualTo("false"));
            });
        }

        [Test]
        public async Task T16_SetUserStatus_ChangeUserStatusTwoTimes_StatusChangeFromFalseToTrueToFalseAndToTrueAgain()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseReadStatus = await _userServiceClient.ReadUser(userId);
            var responseUpdateStatusAgain = await _userServiceClient.UpdateUser(userId, false);
            var responseReadStatusAgain = await _userServiceClient.ReadUser(userId);
            var responseUpdateLastStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseReadLastStatus = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseRead.Content, Is.EqualTo("false"));
                Assert.That(responseReadStatus.Content, Is.EqualTo("true"));
                Assert.That(responseReadStatusAgain.Content, Is.EqualTo("false"));
                Assert.That(responseReadLastStatus.Content, Is.EqualTo("true"));
            });
        }

        [Test]
        public async Task T17_SetUserStatus_ChangeUserStatusOneTime_StatusChangeFromFalseToFalseAgain()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, false);
            var responseUpdatedStatus = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseRead.Content, Is.EqualTo("false"));
                Assert.That(responseUpdatedStatus.Content, Is.EqualTo("false"));
            });
        }

        [Test]
        public async Task T18_SetUserStatus_ChangeUserStatusTwoTimes_StatusChangeFromFalseToTrueToFalseAndToTrueAgain()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseUpdateStatus = await _userServiceClient.UpdateUser(userId, true);
            var responseReadStatus = await _userServiceClient.ReadUser(userId);
            var responseUpdateStatusAgain = await _userServiceClient.UpdateUser(userId, true);
            var responseReadStatusAgain = await _userServiceClient.ReadUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.That(responseReadStatus.Content, Is.EqualTo("true"));
                Assert.That(responseReadStatusAgain.Content, Is.EqualTo("true"));
            });
        }

        [Test]
        public async Task T19_DeleteUser_NotActiveUser_StatusCodeOk()
        {
            /* precondition */
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            var responseCreate = await _userServiceClient.CreateUser(request);
            int userId = int.Parse(responseCreate.Content);
            var responseRead = await _userServiceClient.ReadUser(userId);

            /* action */
            var responseDelete = await _userServiceClient.DeleteUser(userId);

            /* assert */
            Assert.Multiple(() =>
            {
                Assert.AreEqual("false", responseRead.Content);
                Assert.AreEqual(HttpStatusCode.OK, responseDelete.Status);
            });
        }

        [Test]
        public async Task T20_DeleteUser_NotExistingUser_StatusCodeIsInternalServerError()
        {
            /* precondition */
            var randomUserId = int.Parse(_stringGenerator.NumericString(5));

            /* action */
            var response = await _userServiceClient.DeleteUser(randomUserId);

            /* assert */
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.InternalServerError));
        }
    }
}