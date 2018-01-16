using System.Collections.Generic;
using System.IO;
using Api.IntegrationTests.Helpers;
using Api.IntegrationTests.Mocks;
using Api.IntegrationTests.Services;
using Api.Model;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TechTalk.SpecFlow;

namespace Api.IntegrationTests
{
    [Binding]
    public class TestInitialize
    {
        public static string DefaultTestTenant { get; set; }
        public static IContainer Container { get; set; }
        public static TestServer Server { get; private set; }
        public static AvailiblityServiceMock AvailiblityServiceMock { get; private set; }

        [BeforeScenario]
        public void ResetLists()
        {
            ApiResultTable.Reset();
            ProductFileReaderMock.Products = new List<Product>();
            AvailiblityServiceMock.ProductCount = new Dictionary<string, int>();
        }

        [BeforeScenario]
        public void SetupTestServer()
        {
            Server?.Dispose();
            Server = new TestServer(new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<TestStartup>());
            AvailiblityServiceMock?.Dispose();
            AvailiblityServiceMock = new AvailiblityServiceMock();
        }

        [AfterScenario]
        public void TearDown()
        {
            Server?.Dispose();
            Server = null;
            AvailiblityServiceMock?.Dispose();
            AvailiblityServiceMock = null;
        }
    }
}