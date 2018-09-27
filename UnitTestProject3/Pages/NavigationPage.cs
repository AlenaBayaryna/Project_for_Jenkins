using OpenQA.Selenium;


namespace UnitTestProject3
{
    public class NavigationPage
    {
        protected IWebDriver driver;
        private WaitHelpers waiter;

        public NavigationPage(IWebDriver driver)
        {
            this.driver = driver;
            this.waiter = new WaitHelpers(driver);
        }

        private By accountButton = By.XPath("//a[contains(@aria-label,'Google Account')][@role='button']");

        public By accountUserName =
            By.XPath("//div[contains(@aria-label,'Account Information')]//div[contains(.,'alyonatest')]");

        private By composeNewMessageButton = By.XPath("//div[@class='aic']//div[@role='button']");

        private By draftsFolder = By.XPath("//a[@title[contains(.,'Drafts')]]");
        private By sentMailsFolder = By.XPath("//a[@title[contains(.,'Sent')]]");

        private By signOutButton = By.XPath("//a[contains(.,'Sign out')]");

       
        public string ToGetAccountNameMethod()
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
            waiter.WaitClickableMethod(accountButton);
            driver.FindElement(signOutButton).Click();
        }
    }
}