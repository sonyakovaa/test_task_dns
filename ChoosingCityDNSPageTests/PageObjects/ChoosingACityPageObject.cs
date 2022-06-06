using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace ChoosingCityDNSPageTests.PageObjects
{
    // //div[text()="Ваш город"]
    // "//span[text()='Дальневосточный']"
    // div[class="base-modal__header-content"] svg[class="base-modal__header-close-icon dns-icon_remove"]
    // //ul[@class="cities"]/li[@class="modal-row"]/a[@href="javascript:"]/span[text()="Москва"]
    class ChoosingACityPageObject
    {
        private readonly IWebDriver _webDriver;

        private readonly By _findlistPopularCities = By.CssSelector(".big-cities-bubble-list");
        private readonly By _searchField = By.CssSelector(".base-ui-input-search__input");
        private readonly By _citySearchButton = By.CssSelector(".base-ui-input-search__icon" +
            ".base-ui-input-search__icon_search" +
            ".base-ui-input-search__icon-location_left");
        private readonly By _clearSearchFieldButton = By.CssSelector(".base-ui-input-search__clear" +
            ".base-ui-input-search__clear_grey");
        private readonly By _closePageButton = By.CssSelector("div[class='base-modal__header-content'] " +
            "svg[class='base-modal__header-close-icon dns-icon_remove']");
        private readonly By _listPopularCities = By.CssSelector(".city-bubble");
        private readonly String _findDistrictAndRegion = "//span[text()='']";
        private readonly String _findCity = "//ul[@class='cities']/li[@class='modal-row']/a[@href='javascript:']/span[text()='']";
        private readonly By _findDistricts = By.CssSelector(".districts .modal-row");

        public ChoosingACityPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public MainPagePageObject EnterCityInSearchFieldAndPressSearchButton(string city, IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            webDriver.FindElement(_searchField).SendKeys(city);
            webDriver.FindElement(_citySearchButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject EnterCityInSearchFieldAndPressEnter(string city, IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            webDriver.FindElement(_searchField).SendKeys(city);
            new Actions(webDriver).KeyDown(Keys.Enter).Build().Perform();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject ClearSearchFieldByPressClearButton(string city, IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            webDriver.FindElement(_searchField).SendKeys(city);
            webDriver.FindElement(_clearSearchFieldButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject CloseCitySelectionPage(IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            webDriver.FindElement(_closePageButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public List<String> SearchPopularCitiesOnCitySelectionPage(IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            List<IWebElement> elementsCollection = webDriver.FindElements(_listPopularCities).ToList();
            List<String> popularCities = new List<String>();
            foreach (IWebElement element in elementsCollection)
            {
                popularCities.Add(element.Text);
            }

            return popularCities;
        }

        public MainPagePageObject SelectCityUsingList(String district, String region, String city, IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);
            Actions actions = new Actions(webDriver);

            String lineIndexOf = "text()=";
            webDriver.FindElement(By.XPath(_findDistrictAndRegion.Insert(_findDistrictAndRegion.IndexOf("'") + 1, district))).Click();
            webDriver.FindElement(By.XPath(_findDistrictAndRegion.Insert(_findDistrictAndRegion.IndexOf("'") + 1, region))).Click();
            webDriver.FindElement(By.XPath(_findCity.Insert(_findCity.IndexOf(lineIndexOf) + 8, city))).Click();

            return new MainPagePageObject(webDriver);
        }

        public List<String> FindDistricts(IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _findlistPopularCities);

            List<IWebElement> elementsCollection = webDriver.FindElements(_findDistricts).ToList();
            List<String> elements = new List<String>();
            foreach (IWebElement element in elementsCollection)
            {
                elements.Add(element.Text);
            }

            return elements;
        }
    }
}
