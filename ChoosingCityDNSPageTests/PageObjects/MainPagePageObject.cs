using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ChoosingCityDNSPageTests.PageObjects
{
    class MainPagePageObject
    {
        private IWebDriver _webDriver;

        private readonly By _cityButton = By.CssSelector(".city-select__text");
        private readonly By _choosingCityPage = By.CssSelector(".select-lists");

        public MainPagePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public ChoosingACityPageObject ChoosingCity()
        {
            _webDriver.FindElement(_cityButton).Click();

            return new ChoosingACityPageObject(_webDriver);
        }

        public string GetCity()
        {
            string nameCity = _webDriver.FindElement(_cityButton).Text;
            return nameCity;
        }

        public bool CheckingCitySelectionPage()
        {
            try
            {
                _webDriver.FindElement(_choosingCityPage);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }
    }
}
