using ChoosingASityDNSPageTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _ = _webDriver.Manage().Timeouts().ImplicitWait;
        }

        [Test]
        public void SearchCityByPressSearchButtonTest()
        {
            string expectedCity = "Новосибирск";

            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .FindCityField(expectedCity);

            string actualCity = mainPage.GetCity();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void SearchCityByPressEnterKeyTest()
        {
            string expectedCity = "Новосибирск";

            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .FindCityPressEnter(expectedCity);

            string actualCity = mainPage.GetCity();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void ClearingSearchFieldTest()
        {
            string expectedCity = "Новосибирск";

            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .ClearingSearchField(expectedCity);
        }

        [Test]
        public void ClosingPageTest()
        {
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .ClosingPage();

            Assert.IsFalse(mainPage.CheckingCitySelectionPage());
        }

        [Test]
        public void ListPopularCitiesTest()
        {
            var mainPage = new MainPagePageObject(_webDriver);
            List<String> actualSities = mainPage
                .ChoosingCity()
                .FindPopularCities();

            actualSities.Sort();

            List<String> expectedSities = new List<String>() {"Москва", "Санкт-Петербург", "Новосибирск", 
                "Екатеринбург", "Нижний Новгород", "Казань", 
                "Самара", "Владивосток" };
            expectedSities.Sort();
            Assert.AreEqual(expectedSities, actualSities);
        }
    }
}
