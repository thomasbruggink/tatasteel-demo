using System;

namespace Api.Repositories
{
    /// <summary>
    ///     Communication with the availibility service
    /// </summary>
    public interface IAvailabilityRepository : IDisposable
    {
        /// <summary>
        ///     Returns the amount of items available
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        int? GetProductCount(string productId);
    }
}