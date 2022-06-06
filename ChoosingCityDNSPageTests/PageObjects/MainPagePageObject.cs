using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        public ChoosingACityPageObject ChoosingCity(IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_cityButton));

            webDriver.FindElement(_cityButton).Click();

            return new ChoosingACityPageObject(webDriver);
        }

        public string GetCity(IWebDriver webDriver)
        {
            string nameCity = webDriver.FindElement(_cityButton).Text;
            return nameCity;
        }

        public bool CheckingCitySelectionPage(IWebDriver webDriver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_cityButton));

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
            Thread.Sleep(1000);

            String cookies = webDriver.Manage().Cookies.GetCookieNamed("city_path").ToString();
            String cookieCity = cookies.Remove(cookies.IndexOf(";")).Substring(10);
            return cookieCity;
        }
    }
}
