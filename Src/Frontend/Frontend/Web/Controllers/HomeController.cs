using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Business.Repositories;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ImageRepository _imageRepository;

        public HomeController()
        {
            _productRepository = new ProductRepository();
            _imageRepository = new ImageRepository();
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var result = await _productRepository.GetProducts();
            var productSet = new List<ProductModel>();
            foreach (var product in result)
            {
                var productModel = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Count = product.Availability,
                    ImageBlob = await _imageRepository.GetImageById(product.ImageId)
                };
                productSet.Add(productModel);
            }
            return View(productSet);
        }
    }
}