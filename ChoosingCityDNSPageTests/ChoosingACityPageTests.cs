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

        private static IEnumerable<TestCaseData> TestCaseListCookieCities()
        {
            yield return new TestCaseData("Новосибирск", "novosibirsk");
            yield return new TestCaseData("Москва", "moscow");
            yield return new TestCaseData("Энгельс", "engels");
        }

        private static IEnumerable<TestCaseData> TestCaseListPlaces()
        {
            yield return new TestCaseData("Северо-Кавказский", "Республика Дагестан", "Дербент");
            yield return new TestCaseData("Уральский", "Челябинская область", "Миасс");
            yield return new TestCaseData("Центральный", "Город Москва", "Москва");
        }

        private static IEnumerable<TestCaseData> TestCaseListPopularCities()
        {
            yield return new TestCaseData(new List<String>() {"Москва", "Санкт-Петербург", "Новосибирск",
                "Екатеринбург", "Нижний Новгород", "Казань",
                "Самара", "Владивосток"});
        }

        private static IEnumerable<TestCaseData> TestCaseListDistricts()
        {
            yield return new TestCaseData(new List<String>() {"Дальневосточный", "Приволжский", "Северо-Западный", 
                "Северо-Кавказский", "Сибирский", "Уральский", 
                "Центральный", "Южный"});
        }

        private IWebDriver CreateLocalWebDriver()
        {
            IWebDriver webDriverTest = new ChromeDriver();
            webDriverTest.Navigate().GoToUrl("https://www.dns-shop.ru/");
            webDriverTest.Manage().Window.Maximize();

            return webDriverTest;
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressSearchButtonTest(String expectedcity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityField(expectedcity, webDriverTest);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressEnterKeyTest(String expectedcity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityPressEnter(expectedcity, webDriverTest);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void ClearingSearchFieldTest(String expectedcity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .ClearingSearchField(expectedcity, webDriverTest);
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

        [Test, TestCaseSource("TestCaseListPopularCities")]
        public void ListPopularCitiesTest(List<String> expectedPopularCities)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            List<String> actualPopularCities = mainPage
                .ChoosingCity(webDriverTest)
                .FindPopularCities(webDriverTest);

            actualPopularCities.Sort();
            expectedPopularCities.Sort();

            Assert.AreEqual(expectedPopularCities, actualPopularCities);
        }

        [Test, TestCaseSource("TestCaseListPlaces")]
        public void SelectCityFromListTest(String expectedDistrict, String expectedRegion, String expectedCity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .SelectCityFromList(expectedDistrict, expectedRegion, expectedCity, webDriverTest);

            Assert.AreEqual(expectedCity, mainPage.GetCity(webDriverTest));
        }

        [Test, TestCaseSource("TestCaseListDistricts")]
        public void ListDistrictsTest(List<String> expectedDistricts)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            List<String> actualDistricts = mainPage
                .ChoosingCity(webDriverTest)
                .FindDistricts(webDriverTest);

            actualDistricts.Sort();
            expectedDistricts.Sort();

            Assert.AreEqual(expectedDistricts, actualDistricts);
        }

        [Test, TestCaseSource("TestCaseListCookieCities")]
        public void CityCookieTest(String expectedCity, String expectedCookieCity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityField(expectedCity, webDriverTest);

            Assert.AreEqual(expectedCookieCity, mainPage.GetCityCookie(webDriverTest));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void CityAfterSearchAndPageRefreshTest(String expectedcity)
        {
            IWebDriver webDriverTest = CreateLocalWebDriver();
            _ = webDriverTest.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest);
            mainPage
                .ChoosingCity(webDriverTest)
                .FindCityField(expectedcity, webDriverTest);

            webDriverTest.Navigate().Refresh();

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest));
        }
    }
}
