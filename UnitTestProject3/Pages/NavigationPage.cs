using OpenQA.Selenium;

namespace UnitTestProject3
{
    public class NavigationPage
    {
        protected IWebDriver driver;
        internal WaitHelpers waiter;


        public NavigationPage(IWebDriver driver)
        {
            this.driver = driver;
            waiter = new WaitHelpers(driver);
        }

        private readonly By accountButton = By.XPath("//a[contains(@aria-label,'Google Account')][@role='button']");

        public readonly By accountUserName =
            By.XPath("//div[contains(@aria-label,'Account Information')]//div[@class='gb_Cb gb_Db']");

        private readonly By composeNewMessageButton = By.XPath("//div[@class='aic']//div[@role='button']");

        private readonly By draftsFolder = By.XPath("//a[@title[contains(.,'Drafts')]]");
        private readonly By sentMailsFolder = By.XPath("//a[@title[contains(.,'Sent')]]");

        private readonly By signOutButton = By.XPath("//a[contains(.,'Sign out')]");


        public string GetAccountName()
        {
            waiter.WaitClickableMethod(accountButton);
            driver.FindElement(accountButton).Click();
            return driver.FindElement(accountUserName).Text;
        }
        public void ClickComposeNewMailButton()
        {
            waiter.WaitClickableMethod(composeNewMessageButton);
            driver.FindElement(composeNewMessageButton).Click();
        }

        public void OpenDraftsFolder()
        {
            waiter.WaitVisibleMethod(draftsFolder);
            driver.FindElement(draftsFolder).Click();
        }

        public void OpenSentFolder()
        {
            driver.FindElement(sentMailsFolder).Click();
        }

        public void SignOutClickButton()
        {
            waiter.WaitClickableMethod(accountButton);
            driver.FindElement(accountButton).Click();
            waiter.WaitClickableMethod(signOutButton);
            driver.FindElement(signOutButton).Click();
        }
    }
}