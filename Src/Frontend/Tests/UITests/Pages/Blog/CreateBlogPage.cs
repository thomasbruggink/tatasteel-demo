using OpenQA.Selenium;

namespace UITests.Pages.Blog
{
    public class CreateBlogPage : PageBase<CreateBlogPage>, IPage
    {
        public string GetUrl()
        {
            return "/Blog/Index";
        }

        public string Title {
            get { return GetTitleField().Text; }
            set { GetTitleField().SendKeys(value); }
        }

        public string Content
        {
            get { return GetContentField().Text; }
            set {GetContentField().SendKeys(value);}
        }

        public void SubmitBlog()
        {
            var saveButton = AssemblyConfiguration.Driver.FindElement(By.Id("blogSubmitButton"));
            saveButton.Click();
        }

        private IWebElement GetTitleField()
        {
            return AssemblyConfiguration.Driver.FindElement(By.Id("titleInputField"));
        }

        private IWebElement GetContentField()
        {
            return AssemblyConfiguration.Driver.FindElement(By.Id("contentInputField"));
        }
    }
}
