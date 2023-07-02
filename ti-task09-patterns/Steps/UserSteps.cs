using BackendTests.Clients;
using BackendTests.Utils;
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;

namespace BackendTests.Steps
{
    [Binding]
    public sealed class UserSteps
    {
        private readonly UserServiceClient _userServiceClient;
        private readonly UserGenerator _userGenerator;
        private readonly StringGenerator _stringGenerator;

        private DataContext _context;
        
        public UserSteps(DataContext context,
            UserGenerator userGenerator,
            UserServiceClient userServiceClient,
            StringGenerator stringGenerator
            ) 
        {
            _context = context;
            _userGenerator = userGenerator;
            _userServiceClient = userServiceClient;
            _stringGenerator = stringGenerator;
        }

        [When(@"New user is created with empty fields")]
        public async Task GivenCreateNewUser()
        {
            var request = _userGenerator.GenerateUser("", "");
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [When(@"New user is created with null fields")]
        public async Task GivenNewUserIsCreatedWithNullFields()
        {
            var request = _userGenerator.GenerateUser(null, null);
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [When(@"New user is created with random digits")]
        public async Task GivenNewUserIsCreatedWithDigits()
        {
            var firstName = _stringGenerator.NumericString(10);
            var lastName = _stringGenerator.NumericString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [When(@"New user is created with random specials chars")]
        public async Task GivenNewUserIsCreatedWithSpecialsChars()
        {
            var firstName = _stringGenerator.SpecialCharsString(10);
            var lastName = _stringGenerator.SpecialCharsString(10);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [When(@"New user is created with one length")]
        public async Task WhenNewUserIsCreatedWithOneLenght()
        {
            var firstName = _stringGenerator.AlphabetString(1);
            var lastName = _stringGenerator.AlphabetString(1);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [When(@"New user is created with one hundred one length")]
        public async Task WhenNewUserIsCreatedWithOneHundredLenght()
        {
            var firstName = _stringGenerator.AlphabetString(101);
            var lastName = _stringGenerator.AlphabetString(101);
            var request = _userGenerator.GenerateUser(firstName, lastName);
            _context.ResponseGetUserCode = await _userServiceClient.CreateUser(request);
        }

        [Given(@"New users are created")]
        public async Task GivenNewUsersAreCreated()
        {
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var compareFirstName = _stringGenerator.AlphabetString(10);
            var compareLastName = _stringGenerator.AlphabetString(10);
            var firstRequest = _userGenerator.GenerateUser(firstName, lastName);
            var secondRequest = _userGenerator.GenerateUser(compareFirstName, compareLastName);
            var firstResponse = await _userServiceClient.CreateUser(firstRequest);
            var secondResponse = await _userServiceClient.CreateUser(secondRequest);
            _context.FirstUserId = Convert.ToInt32(firstResponse.Body);
            _context.SecondUserId = Convert.ToInt32(secondResponse.Body);
        }

        [When(@"Compare content of users")]
        public async Task WhenCompareContentOfUsers()
        {
            _context.Result = _context.SecondUserId - _context.FirstUserId;
        }

        [Given(@"Second user is deleted")]
        public async Task GivenSecondUserIsDeleted()
        {
            var firstDeleteRequest = await _userServiceClient.DeleteUser(_context.FirstUserId);
        }

        [Given(@"New user is created")]
        public async Task GivenNewUserIsCreated()
        {
            var firstName = _stringGenerator.AlphabetString(10);
            var lastName = _stringGenerator.AlphabetString(10);
            var firstRequest = _userGenerator.GenerateUser(firstName, lastName);
            var firstResponse = await _userServiceClient.CreateUser(firstRequest);
            _context.SecondUserId = Convert.ToInt32(firstResponse.Body) - 1;
            _context.UserId = Convert.ToInt32(firstResponse.Body);
        }

        [Given(@"Get random user id")]
        public void GivenGetRandomUserId()
        {
            _context.UserId = int.Parse(_stringGenerator.NumericString(7));
        }

        [When(@"Get status from user")]
        public async Task WhenGetStatusFromNotExistingUser()
        {
            _context.ResponseGetUserCode = await _userServiceClient.ReadUser(_context.UserId);
        }

        [When(@"Change status to '([^']*)'")]
        public async Task GivenChangeStatusTo(bool expected)
        {
            _context.SetUserStatusResponse = await _userServiceClient.UpdateUser(_context.UserId, expected);
        }

        [When(@"Delete user")]
        public async Task WhenDeleteUser()
        {
            _context.ResponseGetUserCode = await _userServiceClient.DeleteUser(_context.UserId);
        }



    }
}