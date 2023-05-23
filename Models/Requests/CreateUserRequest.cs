using Newtonsoft.Json;

namespace BackendTests.Models.Requests
{
    public class CreateUserRequest
    {
        [JsonProperty("firstName")]
        public string? FirstName;

        [JsonProperty("lastName")]
        public string? LastName;
    }
}
