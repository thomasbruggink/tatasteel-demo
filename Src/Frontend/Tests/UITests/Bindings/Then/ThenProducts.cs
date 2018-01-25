using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.Product;

namespace UITests.Bindings.Then
{
    [Binding]
    public class ThenProducts
    {
        [Then(@"I see the message '(.*)'")]
        public void ThenISeeTheMessage(string message)
        {
            var page = NavigationHelper.ResolvePage<ProductPage>();
            Assert.AreEqual(message, page.BaseMessage);
        }
        
        [Then(@"I see the following products")]
        public void ThenISeeTheFollowingProducts(Table table)
        {
            var page = NavigationHelper.ResolvePage<ProductPage>();
            var products = page.GetProducts();
            Assert.AreEqual(table.RowCount, products.Count, "Expected a different amount of products");
            
            foreach (var tableRow in table.Rows)
            {
                //| ProductId | Name         | ImageId     | Count |
                //| ProductId | Name         | ImageText     | Count |
                var productId = tableRow["ProductId"];
                var name = tableRow["Name"];
                var imageId = tableRow.ContainsKey("ImageId") ? tableRow["ImageId"] : null;
                var imageText = tableRow.ContainsKey("ImageText") ? tableRow["ImageText"] : null;
                var count = tableRow["Count"];

                var product = products.FirstOrDefault(p => p.Name.Equals(name));
                Assert.IsNotNull(product, "Could not find a matching product");
                if(imageId != null)
                    Assert.AreEqual(imageId, product.ImageId, "Image not found");
                else
                    Assert.AreEqual(imageText, product.Image, "Expected an image text");
                Assert.AreEqual(count, product.AmountLeft, "Image does not match");
            }
        }
    }
}
