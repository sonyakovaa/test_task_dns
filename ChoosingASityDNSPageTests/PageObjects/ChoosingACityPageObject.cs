﻿using OpenQA.Selenium;
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

namespace ChoosingASityDNSPageTests.PageObjects
{
    // //div[text()="Ваш город"]
    // "//span[text()='Дальневосточный']"
    // div[class="base-modal__header-content"] svg[class="base-modal__header-close-icon dns-icon_remove"]
    class ChoosingACityPageObject
    {
        private readonly IWebDriver _webDriver;

        private readonly By _findDistrict = By.XPath("//span[text()='Дальневосточный']");
        private readonly By _findCityButton = By.CssSelector(".base-ui-input-search__input");
        private readonly By _performingCitySearchButton = By.CssSelector(".base-ui-input-search__icon" +
            ".base-ui-input-search__icon_search" +
            ".base-ui-input-search__icon-location_left");
        private readonly By _clearFieldButton = By.CssSelector(".base-ui-input-search__clear" +
            ".base-ui-input-search__clear_grey");
        private readonly By _closePageButton = By.CssSelector("div[class='base-modal__header-content'] " +
            "svg[class='base-modal__header-close-icon dns-icon_remove']");
        private readonly By _listPopularCities = By.CssSelector(".city-bubble");

        public ChoosingACityPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public MainPagePageObject FindCityField(string city)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findDistrict));

            _webDriver.FindElement(_findCityButton).SendKeys(city);
            _webDriver.FindElement(_performingCitySearchButton).Click();

            return new MainPagePageObject(_webDriver);
        }

        public MainPagePageObject FindCityPressEnter(string city)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findDistrict));
            Actions actions = new Actions(_webDriver);

            _webDriver.FindElement(_findCityButton).SendKeys(city);
            actions.KeyDown(Keys.Enter).Build().Perform();

            return new MainPagePageObject(_webDriver);
        }

        public MainPagePageObject ClearingSearchField(string city)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findDistrict));

            _webDriver.FindElement(_findCityButton).SendKeys(city);
            _webDriver.FindElement(_clearFieldButton).Click();

            return new MainPagePageObject(_webDriver);
        }

        public MainPagePageObject ClosingPage()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findDistrict));

            _webDriver.FindElement(_closePageButton).Click();

            return new MainPagePageObject(_webDriver);
        }

        public List<String> FindPopularCities()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_findDistrict));

            List<IWebElement> elementsCollection = _webDriver.FindElements(_listPopularCities).ToList();
            List<String> elements = new List<String>();
            foreach(IWebElement element in elementsCollection)
            {
                elements.Add(element.Text);
            }

            return elements;
        }

    }
}
