using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTests.Pages
{
    public abstract class PageBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public PageBase(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
    }
}