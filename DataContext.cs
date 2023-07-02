using BackendTests.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests
{
    public class DataContext
    {
        public CommonResponse<object> ResponseGetUserCode;

        public int UserId;

        public CommonResponse<bool> GetUserStatusResponse;

        public CommonResponse<object> SetUserStatusResponse;

        public CommonResponse<object> DeleteUserResponse;

        public CommonResponse<object> ChargeResponse;

        public int FirstUserId;

        public int SecondUserId;

        public int ThirdUserId;

        public int Result;

        //public CommonResponse<List<GetTransactionInfoResponse>> GetAllUserTransactions;
    }
}
