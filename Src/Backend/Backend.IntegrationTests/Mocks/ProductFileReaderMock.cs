using System.Collections.Generic;

namespace Backend.IntegrationTests.Mocks
{
    class ProductFileReaderMock : IProductReader
    {
        public static Dictionary<string, int> ProductList = new Dictionary<string, int>();

        public int GetAvailibility(string productId)
        {
            if (!ProductList.ContainsKey(productId))
                return -1;
            return ProductList[productId];
        }
    }
}