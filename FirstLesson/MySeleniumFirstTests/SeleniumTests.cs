using System;
using FirstSeleniumTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTests
{
    [TestFixture]
    public class SeleniumTests : SeleniumTestBase
    {
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
        public void CartPage_EnterInvalidCity_ErrorCity()
        {
            //arrange

            #region data

            var homePage = new HomePage(driver, wait);
            var cartPage = new CartPage(driver, wait);
            var courierDeliveryDeliveryLightbox = new CourierDeliveryLightbox(driver, wait);
            var invalidAddress = "KF-89";

            #endregion

            homePage.OpenPage();
            homePage.AddBookToCart();

            //act
            cartPage.ChooseCourierDelivery();
            courierDeliveryDeliveryLightbox.EnterAddress(invalidAddress, true);

            // assert
            courierDeliveryDeliveryLightbox.WaitCityError();
        }

        [Test]
        public void CartPage_FillAll_Success()
        {
            //arrange

            #region data

            var homePage = new HomePage(driver, wait);
            var cartPage = new CartPage(driver, wait);
            var courierDeliveryDeliveryLightbox = new CourierDeliveryLightbox(driver, wait);
            var validAddress = "Малопрудная улица, 5, Екатеринбург";

            #endregion

            homePage.OpenPage();
            homePage.AddBookToCart();

            //act
            cartPage.ChooseCourierDelivery();
            courierDeliveryDeliveryLightbox.EnterAddress(validAddress);
            courierDeliveryDeliveryLightbox.ChooseService();

            //assert
            courierDeliveryDeliveryLightbox.WaitLightboxInvisible();
        }


        [Test]
        public void TestDatepicker()
        {
            var expectedDate = DateTime.Today.AddDays(8).ToString("MM'/'dd'/'yyyy");
            driver.Navigate().GoToUrl("https://jqueryui.com/datepicker/");
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("demo-frame")));
            (driver as IJavaScriptExecutor)
                .ExecuteScript($"$('.hasDatepicker').datepicker('setDate','{expectedDate}')");
            var actualDate = driver.FindElement(By.ClassName("hasDatepicker")).GetAttribute("value");
            Assert.AreEqual(expectedDate, actualDate, "Дата была выставлена неверно");
        }

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
    }
}