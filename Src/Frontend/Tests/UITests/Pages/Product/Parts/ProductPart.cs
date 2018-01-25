using OpenQA.Selenium;

namespace UITests.Pages.Product.Parts
{
    public class ProductPart
    {
        private readonly IWebElement _productElement;

        public ProductPart(IWebElement productElement)
        {
            _productElement = productElement;
        }

        public string Name => _productElement.FindElement(By.TagName("h1")).Text;

        public string Image => _productElement.FindElement(By.ClassName("product-image")).Text;

        public string ImageId => _productElement.FindElement(By.ClassName("product-image")).GetAttribute("id");

        public string AmountLeft => _productElement.FindElement(By.ClassName("product-count")).Text;
    }
}
