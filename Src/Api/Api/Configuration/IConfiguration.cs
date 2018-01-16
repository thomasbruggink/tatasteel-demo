using System.Net;

namespace Api.Configuration
{
    /// <summary>
    ///     Contains configuration items
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        ///     Returns the endpoint to connect to
        /// </summary>
        /// <returns></returns>
        IPEndPoint GetAvailibilityServiceEndPoint();
    }
}