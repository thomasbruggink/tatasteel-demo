using System.Net;

namespace UITests.TestSupport.Api
{
    public class ApiResponse
    {
        public HttpStatusCode Status { get; set; }

        public dynamic Content { get; set; }
    }
}
