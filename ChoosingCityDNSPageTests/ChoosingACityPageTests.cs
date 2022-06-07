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
        ThreadLocal<IWebDriver> webDriverTest = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void Init()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless");
            options.AddArgument("-no-sandbox");
            // options.AddArguments("--disable-dev-shm-usage");

            webDriverTest.Value = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            // webDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
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
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressSearchButton(expectedcity, webDriverTest.Value);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void SearchCityByPressEnterKeyTest(String expectedcity)
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressEnter(expectedcity, webDriverTest.Value);

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListCities")]
        public void ClearSearchFieldByPressClearButtonTest(String expectedcity)
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .ClearSearchFieldByPressClearButton(expectedcity, webDriverTest.Value);
        }

        [Test]
        public void ClosingCitySelectionPageTest()
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .CloseCitySelectionPage(webDriverTest.Value);

            Assert.IsFalse(mainPage.CheckingOpenCitySelectPage(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListPopularCities")]
        public void SearchPopularCitiesOnCitySelectionPageTest(List<String> expectedPopularCities)
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

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
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .SelectCityUsingList(expectedDistrict, expectedRegion, expectedCity, webDriverTest.Value);

            Assert.AreEqual(expectedCity, mainPage.GetCity(webDriverTest.Value));
        }

        [Test, TestCaseSource("TestCaseListDistricts")]
        public void FindDistrictsTest(List<String> expectedDistricts)
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

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
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var actualCookieCity = new MainPagePageObject(webDriverTest.Value)
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressSearchButton(expectedCity, webDriverTest.Value)
                .GetCityCookie(webDriverTest.Value);

            Assert.AreEqual(expectedCookieCity, actualCookieCity);
        }

        /* [Test, TestCaseSource("TestCaseListCities")]
        public void CityAfterSearchAndPageRefreshTest(String expectedcity)
        {
            _ = webDriverTest.Value.Manage().Timeouts().ImplicitWait;

            var mainPage = new MainPagePageObject(webDriverTest.Value);
            mainPage
                .OpenCitySelectPage(webDriverTest.Value)
                .EnterCityInSearchFieldAndPressSearchButton(expectedcity, webDriverTest.Value);

            webDriverTest.Value.Navigate().Refresh();

            Assert.AreEqual(expectedcity, mainPage.GetCity(webDriverTest.Value));
        } */ 

        [TearDown]
        public void Cleanup()
        {
            if (webDriverTest.Value != null)
            {
                Console.WriteLine("finish");
                webDriverTest.Value.Quit();
            }
        }
    }
}
