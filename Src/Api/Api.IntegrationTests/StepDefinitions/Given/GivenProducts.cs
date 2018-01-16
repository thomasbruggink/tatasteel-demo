using Api.IntegrationTests.Mocks;
using Api.Model;
using TechTalk.SpecFlow;

namespace Api.IntegrationTests.StepDefinitions.Given
{
    [Binding]
    public class GivenProducts
    {
        [Given(@"the following product information is available")]
        public void GivenTheFollowingProductInformationIsAvailable(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                //| ProductId | Name         | ImageId     |
                var productId = tableRow["ProductId"];
                var name = tableRow["Name"];
                var imageId = tableRow["ImageId"];

                ProductFileReaderMock.Products.Add(new Product
                {
                    Id = productId,
                    Name = name,
                    ImageId = imageId
                });
            }
        }
    }
}