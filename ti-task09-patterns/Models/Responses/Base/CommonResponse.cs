using System.Net;

namespace BackendTests.Models.Responses.Base
{
    public class CommonResponse<T>
    {
        public HttpStatusCode Status;

        public string? Content;

        public T? Body;
    }
}
