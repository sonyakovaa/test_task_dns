using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoosingCityDNSPageTests
{
    public class LocalWebDriver
    {
        public static IWebDriver CreateLocalWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("-no-sandbox");

            IWebDriver webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            // webDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            webDriver.Navigate().GoToUrl("https://www.dns-shop.ru/");
            webDriver.Manage().Window.Maximize();

            return webDriver;
        }
    }
}
