using OpenQA.Selenium;
using System.Linq;

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
        public bool IsListContainsEmail(string content, By itemsList, By lastItem)
        {
            var list = driver.FindElements(itemsList);
            return list.Any(el => el.FindElement(lastItem).Text.Contains(content));
        }
    }
}
