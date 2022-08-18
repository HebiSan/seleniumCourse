using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTests.Pages
{
    public class CartPage : PageBase
    {
        public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void ChooseCourierDelivery()
        {
            // Открыть лайтбокс курьерской доставки
            driver.FindElement(openDeliverySettingsLocator).Click();
            driver.FindElement(chooseCourierDeliveryLocator).Click();
            // Ожидать, пока определится адрес
            wait.Until(driver => driver.FindElement(addressErrorLocator).Displayed);
        }

        private By openDeliverySettingsLocator = By.CssSelector("button[class*=delivery]");
        private By chooseCourierDeliveryLocator = By.XPath("//div[contains(text(),'Курьер')]");
        private By addressErrorLocator = By.ClassName("error-informer");
    }
}