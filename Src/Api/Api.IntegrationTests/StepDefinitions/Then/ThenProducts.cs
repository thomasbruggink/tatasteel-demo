using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.IntegrationTests.Helpers;
using Api.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Api.IntegrationTests.StepDefinitions.Then
{
    [Binding]
    public class ThenProducts
    {
        [Then(@"the following products are returned")]
        public void ThenTheFollowingProductsAreReturned(Table table)
        {
            var result = ApiResultTable.Instance.GetResultByName("getproducts");
            // Validate response
            Assert.IsTrue(result.IsSuccessStatusCode, "Expected a success status code");

            var productResponse =
                JsonConvert.DeserializeObject<List<Product>>(
                    result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            // Validate correct item count
            Assert.AreEqual(table.RowCount, productResponse.Count,
                "Expected a different amount products to be returned");
            foreach (var tableRow in table.Rows)
            {
                //| ProductId | Name         | ImageId     | Count |
                var productId = tableRow["ProductId"];
                var name = tableRow["Name"];
                var imageId = tableRow["ImageId"];
                int? count = null;
                if (!tableRow["Count"].Equals("-"))
                    count = int.Parse(tableRow["Count"]);

                var matchingProduct = productResponse.FirstOrDefault(p => p.Id.Equals(productId));
                Assert.IsNotNull(matchingProduct, $"Could not find a matching product for id '{productId}'");
                Assert.AreEqual(name, matchingProduct.Name, "Name does not match");
                Assert.AreEqual(imageId, matchingProduct.ImageId, "ImageId does not match");
                Assert.AreEqual(count, matchingProduct.Availability, "Count does not match");
            }
        }

        [Then(@"the product api returned the '(.*)' response")]
        public void ThenTheProductApiReturnedTheResponse(string httpCode)
        {
            var code = Enum.Parse(typeof(HttpStatusCode), httpCode.Replace(" ", string.Empty));
            var result = ApiResultTable.Instance.GetResultByName("getproducts");
            Assert.AreEqual(code, result.StatusCode, "Expected a differnet result");
        }
    }
}