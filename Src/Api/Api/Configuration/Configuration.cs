using System.Net;

namespace Api.Configuration
{
    /// <inheritdoc />
    public class Configuration : IConfiguration
    {
        /// <inheritdoc />
        public IPEndPoint GetAvailibilityServiceEndPoint()
        {
            // Hard coded value. This can be read from anywhere
            return new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
        }
    }
}