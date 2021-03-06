using ChoosingCityDNSPageTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ChoosingCityDNSPageTests
{
    [Parallelizable(ParallelScope.All)]
    public class ChoosingACityPageTests
    {
        ThreadLocal<IWebDriver> webDriverTest = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void Init()
        {
            var options = new ChromeOptions();
            options.AddArgument("-no-sandbox");

            webDriverTest.Value = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            webDriverTest.Value.Navigate().GoToUrl("https://www.dns-shop.ru/");
            webDriverTest.Value.Manage().Window.Maximize();
        }

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

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressSearchButtonTest(String expectedcity)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressSearchButton(expectedcity, webDriverTest.Value);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressEnterKeyTest(String expectedcity)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressEnter(expectedcity, webDriverTest.Value);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void ClearSearchFieldByPressClearButtonTest(String expectedcity)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .ClearSearchFieldByPressClearButton(expectedcity, webDriverTest.Value);
        }

        [Test]
        public void ClosingCitySelectionPageTest()
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .CloseCitySelectionPage(webDriverTest.Value);

            Assert.IsFalse(mainPage.CheckingOpenCitySelectPage(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListPopularCities")]
        public void SearchPopularCitiesOnCitySelectionPageTest(List<String> expectedPopularCities)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            List<String> actualPopularCities = mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .SearchPopularCitiesOnCitySelectionPage(webDriverTest.Value);

            actualPopularCities.Sort();
            expectedPopularCities.Sort();

            Assert.AreEqual(expectedPopularCities, actualPopularCities);
        }

        [Test, TestCaseSource("TestCaseListPlaces")]
        public void SelectCityUsingListTest(String expectedDistrict, String expectedRegion, String expectedCity)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .SelectCityUsingList(expectedDistrict, expectedRegion, expectedCity, webDriverTest.Value);

            Assert.AreEqual(expectedCity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListDistricts")]
        public void FindDistrictsTest(List<String> expectedDistricts)
        {
            var mainPage = new MainPagePageObject(webDriverTest.Value);
            List<String> actualDistricts = mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .FindDistricts(webDriverTest.Value);

            actualDistricts.Sort();
            expectedDistricts.Sort();

            Assert.AreEqual(expectedDistricts, actualDistricts);
        }

        [Test, TestCaseSource("TestCaseListCookieCities")]
        public void CityCookieTest(String expectedCity, String expectedCookieCity)
        {
            var actualCookieCity = new MainPagePageObject(webDriverTest.Value)
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressSearchButton(expectedCity, webDriverTest.Value)
                .GetCityCookie(webDriverTest.Value);

            Assert.AreEqual(expectedCookieCity, actualCookieCity);
        }

        [TearDown]
        public void Cleanup()
        {
            webDriverTest.Value.Close();
            webDriverTest.Value.Quit();
            webDriverTest.Value = null;
        }
    }
}
