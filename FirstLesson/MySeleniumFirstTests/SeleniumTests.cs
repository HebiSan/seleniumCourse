using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FirstSeleniumTests
{
    [TestFixture]
    public class SeleniumTests
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
        public void FillDataForCourierDelivery_Success()
        {
            #region Locators

            //верстка на сайте поменялась, не было некоторых элементов, которые искали на практике
            //var searchInput = driver.FindElement(By.Id("searchform"));
            //var searchHelp = driver.FindElement(By.Name("searchformadvanced"));
            //var years = driver.FindElements(By.CssSelector("select[name='year_begin'] option:not([selected])"));
            //var link = driver.FindElement(By.LinkText("Как сделать заказ"));
            //Домашка:
            var cookiePolicyAgreeLocator = By.CssSelector("button[class^=cookie-policy]");
            var booksMenuLocator = By.CssSelector("[data-toggle=header-genres]");
            var allBooksLocator = By.XPath("//*[@id='header-genres']//a[@href='/books/']");
            var addBookInCartLocator = By.XPath("(//*[starts-with(@id,'buy')])[1]");
            var issueOrderLocator = By.XPath("(//*[starts-with(@id,'buy')])[text()='ОФОРМИТЬ'][1]");
            var beginOrderLocator = By.CssSelector("[id=cart-total-default] button[type=submit]");
            var openDeliverySettingsLocator = By.CssSelector("button[class*=delivery]");
            var chooseCourierDeliveryLocator = By.XPath("//div[contains(text(),'Курьер')]");
            var chooseSelfDeliveryLocator = By.XPath("//div[contains(text(),'Самовывоз')]");
            var addressInputLocator = By.Id("deliveryAddress");
            var addressErrorLocator = By.ClassName("error-informer");
            var cityErrorLocator = By.XPath(
                "//div[@class='delivery--courier-delivery']//span[contains(text(),'Извините, но пока мы не доставляем заказы в этот населённый пункт')]");
            var firstSuggestedAddressLocator = By.CssSelector("ymaps[class$=suggest-item-0]");
            var loaderLocator = By.XPath("//div[@class='delivery--courier-delivery']//div[@class='loading']");
            var bigLoaderLocator = By.ClassName("loading");
            var appropriateServicesLocator = By.XPath("//*[@class='delivery--courier-delivery']//div[1]/li");
            var closeButtonLocator = By.ClassName("delivery--button-block--close");
            var saveButtonLocator = By.ClassName("button-save");
            var chooseAnotherButtonLocator = By.ClassName("button-close");
            var closeImgLocator = By.CssSelector("img[class^=x-close]");
            var lightBoxLocator = By.ClassName("container");

            #endregion

            driver.Navigate().GoToUrl("https://www.labirint.ru/");
            // Кликнуть по кнопке для принятия политики Cookie
            driver.FindElement(cookiePolicyAgreeLocator).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(cookiePolicyAgreeLocator));
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
            // Открыть лайтбокс курьерской доставки
            driver.FindElement(openDeliverySettingsLocator).Click();
            driver.FindElement(chooseCourierDeliveryLocator).Click();
            // Ожидать, пока определится адрес
            wait.Until(driver => driver.FindElement(addressErrorLocator).Displayed);
            // В открывшемся лайтбоксе в “Населенный пункт” ввести некорректный город
            driver.FindElement(addressInputLocator).Clear();
            driver.FindElement(addressInputLocator).SendKeys("KF-89");
            // Убрать фокус с поля
            driver.FindElement(addressInputLocator).SendKeys(Keys.Enter);
            // Проверить, что появилась ошибка “Неизвестный город”
            wait.Until(ExpectedConditions.ElementIsVisible(cityErrorLocator));
            // Очищаем поле ввода, вводим адрес
            driver.FindElement(addressInputLocator).Clear();
            driver.FindElement(addressInputLocator).SendKeys("Малопрудная улица, 5, Екатеринбург");
            // Кликнуть по появившейся подсказке - ? не смогла придумать, как добиться появления выпадающего меню
            // driver.FindElement(firstSuggestedAddressLocator).Click();
            // Подождать, когда лоадер скроется
            wait.Until(ExpectedConditions.ElementIsVisible(loaderLocator));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loaderLocator));
            wait.Until(ExpectedConditions.ElementIsVisible(appropriateServicesLocator));
            // Выбрать доставку
            driver.FindElement(appropriateServicesLocator).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(saveButtonLocator));
            // Кликнуть по кнопке “Выбрать эту доставку"
            driver.FindElement(saveButtonLocator).Click();
            // Проверить, что не отображается лайтбокс курьерской доставки
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(lightBoxLocator));
        }

        // [Test]
        // public void TestDatepicker()
        // {
        //     driver.Navigate().GoToUrl("https://jqueryui.com/datepicker/");
        //     (driver as IJavaScriptExecutor)
        //         .ExecuteScript($"$('.ui-datepicker').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");
        // }

        [Test]
        public void Labirint_AuthorizeViaMyLabirintLink()
        {
            var lightboxLocator = By.ClassName("lab-modal-content");
            var myLabirintLocator = By.ClassName("top-link-main_cabinet");
            var authorizeButtonLocator =
                By.CssSelector(
                    ".dropdown-block-opened [data-sendto-params='auth-registration'].b-header-e-border-top");

            driver.Navigate().GoToUrl("https://www.labirint.ru/");

            new Actions(driver)
                .MoveToElement(driver.FindElement(myLabirintLocator))
                .Build()
                .Perform();

            wait.Until(driver => driver.FindElement(authorizeButtonLocator));

            driver.FindElement(authorizeButtonLocator).Click();

            Assert.IsTrue(driver.FindElement(lightboxLocator).Displayed,
                "После клика на 'Вход или регистрация в Лабиринте' лайтбокс не отобразился");
        }

        [Test]
        public void Labirint_FillFormOnGuestbook_Success()
        {
            var searchInArchiveLocator = By.Id("btwo");
            var fieldNameLocator = By.Name("sname");
            var fieldKeywordsLocator = By.CssSelector("#b_nested [name=keywords]");
            var yearEndLocator = By.Name("year_end");
            var notForThisLinkLocator = By.Id("hd");
            var lightboxLocator = By.Id("notForGuestbook");

            driver.Navigate().GoToUrl("https://www.labirint.ru/guestbook");
            driver.FindElement(searchInArchiveLocator).Click();
            driver.FindElement(fieldNameLocator).SendKeys("Софья");
            driver.FindElement(fieldKeywordsLocator).SendKeys("blablabla");
            driver.FindElement(fieldKeywordsLocator).Clear();
            var selectElement = new SelectElement(driver.FindElement(yearEndLocator));
            selectElement.SelectByText("2019");
            var selectedYear = selectElement.SelectedOption.Text;

            Assert.AreEqual("2019", selectedYear, "Неверно выбран год окончания поиска");

            driver.FindElement(notForThisLinkLocator).Click();

            Assert.IsTrue(driver.FindElement(lightboxLocator).Displayed, "Не отобразился лайтбокс");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }

        private IWebDriver driver;
        private WebDriverWait wait;
    }
}