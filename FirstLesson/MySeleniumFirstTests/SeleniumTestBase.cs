using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTests
{
    public abstract class SeleniumTestBase
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--incognito");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

     
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }

        protected IWebDriver driver;
        protected WebDriverWait wait;
    }
}