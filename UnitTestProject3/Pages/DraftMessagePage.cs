using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject3
{
    class DraftMessagePage:NavigationPage
    {
        private readonly By draftMessageRecipient = By.XPath("//input[@name='to']");
        private readonly By draftMessageSubject = By.XPath("//input[@name='subject']");
        private readonly By draftMessageBody = By.XPath("//div[@aria-label='Message Body']");
        private readonly By draftMessageSendButton = By.XPath("//tbody//td[@class='gU Up']//div[@role='button']");

        private WaitHelpers waiter;
        public DraftMessagePage(IWebDriver driver) : base(driver)
        {
            this.waiter = new WaitHelpers(driver);
        }

        public void VerifySavedDraftContent( string recipientContent,  string subjectContent, string bodyContent)
        {
            Assert.AreEqual(recipientContent, GetRecipient(), "Message recipient is incorrect");
            Assert.AreEqual(subjectContent, GetSubject(), "Message subject is incorrect");
            Assert.AreEqual(bodyContent, GetBody(), "Message body is incorrect");
        }
        public string  GetRecipient()
        {
            return driver.FindElement(draftMessageRecipient).GetAttribute("value");
        }
        public string  GetSubject()
        {
            return driver.FindElement(draftMessageSubject).GetAttribute("value");
        }
        public string  GetBody()
        {
            return driver.FindElement(draftMessageBody).Text;
        }

        public void ClickSendButton()
        {
            waiter.UntilCustomCondition(driver => driver.FindElement(draftMessageRecipient).Enabled);
            driver.FindElement(draftMessageSendButton).Click();
        }
    }
}
