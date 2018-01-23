using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.Blog;
using UITests.Pages.User;

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
                case "profile":
                    {
                        NavigationHelper.Navigate<UserPage>();
                        break;
                    }
                case "blog":
                    {
                        NavigationHelper.Navigate<BlogPage>();
                        break;
                    }
            }
        }
    }
}
