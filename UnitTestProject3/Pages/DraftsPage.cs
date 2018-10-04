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
            bool w = IsListContainsEmail(subjectContent);
            if (w != false)
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

        public bool IsListContainsEmail(string content)
        {
            var list = driver.FindElements(draftsList);
            return list.Any(el => el.FindElement(lastDraft).Text.Contains(content));
        }

        public bool IsSubjectEqual(string content)
        {
            var list = driver.FindElements(draftsList);
            return list.Any(el => el.FindElement(lastDraft).Text.Equals(content));
        }
    }
}
