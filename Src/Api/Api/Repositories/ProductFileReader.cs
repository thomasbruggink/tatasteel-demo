using System.Collections.Generic;
using System.IO;
using Api.Model;
using Newtonsoft.Json;

namespace Api.Repositories
{
    /// <inheritdoc />
    public class ProductFileReader : IProductReader
    {
        /// <inheritdoc />
        public List<Product> GetProducts()
        {
            // Read the products file and return data
            var content = File.ReadAllText("./Data/products.json");
            return content == null ? null : JsonConvert.DeserializeObject<List<Product>>(content);
        }
    }
}