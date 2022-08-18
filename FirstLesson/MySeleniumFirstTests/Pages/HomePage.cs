using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FirstSeleniumTests.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(url);
            // Кликнуть по кнопке для принятия политики Cookie
            driver.FindElement(cookiePolicyAgreeLocator).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(cookiePolicyAgreeLocator));
        }

        public void AddBookToCart()
        {
            // В шапке навести на ссылку “Книги”
            new Actions(driver)
                .MoveToElement(driver.FindElement(booksMenuLocator)).Build()
                .Perform();
            // Дождаться показа “Все книги”
            wait.Until(ExpectedConditions.ElementIsVisible(allBooksLocator));
            // Кликнуть в раскрывшемся списке по ссылке “Все книги”
            driver.FindElement(allBooksLocator).Click();
            // (*) Проверить, что перешли на страницу с URL = https://www.labirint.ru/books/
            wait.Until(ExpectedConditions.UrlToBe("https://www.labirint.ru/books/"));
            // Кликнуть по кнопке “В корзину” у первой книги на странице
            driver.FindElement(addBookInCartLocator).Click();
            // Кликнуть по кнопке “Оформить” у первой книги на странице
            driver.FindElement(issueOrderLocator).Click();
            // На открывшейся странице кликнуть по кнопке “Начать оформление”
            driver.FindElement(beginOrderLocator).Click();
        }

        private string url = "https://www.labirint.ru/";
        private By cookiePolicyAgreeLocator = By.CssSelector("button[class^=cookie-policy]");
        private By booksMenuLocator = By.CssSelector("[data-toggle=header-genres]");
        private By allBooksLocator = By.XPath("//*[@id='header-genres']//a[@href='/books/']");
        private By addBookInCartLocator = By.XPath("(//*[starts-with(@id,'buy')])[1]");
        private By issueOrderLocator = By.XPath("(//*[starts-with(@id,'buy')])[text()='ОФОРМИТЬ'][1]");
        private By beginOrderLocator = By.CssSelector("[id=cart-total-default] button[type=submit]");
    }
}