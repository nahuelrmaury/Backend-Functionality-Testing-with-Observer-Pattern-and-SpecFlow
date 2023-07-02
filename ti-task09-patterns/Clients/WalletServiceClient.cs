using Newtonsoft.Json;
using System.Text;
using BackendTests.Models.Responses.Base;
using BackendTests.Models.Requests;
using BackendTests.Extensions;

namespace BackendTests.Clients
{
    public class WalletServiceClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://walletservice-uat.azurewebsites.net";

        public async Task<CommonResponse<object>> ChargeWallet(ChargeWalletRequest request)
        {

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseUrl}/Balance/Charge"),
                Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

            return await response.ToCommonResponse<object>();
        }

        public async Task<CommonResponse<object>> GetBalance(int id)
        {
            var getWalletInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}/Balance/GetBalance?userId={id}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getWalletInfoRequest);

            return await response.ToCommonResponse<object>();

        }
        
        public async Task<CommonResponse<object>> GetTransactions(int id)
        {
            var getWalletInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}/Balance/GetTransactions?userId={id}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getWalletInfoRequest);

            return await response.ToCommonResponse<object>();

        }

        public async Task<CommonResponse<object>> RevertTransaction(string transactionId)
        {
            var getWalletInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseUrl}/Balance/RevertTransaction?userId={transactionId}")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(getWalletInfoRequest);

            return await response.ToCommonResponse<object>();
        }
    }
}