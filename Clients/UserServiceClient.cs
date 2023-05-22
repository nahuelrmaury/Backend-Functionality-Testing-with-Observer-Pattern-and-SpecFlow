using Newtonsoft.Json;
using System.Text;
using BackendTests.Models.Responses.Base;
using BackendTests.Models.Requests;
using BackendTests.Extensions;

namespace BackendTests.Clients
{
    public class UserServiceClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://userservice-uat.azurewebsites.net";

        public async Task<CommonResponse<object>> CreateUser(CreateUserRequest request)
        {

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseUrl}/Register/RegisterNewUser"),
                Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

            return await response.ToCommonResponse<object>();
        }

        public async Task<CommonResponse<object>> ReadUser(int id)
        {
            var getUserInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}/UserManagement/GetUserStatus?userId={id}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getUserInfoRequest);

            return await response.ToCommonResponse<object>();

        }

        public async Task<CommonResponse<object>> UpdateUser(int id, bool status)
        {
            var getUserInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseUrl}/UserManagement/SetUserStatus?userId={id}&newStatus={status}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getUserInfoRequest);

            return await response.ToCommonResponse<object>();
        }

        public async Task<CommonResponse<object>> DeleteUser(int id)
        {
            var getUserInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseUrl}/Register/DeleteUser?userId={id}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getUserInfoRequest);

            return await response.ToCommonResponse<object>();
        }
    }
}