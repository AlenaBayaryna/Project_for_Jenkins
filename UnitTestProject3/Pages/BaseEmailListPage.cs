using OpenQA.Selenium;

namespace UnitTestProject3
{
    public class BaseEmailListPage : NavigationPage
    {        
        public BaseEmailListPage(IWebDriver driver) : base(driver)
        {

        }
        public By EmailListLocator { get; set; }

        public void WaitForEmailList()
        {
            new WaitHelpers(driver).UntilCustomCondition(driver => driver.FindElements(EmailListLocator).Count > 0);
        }
    }
}
