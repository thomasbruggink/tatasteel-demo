using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Owin.Hosting;
using TechTalk.SpecFlow;
using UITests.Services;
using UITests.TestSupport.Models;

namespace UITests
{
    [Binding]
    public class TestInitialize
    {
        public static IDisposable ApiServiceTestServer;

        [BeforeScenario]
        public void Reset()
        {
            ApiServiceMock.ResponseCode = HttpStatusCode.OK;
            ApiServiceMock.ResponseMessages = new List<Product>();
        }

        [BeforeScenario]
        public void SetupServiceMocks()
        {
            ApiServiceTestServer = WebApp.Start<ApiServiceMock>("http://localhost:64339");
        }

        [AfterScenario]
        public void Cleanup()
        {
            ApiServiceTestServer?.Dispose();
            ApiServiceTestServer = null;
        }
    }
}
