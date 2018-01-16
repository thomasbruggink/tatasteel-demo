using Api.IntegrationTests.Helpers;
using TechTalk.SpecFlow;

namespace Api.IntegrationTests.StepDefinitions.When
{
    [Binding]
    public class WhenProducts
    {
        [When(@"I request products")]
        public void WhenIRequestProducts()
        {
            var response = ProductControllerHelper.GetProducts();
            ApiResultTable.Instance.AddResult("getproducts", response);
        }

        [When(@"I request '(.*)' products")]
        public void WhenIRequestProducts(int pageSize)
        {
            var response = ProductControllerHelper.GetProducts(pageSize, 1);
            ApiResultTable.Instance.AddResult("getproducts", response);
        }

        [When(@"I request '(.*)' products from page '(.*)'")]
        public void WhenIRequestProductsFromPage(int pageSize, int pageIndex)
        {
            var response = ProductControllerHelper.GetProducts(pageSize, pageIndex);
            ApiResultTable.Instance.AddResult("getproducts", response);
        }
    }
}