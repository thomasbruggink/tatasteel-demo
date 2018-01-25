using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Model;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controllers
{
    /// <summary>
    ///     Allows access to the product API
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Returns a list of products
        /// </summary>
        /// <param name="pageSize">The amount of items per page (max 1000)</param>
        /// <param name="pageIndex">The page to fetch</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/products")]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "A list of products", Type = typeof(List<Product>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Description = "The request was invalid", Type = typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Description = "An internal server error occured")]
        public IActionResult Get(int pageSize = 10, int pageIndex = 1)
        {
            if (pageSize < 0 || pageIndex <= 0)
                return BadRequest("Pagesize and pageindex must be positive numbers");
            if (pageSize > 1000)
                pageSize = 1000;

            // Check if all values have a result otherwise return partial
            var result = _productRepository.GetProducts(pageSize, pageIndex - 1);
            return result.Any(p => p.Availability == null) ? StatusCode(206, result) : Ok(result);
        }
    }
}