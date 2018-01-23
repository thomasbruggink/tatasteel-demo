using OpenQA.Selenium;

namespace UITests.Pages.Blog.Parts
{
    public class BlogPart
    {
        private readonly IWebElement _blogElement;

        public BlogPart(IWebElement blogElement)
        {
            _blogElement = blogElement;
        }

        public string Title => _blogElement.FindElement(By.TagName("h1")).Text;

        public string Content => _blogElement.FindElement(By.ClassName("blog-content")).Text;

        public string Writer => _blogElement.FindElement(By.ClassName("blog-writer")).Text;
    }
}
