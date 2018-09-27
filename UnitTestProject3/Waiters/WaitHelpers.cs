using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace UnitTestProject3
{
    class WaitHelpers
    {
        private IWebDriver driver;
        public WaitHelpers(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void WaitClickableMethod(By elementLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
        }
        public void WaitVisibleMethod(By elementLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
        }
        public bool UntilCustomCondition(Func<IWebDriver, bool> condition)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            return wait.Until(condition);
        }
    }
}
