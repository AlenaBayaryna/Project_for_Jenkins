using OpenQA.Selenium;
using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace UnitTestProject3
{
    class DraftsPage: BaseEmailListPage
    {
        private readonly By draftsList = By.XPath("//div[@role='main']//tr");
        private readonly By lastDraft = By.XPath("//div[@role='main']//tr[1]");

        private WaitHelpers waiter;

        public DraftsPage(IWebDriver driver) : base(driver)
        {
            EmailListLocator = draftsList;
            this.waiter = new WaitHelpers(driver);
        }
      
        public void VerifyDraftSaved(string content)
        {
            var list = driver.FindElements(draftsList);
            Assert.That(list.Any(el => el.FindElement(lastDraft).Text.Contains(content)), Is.True,
                "Draft doesn't exist");
        }

        public void OpenSavedDraft()
        {
            OpenDraftsFolder();
            Thread.Sleep(1000);
            WaitForEmailList();
            driver.FindElement(lastDraft).Click();
        }
        
        public void VerifySentDraftAbsent(string content)
        {
            OpenDraftsFolder();
            var list = driver.FindElements(draftsList);
            Assert.That(list.Any(el => el.FindElement(lastDraft).Text.Equals(content)), Is.False,
                "Mail is still in 'Drafts’");
        }
    }
}
