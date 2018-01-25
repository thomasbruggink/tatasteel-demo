using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UITests.Pages.Product.Parts;

namespace UITests.Pages.Product
{
    public class ProductPage : PageBase<ProductPage>, IPage
    {
        public List<ProductPart> GetProducts()
        {
            var elements = AssemblyConfiguration.Driver.FindElements(By.ClassName("product"));

            return elements.Select(e => new ProductPart(e)).ToList();
        }

        public string BaseMessage => AssemblyConfiguration.Driver.FindElement(By.ClassName("product-base-message")).Text;

        public string GetUrl()
        {
            return "/";
        }
    }
}
