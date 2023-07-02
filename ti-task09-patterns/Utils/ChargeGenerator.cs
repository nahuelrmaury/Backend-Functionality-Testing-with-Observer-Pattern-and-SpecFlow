using BackendTests.Models.Requests; 

namespace BackendTests.Utils
{
    public class ChargeGenerator
    {
        public ChargeWalletRequest ChargeWallet(int userId, double amount)
        {
            return new ChargeWalletRequest()
            {
                UserId = userId,
                Amount = amount,
            };
        }
    }
}