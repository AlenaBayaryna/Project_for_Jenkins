using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace UnitTestProject3
{
    class DraftsPage : BaseEmailListPage
    {
        private readonly By draftsList = By.XPath("//div[@role='main']//tr");
        private readonly By lastDraft = By.XPath("//div[@role='main']//tr[1]");

        public DraftsPage(IWebDriver driver) : base(driver)
        {
            EmailListLocator = draftsList;
        }

        public void OpenSavedDraft(string subjectContent)
        {
            OpenDraftsFolder();
            Thread.Sleep(1000);
            WaitForEmailList();
            
            if (IsListContainsEmail(subjectContent,draftsList,lastDraft))
            {
                driver.FindElement(lastDraft).Click();
            }
            else throw new System.NullReferenceException("This draft doesn't exist");
        }

        public void VerifySentDraftAbsent(string content)
        {
            OpenDraftsFolder();
            Assert.IsFalse(IsSubjectEqual(content), "Mail is still in 'Drafts'");
        }

        public bool IsSubjectEqual(string content)
        {
            var list = driver.FindElements(draftsList);
            return list.Any(el => el.FindElement(lastDraft).Text.Equals(content));
        }
    }
}
