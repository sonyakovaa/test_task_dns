using ChoosingCityDNSPageTests.PageObjects;
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

namespace ChoosingCityDNSPageTests
{
    [Parallelizable]
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

        private static IEnumerable<TestCaseData> TestCaseListCities()
        {
            yield return new TestCaseData("Новосибирск");
            yield return new TestCaseData("Москва");
            yield return new TestCaseData("Энгельс");
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressSearchButtonTest(String city)
        {
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .FindCityField(city);

            string actualCity = mainPage.GetCity();

            Assert.AreEqual(city, actualCity);
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressEnterKeyTest(String city)
        {
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .FindCityPressEnter(city);

            string actualCity = mainPage.GetCity();

            Assert.AreEqual(city, actualCity);
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void ClearingSearchFieldTest(String city)
        {
            var mainPage = new MainPagePageObject(_webDriver);
            mainPage
                .ChoosingCity()
                .ClearingSearchField(city);
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
            List<String> actualCities = mainPage
                .ChoosingCity()
                .FindPopularCities();

            actualCities.Sort();

            List<String> expectedCities = new List<String>() {"Москва", "Санкт-Петербург", "Новосибирск", 
                "Екатеринбург", "Нижний Новгород", "Казань", 
                "Самара", "Владивосток" };
            expectedCities.Sort();
            Assert.AreEqual(expectedCities, actualCities);
        }
    }
}
