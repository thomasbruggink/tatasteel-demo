using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.Product;

namespace UITests.Bindings
{
    [Binding]
    public class Navigation
    {
        [Given(@"I look at the '(.*)' page")]
        [When(@"I look at the '(.*)' page")]
        public void GivenILookAtThePage(string page)
        {
            switch (page.ToLower())
            {
                case "home":
                    {
                        NavigationHelper.Navigate<ProductPage>();
                        break;
                    }
            }
        }
    }
}
