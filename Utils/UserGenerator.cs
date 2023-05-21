using BackendTests.Models.Requests;

namespace BackendTests.Utils
{
    public class UserGenerator
    {
        public CreateUserRequest GenerateUser(string firstName, string lastName)
        {
            return new CreateUserRequest()
            {
                FirstName = firstName,
                LastName = lastName,
            };
        }
    }
}