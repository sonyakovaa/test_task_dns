using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace ChoosingCityDNSPageTests.PageObjects
{
    class MainPagePageObject
    {
        private readonly IWebDriver _webDriver;

        private readonly By _cityButton = By.CssSelector(".city-select__text");
        private readonly By _choosingCityPage = By.CssSelector(".select-lists");

        public MainPagePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public ChoosingACityPageObject OpenCitySelectPage(IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _cityButton);
            webDriver.FindElement(_cityButton).Click();

            return new ChoosingACityPageObject(webDriver);
        }

        public string GetCity(IWebDriver webDriver)
        {
            WaitUntil.WaitElement(webDriver, _cityButton);
            string nameCity = webDriver.FindElement(_cityButton).Text;

            return nameCity;
        }

        public bool CheckingOpenCitySelectPage(IWebDriver webDriver)
        {
            try
            {
                WaitUntil.WaitElement(webDriver, _cityButton);
                webDriver.FindElement(_choosingCityPage);

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public String GetCityCookie(IWebDriver webDriver)
        {
            Thread.Sleep(3000);

            String cookies = webDriver.Manage().Cookies.GetCookieNamed("city_path").ToString();
            string cookieCity = cookies.Remove(cookies.IndexOf(";")).Substring(10);

            return cookieCity;
        }
    }
}
