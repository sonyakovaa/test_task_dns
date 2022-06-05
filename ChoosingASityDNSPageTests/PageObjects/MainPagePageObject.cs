using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ChoosingASityDNSPageTests.PageObjects
{
    class MainPagePageObject
    {
        private IWebDriver _webDriver;

        private readonly By _choosingCityButton = By.CssSelector(".city-select__text");
        private readonly By _nameCity = By.CssSelector(".city-select__text");
        private readonly By _choosingCityPage = By.CssSelector(".select-lists");

        public MainPagePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public ChoosingACityPageObject choosingCity()
        {
            _webDriver.FindElement(_choosingCityButton).Click();

            return new ChoosingACityPageObject(_webDriver);
        }

        public string getCity()
        {
            string nameCity = _webDriver.FindElement(_nameCity).Text;
            return nameCity;
        }

        public bool checkingCitySelectionPage()
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
