using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Business.Models;
using Newtonsoft.Json;

namespace Business.Repositories
{
    public class ProductRepository
    {
        private readonly HttpClient _httpClient;

        public ProductRepository()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Configuration.Instance.ApiEndpoint)
            };
        }

        public async Task<List<Product>> GetProducts()
        {
            var result = await _httpClient.GetAsync("/api/products");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(content);
        }
    }
}
