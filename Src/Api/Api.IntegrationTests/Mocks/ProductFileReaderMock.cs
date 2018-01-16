using System.Collections.Generic;
using Api.Model;
using Api.Repositories;

namespace Api.IntegrationTests.Mocks
{
    public class ProductFileReaderMock : IProductReader
    {
        public static List<Product> Products = new List<Product>();

        public List<Product> GetProducts()
        {
            return Products;
        }
    }
}