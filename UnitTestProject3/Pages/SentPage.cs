using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace UnitTestProject3
{
    class SentPage : BaseEmailListPage
    {
        public SentPage(IWebDriver driver) : base(driver)
        {
        }

        public void VerifySentMailPresent(string content)
        {
            OpenSentFolder();
            WaitForEmailList();
            Thread.Sleep(1000);
            Assert.That((IsListContainsEmail(content)), Is.True,
                "Sent mail doesn't exist");
        }
    }
}
