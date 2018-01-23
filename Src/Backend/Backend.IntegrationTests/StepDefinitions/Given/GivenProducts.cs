using System;
using System.Collections.Generic;
using System.Text;
using Backend.IntegrationTests.Mocks;
using TechTalk.SpecFlow;

namespace Backend.IntegrationTests.StepDefinitions.Given
{
    [Binding]
    public class GivenProducts
    {
        [Given(@"the following product information is available")]
        public void GivenTheFollowingProductInformationIsAvailable(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                // | ProductId | Count |
                var productId = tableRow["ProductId"];
                var count = int.Parse(tableRow["Count"]);

                ProductFileReaderMock.ProductList.Add(productId, count);
            }
        }
    }
}
