using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.IntegrationTests.Helpers;
using Backend.IntegrationTests.Mocks;
using TechTalk.SpecFlow;

namespace Backend.IntegrationTests
{
    [Binding]
    public class TestInitialize
    {
        private AvailabilityServer _availabilityServer;
        private Task _hostingTask;

        [BeforeScenario]
        public void InitServer()
        {
            _availabilityServer = new AvailabilityServer(new ProductFileReaderMock());
            _hostingTask?.Dispose();
            _hostingTask = new Task(() =>
            {
                _availabilityServer.Start();
            });
            _hostingTask.Start();
        }

        [BeforeScenario]
        public void ResetData()
        {
            ProductFileReaderMock.ProductList = new Dictionary<string, int>();
            ProductResultTable.Reset();
        }

        [AfterScenario]
        public void Cleanup()
        {
            _availabilityServer.Stop();
            _hostingTask?.Wait();
            _hostingTask?.Dispose();
            _hostingTask = null;
        }
    }
}
