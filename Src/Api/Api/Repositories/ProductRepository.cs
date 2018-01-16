using System.Collections.Generic;
using System.Linq;
using Api.Model;

namespace Api.Repositories
{
    /// <inheritdoc />
    public class ProductRepository : IProductRepository
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IProductReader _productReader;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="productReader"></param>
        /// <param name="availabilityRepository"></param>
        public ProductRepository(IProductReader productReader, IAvailabilityRepository availabilityRepository)
        {
            _productReader = productReader;
            _availabilityRepository = availabilityRepository;
        }

        /// <inheritdoc />
        public List<Product> GetProducts(int pageSize = 10, int pageIndex = 0)
        {
            // Get the correct amount of products
            var products = _productReader.GetProducts().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            // Get the availiblity for each product
            foreach (var product in products)
                product.Availability = _availabilityRepository.GetProductCount(product.Id);

            return products;
        }
    }
}