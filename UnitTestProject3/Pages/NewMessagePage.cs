using NUnit.Framework;
using OpenQA.Selenium;

namespace UnitTestProject3
{
    class NewMessagePage : NavigationPage
    {
        private By newMessageWindow = By.XPath("//div[@role='dialog']");
        private By newMessageWindowCloseButton = By.XPath("//img[@aria-label='Save & Close']");
        
        private By newMessageRecipient = By.XPath("//textarea[@name='to']");
        private By newMessageSubject = By.XPath("//input[@name='subjectbox']");
        private By newMessageBody = By.XPath("//div[@aria-label='Message Body']");

        private By newMessageSendButton = By.Id(":6");
        private WaitHelpers waiter;

        public NewMessagePage(IWebDriver driver) : base(driver)
        {
            this.waiter = new WaitHelpers(driver);
        }
        
        public void CreateNewMail(string recipientContent, string subjectContent, string bodyContent)//U
        {
            ClickComposeNewMailButton();
            waiter.WaitClickableMethod(newMessageWindow);
            driver.FindElement(newMessageRecipient).SendKeys(recipientContent);
            driver.FindElement(newMessageSubject).SendKeys(subjectContent);
            driver.FindElement(newMessageBody).SendKeys(bodyContent);
        }
       
        public string  GetRecipient()
        {
            waiter.WaitClickableMethod(newMessageWindow);
            driver.FindElement(newMessageRecipient).Click();
            return driver.FindElement(newMessageRecipient).Text;
        }
        public string  GetSubject()
        {
            waiter.WaitClickableMethod(newMessageWindow);
            driver.FindElement(newMessageSubject).Click();
            return driver.FindElement(newMessageSubject).Text;
        }
        public string  GetBody()
        {
            waiter.WaitClickableMethod(newMessageWindow);
            return driver.FindElement(newMessageBody).Text;
        }
        
        public void CloseNewMessageWindowToSaveAsDraft()
        {
            waiter.WaitClickableMethod(newMessageWindowCloseButton);
            driver.FindElement(newMessageWindowCloseButton).Click();
        }

        public void ClickSendButton()
        {
            waiter.WaitClickableMethod(newMessageSendButton);
            driver.FindElement(newMessageSendButton).Click();
        }
    }
}
