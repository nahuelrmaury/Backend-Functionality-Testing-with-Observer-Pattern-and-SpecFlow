using Newtonsoft.Json;

namespace BackendTests.Models.Requests
{
    public class ChargeWalletRequest
    {
        [JsonProperty("userId")]
        public int UserId;

        [JsonProperty("amount")]
        public double Amount;
    }
}
