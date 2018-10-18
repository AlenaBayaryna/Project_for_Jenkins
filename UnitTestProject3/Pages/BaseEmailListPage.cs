using OpenQA.Selenium;
using System.Linq;

namespace UnitTestProject3
{
    public class BaseEmailListPage : NavigationPage
    {
        public readonly By mailsList = By.XPath("//div[@role='main']//tr");
        public readonly By mailSubject = By.XPath(".//span[@class='bog']");
       
        public BaseEmailListPage(IWebDriver driver) : base(driver)
        {
        }
       
        public void WaitForEmailList()
        {
            new WaitHelpers(driver).UntilCustomCondition(driver => driver.FindElements(mailsList).Count > 0);
        }

        public bool IsListContainsEmail(string content)
        {
            var list = driver.FindElements(mailsList);
            return list.Any(el => el.FindElement(mailSubject).Text.Contains(content));
        }

        public void GetEmailContainsRequiredSubjectContent(string content)
        {
            var list = driver.FindElements(mailsList);
            list.First(el => el.FindElement(mailSubject).Text.Contains(content)).Click();
        }
    }
}
