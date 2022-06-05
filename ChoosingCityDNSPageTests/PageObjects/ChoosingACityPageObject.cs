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

        private readonly By _findListCities = By.CssSelector(".big-cities-bubble-list");
        private readonly By _findCityButton = By.CssSelector(".base-ui-input-search__input");
        private readonly By _performingCitySearchButton = By.CssSelector(".base-ui-input-search__icon" +
            ".base-ui-input-search__icon_search" +
            ".base-ui-input-search__icon-location_left");
        private readonly By _clearFieldButton = By.CssSelector(".base-ui-input-search__clear" +
            ".base-ui-input-search__clear_grey");
        private readonly By _closePageButton = By.CssSelector("div[class='base-modal__header-content'] " +
            "svg[class='base-modal__header-close-icon dns-icon_remove']");
        private readonly By _listPopularCities = By.CssSelector(".city-bubble");
        private readonly String _findDistrictAndRegion = "//span[text()='']";
        private readonly String _findCity = "//ul[@class='cities']/li[@class='modal-row']/a[@href='javascript:']/span[text()='']";

        public ChoosingACityPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public MainPagePageObject FindCityField(string city, IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));

            webDriver.FindElement(_findCityButton).SendKeys(city);
            webDriver.FindElement(_performingCitySearchButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject FindCityPressEnter(string city, IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));
            Actions actions = new Actions(webDriver);

            webDriver.FindElement(_findCityButton).SendKeys(city);
            actions.KeyDown(Keys.Enter).Build().Perform();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject ClearingSearchField(string city, IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));

            webDriver.FindElement(_findCityButton).SendKeys(city);
            webDriver.FindElement(_clearFieldButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public MainPagePageObject ClosingPage(IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));

            webDriver.FindElement(_closePageButton).Click();

            return new MainPagePageObject(webDriver);
        }

        public List<String> FindPopularCities(IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));

            List<IWebElement> elementsCollection = webDriver.FindElements(_listPopularCities).ToList();
            List<String> elements = new List<String>();
            foreach(IWebElement element in elementsCollection)
            {
                elements.Add(element.Text);
            }

            return elements;
        }

        public MainPagePageObject SelectCityFromList (String district, String region, String city, IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findListCities));
            Actions actions = new Actions(webDriver);

            String lineIndexOf = "text()=";
            webDriver.FindElement(By.XPath(_findDistrictAndRegion.Insert(_findDistrictAndRegion.IndexOf("'") + 1, district))).Click();
            webDriver.FindElement(By.XPath(_findDistrictAndRegion.Insert(_findDistrictAndRegion.IndexOf("'") + 1, region))).Click();
            webDriver.FindElement(By.XPath(_findCity.Insert(_findCity.IndexOf(lineIndexOf) + 8, city))).Click();

            return new MainPagePageObject(webDriver);
        }

    }
}
