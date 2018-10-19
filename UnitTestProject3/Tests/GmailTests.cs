using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject3
{
    [TestFixture]
    public class GmailTest
    {
        private IWebDriver driver;
        private const string UserName = "Alyona Testtask";
        private const string RecipientContent = "a.bayaryna@godeltech.com";
        private const string Email = "alyonatest456";
        private const string Password = "Test4567";
        
        [SetUp]
        public void DriverInit()
        {
            driver = new ChromeDriver { Url = "http://gmail.com" };
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            new LogInPage(driver).PerformLogin(Email, Password);
        }

        public string GenerateUniqSubjectContent()
        {
            Guid guidName = Guid.NewGuid();
            return "Message " + guidName;
        }

        public string GenerateUniqBodyContent()
        {
            Guid guidName = Guid.NewGuid();
            return "MessageBody " + guidName;
        }

        [Test]
        public void LoginTest()
        {
            new LogInPage(driver).VerifyLogIn(UserName);
        }

        [Test]
        public void CreateMailAndSaveDraft()
        {
            string subjectContent = GenerateUniqSubjectContent();
            string bodyContent = GenerateUniqBodyContent();

            var message = new NewMessagePage(driver);
            message.CreateNewMail(RecipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();

            new DraftsPage(driver).OpenSavedDraft(subjectContent);
            new DraftMessagePage(driver).VerifyDraftSaved(subjectContent);
        }

        [Test]
        public void CreateMailSaveDraftAndCheckDraftContent()
        {
            string subjectContent = GenerateUniqSubjectContent();
            string bodyContent = GenerateUniqBodyContent();

            var message = new NewMessagePage(driver);
            message.CreateNewMail(RecipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();
            
            new DraftsPage(driver).OpenSavedDraft(subjectContent);
            new DraftMessagePage(driver).VerifySavedDraftContent(RecipientContent, subjectContent, bodyContent);
        }

        [Test]
        public void CreateMailSaveDraftAndSend()
        {
            string subjectContent = GenerateUniqSubjectContent();
            string bodyContent = GenerateUniqBodyContent();
            var message = new NewMessagePage(driver);
            message.CreateNewMail(RecipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();

            var draftsFolder = new DraftsPage(driver);
            draftsFolder.OpenSavedDraft(subjectContent);
            new DraftMessagePage(driver).ClickSendButton();

            draftsFolder.VerifySentDraftAbsent(subjectContent);
            var sentFolder = new SentPage(driver);
            sentFolder.VerifySentMailPresent(subjectContent);
        }

        [Test]
        public void LogoutTest()
        {
            var login = new LogInPage(driver);
            login.SignOutClickButton();
            login.VerifyLogOut();
        }

        [TearDown]
        public void DriverQuit()
        {
            driver.Quit();
        }
    }
}