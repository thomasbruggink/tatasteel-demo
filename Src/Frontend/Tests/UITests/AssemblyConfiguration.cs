using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace UITests
{
    [TestClass]
    public class AssemblyConfiguration
    {
        public static IWebDriver Driver;

        [AssemblyInitialize]
        public static void ConfigureAssembly(TestContext testContext)
        {
            Driver = new ChromeDriver()
            {
                Url = "http://localhost:49854/"
            };
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            Driver?.Dispose();
            Driver = null;
        }
    }
}