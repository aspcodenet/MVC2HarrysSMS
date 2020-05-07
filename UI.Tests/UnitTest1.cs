using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static IWebDriver _driver;
        [ClassInitialize]
        public static void Initialize(TestContext aContext)
        {
            _driver = new ChromeDriver();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _driver.Close();
            _driver.Dispose();
        }

        [TestMethod]
        public void ShouldLoadSmokeTest()
        {
            _driver.Navigate().GoToUrl("http://localhost:50521");
            Wait(5);
        }


        [TestMethod]
        public void CreateCustomerWithoutNameShouldGiveValidationError()
        {
            _driver.Navigate().GoToUrl("http://localhost:44391/Customer/Create");
            _driver.FindElement((By.Id("PersonNummer"))).SendKeys("1212129999");
            _driver.FindElement(By.Id("submitButton")).Click();
            Wait(5);
            Assert.IsTrue(_driver.FindElement(By.Id("Namn-error")).Displayed);
       }


        private void Wait(int secs = 1)
        {
            System.Threading.Thread.Sleep(secs*1000);
        }
    }
}
