using System.Net.Http;

namespace Api.IntegrationTests.Helpers
{
    public class ProductControllerHelper
    {
        public static HttpResponseMessage GetProducts()
        {
            var request = TestInitialize.Server.CreateRequest("/api/products");
            return request.GetAsync().Result;
        }

        public static HttpResponseMessage GetProducts(int pageSize, int pageIndex)
        {
            var request =
                TestInitialize.Server.CreateRequest($"/api/products?pageSize={pageSize}&pageIndex={pageIndex}");
            return request.GetAsync().Result;
        }
    }
}