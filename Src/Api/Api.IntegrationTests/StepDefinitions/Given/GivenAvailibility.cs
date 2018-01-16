using Api.IntegrationTests.Services;
using TechTalk.SpecFlow;

namespace Api.IntegrationTests.StepDefinitions.Given
{
    [Binding]
    public class GivenAvailibility
    {
        [Given(@"the following item count is available")]
        public void GivenTheFollowingItemCountIsAvailable(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                // | ProductId | Count |
                var productId = tableRow["ProductId"];
                var count = int.Parse(tableRow["Count"]);

                AvailiblityServiceMock.ProductCount.Add(productId, count);
            }
        }

        [Given(@"the availibility system is not available")]
        public void GivenTheAvailibilitySystemIsNotAvailable()
        {
            // Dispose the service so we dont accept connections
            TestInitialize.AvailiblityServiceMock?.Dispose();
        }
    }
}