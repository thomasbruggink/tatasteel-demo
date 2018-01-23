using Backend.IntegrationTests.Helpers;
using TechTalk.SpecFlow;

namespace Backend.IntegrationTests.StepDefinitions.When
{
    [Binding]
    public class WhenAvailibility
    {
        [When(@"I request the product with id '(.*)'")]
        public void WhenIRequestTheProductWithId(string productId)
        {
            if (productId == "-")
                productId = string.Empty;
            var result = AvailibilityServerHelper.GetProductCount(productId);
            ProductResultTable.Instance.AddResult("getproduct", result);
        }
    }
}
