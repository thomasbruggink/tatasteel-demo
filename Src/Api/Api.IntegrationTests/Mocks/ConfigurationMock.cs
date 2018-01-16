using System.Net;
using Api.Configuration;

namespace Api.IntegrationTests.Mocks
{
    public class ConfigurationMock : IConfiguration
    {
        public IPEndPoint GetAvailibilityServiceEndPoint()
        {
            return new IPEndPoint(IPAddress.Loopback, 5001);
        }
    }
}