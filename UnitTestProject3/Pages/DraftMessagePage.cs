using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace UnitTestProject3
{
    class DraftMessagePage : NavigationPage
    {
        private readonly By draftMessageRecipient = By.XPath("//input[@name='to']");
        private readonly By draftMessageFilledRecipient = By.XPath("//div[@class='aoD hl']");
        private readonly By draftMessageFilledRecipientAprooved = By.XPath("//div[@class='vT']");
        private readonly By draftMessageSubject = By.XPath("//input[@name='subject']");
        private readonly By draftMessageBody = By.XPath("//div[@aria-label='Message Body']");
        private readonly By draftMessageSendButton = By.XPath("//tbody//td[@class='gU Up']//div[@role='button']");
        private readonly By draftMessageDeleteButton = By.XPath("//div[@class='og T-I-J3']");

        public DraftMessagePage(IWebDriver driver) : base(driver)
        {
        }

        public void VerifyDraftSaved(string subjectContent)
        {
            waiter.UntilCustomCondition(driver => driver.FindElement(draftMessageSubject).Enabled);
            StringAssert.AreEqualIgnoringCase(subjectContent, GetSubject(), "Message subject is incorrect");
        }

        public void VerifySavedDraftContent(string recipientContent, string subjectContent, string bodyContent)
        {
            StringAssert.AreEqualIgnoringCase(recipientContent, GetRecipient(), "Message recipient is incorrect");
            StringAssert.AreEqualIgnoringCase(subjectContent, GetSubject(), "Message subject is incorrect");
            StringAssert.AreEqualIgnoringCase(bodyContent, GetBody(), "Message body is incorrect");
        }

        public string GetRecipient()
        {
            return driver.FindElement(draftMessageRecipient).GetAttribute("value");
        }

        public string GetSubject()
        {
            return driver.FindElement(draftMessageSubject).GetAttribute("value");
        }

        public string GetBody()
        {
            return driver.FindElement(draftMessageBody).Text;
        }

        public void ClickSendButton()
        {
            driver.FindElement(draftMessageFilledRecipient).Click();
            waiter.UntilCustomCondition(driver => driver.FindElement(draftMessageSendButton).Enabled);
            waiter.UntilCustomCondition(driver => driver.FindElement(draftMessageFilledRecipientAprooved).Enabled);
            driver.FindElement(draftMessageSendButton).Click();
        }
    }
}
