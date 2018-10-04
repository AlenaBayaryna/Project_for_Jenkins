﻿using NUnit.Framework;
using OpenQA.Selenium;

namespace UnitTestProject3
{
    class DraftMessagePage : NavigationPage
    {
        private readonly By draftMessageRecipient = By.XPath("//input[@name='to']");
        private readonly By draftMessageSubject = By.XPath("//input[@name='subject']");
        private readonly By draftMessageBody = By.XPath("//div[@aria-label='Message Body']");
        private readonly By draftMessageSendButton = By.XPath("//tbody//td[@class='gU Up']//div[@role='button']");

        public DraftMessagePage(IWebDriver driver) : base(driver)
        {
        }

        public void VerifyDraftSaved(string subjectContent)
        {
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
            waiter.UntilCustomCondition(driver => driver.FindElement(draftMessageSendButton).Enabled);
            driver.FindElement(draftMessageSendButton).Click();
        }
    }
}
