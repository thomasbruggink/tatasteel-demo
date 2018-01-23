using Backend.IntegrationTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Backend.IntegrationTests.StepDefinitions.Then
{
    [Binding]
    public class ThenProducts
    {
        [Then(@"the received product count is '(.*)'")]
        public void ThenTheReceivedProductCountIs(int result)
        {
            var receivedData = ProductResultTable.Instance.GetResultByName("getproduct");
            Assert.AreEqual(result, receivedData, "Expected a different product count");
        }
    }
}
