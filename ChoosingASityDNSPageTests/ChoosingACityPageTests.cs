using ChoosingASityDNSPageTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChoosingASityDNSPageTests
{
    public class ChoosingACityPageTests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl("https://www.dns-shop.ru/");
        }

        [Test]
        public void SearchCityByPressSearchButtonTest()
        {
            string expectedCity = "Новосибирск";

            _ = _webDriver.Manage().Timeouts().ImplicitWait;
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .choosingCity()
                .findCityField(expectedCity);

            string actualCity = mainPage.getCity();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void SearchCityByPressEnterKeyTest()
        {
            string expectedCity = "Новосибирск";

            _ = _webDriver.Manage().Timeouts().ImplicitWait;
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .choosingCity()
                .findCityPressEnter(expectedCity);

            string actualCity = mainPage.getCity();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void ClearingSearchFieldTest()
        {
            string expectedCity = "Новосибирск";

            _ = _webDriver.Manage().Timeouts().ImplicitWait;
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .choosingCity()
                .clearingSearchField(expectedCity);
        }

        [Test]
        public void ClosingPageTest()
        {
            _ = _webDriver.Manage().Timeouts().ImplicitWait;
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .choosingCity()
                .closingPage();

            Assert.IsFalse(mainPage.checkingCitySelectionPage());
        }
    }
}
