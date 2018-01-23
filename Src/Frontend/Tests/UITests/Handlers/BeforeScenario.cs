using TechTalk.SpecFlow;
using UITests.TestSupport.Api;

namespace UITests.Handlers
{
    [Binding]
    public class BeforeScenario
    {
        [BeforeScenario]
        public void ResetDatabase()
        {
            var databaseApiClient = new DatabaseApiClient();
            databaseApiClient.ResetDatabase();
        }
    }
}