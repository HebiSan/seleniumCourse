using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FirstSeleniumTests.Pages
{
    public class CourierDeliveryLightbox : PageBase
    {
        public CourierDeliveryLightbox(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void ChooseService()
        {
            // Выбрать доставку
            driver.FindElement(appropriateServicesLocator).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(saveButtonLocator));
            // Кликнуть по кнопке “Выбрать эту доставку"
            driver.FindElement(saveButtonLocator).Click();
        }

        public void EnterAddress(string address, bool isError = false)
        {
            // Очищаем поле ввода, вводим адрес
            driver.FindElement(addressInputLocator).Clear();
            driver.FindElement(addressInputLocator).SendKeys(address);
            if (!isError)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(loaderLocator));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loaderLocator));
                wait.Until(ExpectedConditions.ElementIsVisible(appropriateServicesLocator));
            }
            else
                driver.FindElement(addressInputLocator).SendKeys(Keys.Enter);
        }

        public void WaitCityError()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(cityErrorLocator));
        }

        public void WaitLightboxInvisible()
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(lightBoxLocator));
        }

        private By addressInputLocator = By.Id("deliveryAddress");

        private By cityErrorLocator = By.XPath(
            "//div[@class='delivery--courier-delivery']//span[contains(text(),'Извините, но пока мы не доставляем заказы в этот населённый пункт')]");

        private By loaderLocator = By.XPath("//div[@class='delivery--courier-delivery']//div[@class='loading']");
        private By appropriateServicesLocator = By.XPath("//*[@class='delivery--courier-delivery']//div[1]/li");
        private By saveButtonLocator = By.ClassName("button-save");
        private By lightBoxLocator = By.ClassName("container");
    }
}