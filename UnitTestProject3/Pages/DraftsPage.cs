using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace UnitTestProject3
{
    class DraftsPage : BaseEmailListPage
    {
        public DraftsPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenSavedDraft(string subjectContent)
        {
            OpenDraftsFolder();
            Thread.Sleep(1000);
            WaitForEmailList();
            GetEmailContainsRequiredSubjectContent(subjectContent);
        }

        public void VerifySentDraftAbsent(string content)
        {
            OpenDraftsFolder();
            Assert.IsFalse(IsSubjectEqual(content), "Mail is still in 'Drafts'");
        }

        public bool IsSubjectEqual(string content)
        {
            var list = driver.FindElements(mailsList);
            return list.Any(el => el.FindElement(mailSubject).Text.Equals(content));
        }
    }
}
