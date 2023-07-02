using Newtonsoft.Json;
using BackendTests.Models.Responses.Base;


namespace BackendTests.Extensions
{
    public static class CommonResponseExtensions
    {
        public static async Task<CommonResponse<T>> ToCommonResponse<T>(this HttpResponseMessage message)
        {
            string responseBody = await message.Content.ReadAsStringAsync();

            var commonResponse = new CommonResponse<T>
            {
                Status = message.StatusCode,
                Content = responseBody
            };

            try
            {
                commonResponse.Body = JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (JsonReaderException exception)
            {
            }

            return commonResponse;
        }
    }
}
