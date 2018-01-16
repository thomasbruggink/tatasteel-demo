using System.Collections.Generic;
using Api.Model;

namespace Api.Repositories
{
    /// <summary>
    ///     A repository to access products
    /// </summary>
    public interface IProductReader
    {
        /// <summary>
        ///     Returns all available products
        /// </summary>
        /// <returns></returns>
        List<Product> GetProducts();
    }
}