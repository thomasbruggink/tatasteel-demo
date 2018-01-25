using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Business.Models;
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
            List<Product> products;
            try
            {
                products = await _productRepository.GetProducts();
            }
            catch (Exception)
            {
                products = new List<Product>();
            }
            var productSet = new List<ProductModel>();
            foreach (var product in products)
            {
                var productModel = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Count = product.Availability,
                    ImageId = product.ImageId,
                    ImageType = product.ImageId.Split('.').Last(),
                    ImageBlob = await _imageRepository.GetImageById(product.ImageId)
                };
                productSet.Add(productModel);
            }
            return View(productSet);
        }
    }
}