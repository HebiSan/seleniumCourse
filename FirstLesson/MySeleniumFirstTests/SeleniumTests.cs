using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ClassLibrary1
{
    public class SeleniumTests
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            driver = new ChromeDriver("D:\\Downloads\\chromedriver_win32", options);
            //waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void MyFirstTest()
        {
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
            var searchInput = driver.FindElement(By.Name("search"));
            var searchButton = driver.FindElement(By.Name("go"));
            searchInput.SendKeys("Selenium");
            searchButton.Click();
            Assert.IsTrue(driver.Title.Contains("Selenium"), "Неверный заголовок страницы");
        }

        [Test]
        public void Locators()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru/books");
            //верстка на сайте поменялась, не было некоторых элементов, которые искали на практике
            //var searchInput = driver.FindElement(By.Id("searchform"));
            //var searchHelp = driver.FindElement(By.Name("searchformadvanced"));
            //var years = driver.FindElements(By.CssSelector("select[name='year_begin'] option:not([selected])"));
            //var link = driver.FindElement(By.LinkText("Как сделать заказ"));
            //Домашка:
            var cookiePolicyAgree = By.CssSelector("button[class^=cookie-policy]");
            var booksMenu = By.LinkText("Книги");
            var allBooks = By.LinkText("Все книги");
            var addBookInCart = By.XPath("(//*[starts-with(@id,'buy')])[text()='В КОРЗИНУ'][1]");
            var issueOrder = By.XPath("(//*[starts-with(@id,'buy')])[text()='ОФОРМИТЬ'][1]");
            var beginOrder =
                By.CssSelector(
                    "button[type='submit']"); //синей кнопки нет на странице, локатор на большую красную в футере
            var openDeliverySettings = By.LinkText("Выбрать новое место и способ"); //нет галочки курьерской доставки
            var chooseCourierDelivery = By.XPath("//div[contains(text(),'Курьер')]");
            var chooseSelfDelivery = By.XPath("//div[contains(text(),'Самовывоз')]");
            var addressInput = By.Id("deliveryAddress");
            var addressError = By.CssSelector("//div[contains(text(),'Уточните адрес для доставки курьером')]");
            var cityError =
                By.CssSelector(
                    "//div[contains(text(),'Извините, но пока мы не доставляем заказы в этот населённый пункт')]");
            var loader = By.ClassName("loading");
            var appropriateServices =
                By.XPath("//*[@class='delivery--courier-delivery']//li"); //список найденных служб доставки
            var closeButton = By.ClassName("delivery--button-block--close");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        private IWebDriver driver;
        private WebDriverWait waiter;
    }
}