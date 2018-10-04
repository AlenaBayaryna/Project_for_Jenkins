using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace UnitTestProject3
{
    class SentPage : BaseEmailListPage
    {
        private readonly By sentMailsList = By.XPath("//div[@role='main']//tr");
        private readonly By lastSentMail = By.XPath("//div[@role='main']//tr[1]//span[@class='bog']");
        private readonly By sentMailSubject = By.XPath("//div[@class='ha']/h2");

        public SentPage(IWebDriver driver) : base(driver)
        {
            EmailListLocator = sentMailsList;
        }

        public void VerifySentMailPresent(string content)
        {
            OpenSentFolder();
            WaitForEmailList();
            var list = driver.FindElements(sentMailsList);
            Assert.That(list.Any(el => el.FindElement(lastSentMail).Text.Contains(content)), Is.True,
                "Draft doesn't exist");
        }
    }
}
