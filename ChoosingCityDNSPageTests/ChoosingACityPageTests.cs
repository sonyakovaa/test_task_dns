using ChoosingCityDNSPageTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChoosingCityDNSPageTests
{
    [Parallelizable(ParallelScope.All)]
    public class ChoosingACityPageTests
    {
        private static IEnumerable<TestCaseData> TestCaseListCities()
        {
            yield return new TestCaseData("Новосибирск");
            yield return new TestCaseData("Москва");
            yield return new TestCaseData("Энгельс");
        }

        private static IEnumerable<TestCaseData> TestCaseListPlaces()
        {
            yield return new TestCaseData("Северо-Кавказский", "Республика Дагестан", "Дербент");
            yield return new TestCaseData("Уральский", "Челябинская область", "Миасс");
            yield return new TestCaseData("Центральный", "Город Москва", "Москва");
        }

        private IWebDriver CreateLocalWebDriver()
        {
            IWebDriver webDriverTest = new ChromeDriver();
            webDriverTest.Navigate().GoToUrl("https://www.dns-shop.ru/");
            webDriverTest.Manage().Window.Maximize();

            return webDriverTest;
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressSearchButtonTest(String city)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityField(city, webDriverTest);

            string actualCity = mainPage.GetCity(webDriverTest);

            Assert.AreEqual(city, actualCity);
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressEnterKeyTest(String city)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityPressEnter(city, webDriverTest);

            string actualCity = mainPage.GetCity(webDriverTest);

            Assert.AreEqual(city, actualCity);
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void ClearingSearchFieldTest(String city)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .ClearingSearchField(city, webDriverTest);
        }

        [Test]
        public void ClosingPageTest()
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .ClosingPage(webDriverTest);

            Assert.IsFalse(mainPage.CheckingCitySelectionPage(webDriverTest));
        }

        [Test]
        public void ListPopularCitiesTest()
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            List<String> actualCities = mainPage
                .ChoosingCity(webDriverTest)
                .FindPopularCities(webDriverTest);

            actualCities.Sort();

            List<String> expectedCities = new List<String>() {"Москва", "Санкт-Петербург", "Новосибирск",
                "Екатеринбург", "Нижний Новгород", "Казань",
                "Самара", "Владивосток" };
            expectedCities.Sort();
            Assert.AreEqual(expectedCities, actualCities);
        }

        [Test, TestCaseSource("TestCaseListPlaces")]
        public void SelectCityFromListTest(String district, String region, String city)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .SelectCityFromList(district, region, city, webDriverTest);
                
            string actualCity = mainPage.GetCity(webDriverTest);

            Assert.AreEqual(city, actualCity);
        }


    }
}
