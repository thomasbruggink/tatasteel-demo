using System.Collections.Generic;
using Api.Model;

namespace Api.Repositories
{
    /// <summary>
    ///     Retrieves products
    /// </summary>
    public interface IProductRepository
    {
        List<Product> GetProducts(int pageSize = 10, int pageIndex = 0);
    }
}