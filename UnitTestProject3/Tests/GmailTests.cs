using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject3
{
    public class GmailTest
    {
        private IWebDriver driver;
        private const string UserName = "Alyona Testtask";
        private string recipientContent= "a.bayaryna@godeltech.com";
        private static string timeSpan = DateTime.UtcNow.ToString();
        private string subjectContent = "Message "+ timeSpan;
        private string bodyContent = "MessageBody "+ timeSpan;
     
        [SetUp]
        public void DriverInit()
        {
            driver = new ChromeDriver {Url = "http://gmail.com"};
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var login = new LogInPage(driver);
            login.ToSetUserLoginAndPassword();
        }

        [Test]
        public void LoginTest()
        {
            var login = new LogInPage(driver);
            login.VarifyLogInAction();
        }

        [Test]
        public void CreateMailAndSaveDraft()
        {
            var message = new NewMessagePage(driver);
            var draftsFolder = new DraftsPage(driver);

            message.CreateNewMail(recipientContent, subjectContent, bodyContent);
            string actualSubjectContent = message.GetSubject();
            message.CloseNewMessageWindowToSaveAsDraft();
            message.OpenDraftsFolder();
     
            draftsFolder.VerifyDraftSaved(actualSubjectContent);
        }

        [Test]
        public void CreateMailSaveDraftAndCheckDraftContent()
        {
            var message = new NewMessagePage(driver);
            var draftsFolder = new DraftsPage(driver);
            var draftmessage = new DraftMessagePage(driver);

            message.CreateNewMail(recipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();
           
            draftsFolder.OpenSavedDraft();
            draftmessage.VerifySavedDraftContent(recipientContent, subjectContent, bodyContent);
        }

        [Test]
        public void CreateMailSaveDraftAndSend()
        {
            var message = new NewMessagePage(driver);
            var draftsFolder = new DraftsPage(driver);
            var draftmessage = new DraftMessagePage(driver);
            var sentFolder = new SentPage(driver);

            message.CreateNewMail(recipientContent, subjectContent, bodyContent);
            string actualSubjectContent = message.GetSubject();
            message.CloseNewMessageWindowToSaveAsDraft();
         
            draftsFolder.OpenSavedDraft();

            draftmessage.ClickSendButton();

            draftsFolder.VerifySentDraftAbsent(actualSubjectContent);
            sentFolder.VerifySentMailPresent(actualSubjectContent);
        }

        [Test]
        public void LogoutTest()
        {
            var login = new LogInPage(driver);
            login.SignOutClickButton();
            login.VarifyLogOutAction();
        }

        [TearDown]
        public void DriverQuite()
        {
            driver.Quit();
        }
    }
}