using System.Net;
using TechTalk.SpecFlow;
using UITests.Services;

namespace UITests.Bindings.Given
{
    [Binding]
    public class GivenApi
    {
        [Given(@"the backend is unavailable")]
        public void GivenTheBackendIsUnavailable()
        {
            ApiServiceMock.ResponseCode = HttpStatusCode.InternalServerError;
        }
    }
}
