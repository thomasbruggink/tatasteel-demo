// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpecFlow.GeneratedTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class ProductsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Products", @"	In order to ensure all product information can be retrieved correctly I want to make sure the API works as expected
	The product information is stored in a local .json file and is read and parsed when the api is called

	To verify the availability of a product a third party system is called.
	To make sure product information is always available downtime on the availability system cannot disrupt product information.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Products")))
            {
                global::SpecFlow.GeneratedTests.Features.ProductsFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(TestContext);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Name",
                        "ImageId"});
            table1.AddRow(new string[] {
                        "1XS5",
                        "Wooden plank",
                        "plank.png"});
            table1.AddRow(new string[] {
                        "556X",
                        "Steel plank",
                        "splank.png"});
            table1.AddRow(new string[] {
                        "A2",
                        "X12 Screws",
                        "screws.png"});
            table1.AddRow(new string[] {
                        "T42",
                        "Heavy tiles",
                        "h_tiles.png"});
            table1.AddRow(new string[] {
                        "T21",
                        "Light tiles",
                        "l_tiles.png"});
            testRunner.Given("the following product information is available", ((string)(null)), table1, "Given ");
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Count"});
            table2.AddRow(new string[] {
                        "1XS5",
                        "100"});
            table2.AddRow(new string[] {
                        "556X",
                        "2"});
            table2.AddRow(new string[] {
                        "A2",
                        "10"});
            table2.AddRow(new string[] {
                        "T42",
                        "76"});
            table2.AddRow(new string[] {
                        "T21",
                        "901"});
            testRunner.And("the following item count is available", ((string)(null)), table2, "And ");
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("products")]
        public virtual void RequestingProductsFromTheAPI()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting products from the API", new string[] {
                        "products"});
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            testRunner.When("I request products", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Name",
                        "ImageId",
                        "Count"});
            table3.AddRow(new string[] {
                        "1XS5",
                        "Wooden plank",
                        "plank.png",
                        "100"});
            table3.AddRow(new string[] {
                        "556X",
                        "Steel plank",
                        "splank.png",
                        "2"});
            table3.AddRow(new string[] {
                        "A2",
                        "X12 Screws",
                        "screws.png",
                        "10"});
            table3.AddRow(new string[] {
                        "T42",
                        "Heavy tiles",
                        "h_tiles.png",
                        "76"});
            table3.AddRow(new string[] {
                        "T21",
                        "Light tiles",
                        "l_tiles.png",
                        "901"});
            testRunner.Then("the following products are returned", ((string)(null)), table3, "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("paged")]
        public virtual void RequestingASpecificAmountOfProducts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting a specific amount of products", new string[] {
                        "products",
                        "paged"});
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            testRunner.When("I request \'2\' products", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Name",
                        "ImageId",
                        "Count"});
            table4.AddRow(new string[] {
                        "1XS5",
                        "Wooden plank",
                        "plank.png",
                        "100"});
            table4.AddRow(new string[] {
                        "556X",
                        "Steel plank",
                        "splank.png",
                        "2"});
            testRunner.Then("the following products are returned", ((string)(null)), table4, "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("paged")]
        public virtual void RequestingASpecificAmountOfProductsFromASecondPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting a specific amount of products from a second page", new string[] {
                        "products",
                        "paged"});
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            testRunner.When("I request \'2\' products from page \'3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Name",
                        "ImageId",
                        "Count"});
            table5.AddRow(new string[] {
                        "T21",
                        "Light tiles",
                        "l_tiles.png",
                        "901"});
            testRunner.Then("the following products are returned", ((string)(null)), table5, "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Products")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("products")]
        public virtual void ItemsAreStillReturnedWhenTheAvailibiltySystemIsOffline()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Items are still returned when the availibilty system is offline", new string[] {
                        "products"});
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            testRunner.Given("the availibility system is not available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
            testRunner.When("I request products", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
            testRunner.Then("the product api returned the \'Partial Content\' response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductId",
                        "Name",
                        "ImageId",
                        "Count"});
            table6.AddRow(new string[] {
                        "1XS5",
                        "Wooden plank",
                        "plank.png",
                        "-"});
            table6.AddRow(new string[] {
                        "556X",
                        "Steel plank",
                        "splank.png",
                        "-"});
            table6.AddRow(new string[] {
                        "A2",
                        "X12 Screws",
                        "screws.png",
                        "-"});
            table6.AddRow(new string[] {
                        "T42",
                        "Heavy tiles",
                        "h_tiles.png",
                        "-"});
            table6.AddRow(new string[] {
                        "T21",
                        "Light tiles",
                        "l_tiles.png",
                        "-"});
            testRunner.And("the following products are returned", ((string)(null)), table6, "And ");
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
