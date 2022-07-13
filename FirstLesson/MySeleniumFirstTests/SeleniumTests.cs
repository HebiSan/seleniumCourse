using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTests
{
    [TestFixture]
    public class SeleniumTests
    {
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            driver = new ChromeDriver(options);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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
            var booksMenu = By.CssSelector("[data-toggle=header-genres]");
            var allBooks = By.XPath("//*[@id='header-genres']//a[@href='/books/']");
            var addBookInCart = By.XPath("(//*[starts-with(@id,'buy')])[text()='В КОРЗИНУ'][1]");
            var issueOrder = By.XPath("(//*[starts-with(@id,'buy')])[text()='ОФОРМИТЬ'][1]");
            var beginOrder = By.CssSelector("[id=cart-total-default] button[type=submit]");
            var openDeliverySettings = By.CssSelector("button[class*=delivery]"); 
            var chooseCourierDelivery = By.XPath("//div[contains(text(),'Курьер')]");
            var chooseSelfDelivery = By.XPath("//div[contains(text(),'Самовывоз')]");
            var addressInput = By.Id("deliveryAddress");
            var addressError = By.CssSelector("//div[contains(text(),'Уточните адрес для доставки курьером')]");
            var cityError = By.CssSelector("//div[contains(text(),'Извините, но пока мы не доставляем заказы в этот населённый пункт')]");
            var loader = By.ClassName("loading");
            var appropriateServices = By.XPath("//*[@class='delivery--courier-delivery']//div[2]/li");
            var closeButton = By.ClassName("delivery--button-block--close");
            var saveButton = By.ClassName("button-save");
            var chooseAnotherButton = By.ClassName("button-close");
            var closeImg = By.CssSelector("img[class^=x-close]");
            var lightBox = By.ClassName("container");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }

        private IWebDriver driver;
        private WebDriverWait waiter;
    }
}