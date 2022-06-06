using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoosingCityDNSPageTests
{
    public static class WaitUntil
    {
        public static void WaitElement(IWebDriver webDriver, By locator, int seconds = 180)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch(WebDriverTimeoutException exception)
            {
                throw new NotFoundException($"The program could not find the transmitted locator: {locator}", exception);
            }
        }

    }
}
