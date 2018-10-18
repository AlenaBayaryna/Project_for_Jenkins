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
        private readonly static string timeSpan = DateTime.UtcNow.ToString();
        private readonly string subjectContent = "Message " + timeSpan;
        private readonly string bodyContent = "MessageBody " + timeSpan;

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

        [Test]
        public void LoginTest()
        {
            new LogInPage(driver).VerifyLogIn(UserName);
        }

        [Test]
        public void CreateMailAndSaveDraft()
        {
            var message = new NewMessagePage(driver);

            message.CreateNewMail(RecipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();

            var draftsFolder = new DraftsPage(driver);
            var draftsMessage = new DraftMessagePage(driver);

            draftsFolder.OpenSavedDraft(subjectContent);
            draftsMessage.VerifyDraftSaved(subjectContent);
            draftsFolder.OpenSavedDraft(subjectContent);
            draftsMessage.DeleteCreatedDraft();
        }

        [Test]
        public void CreateMailSaveDraftAndCheckDraftContent()
        {
            var message = new NewMessagePage(driver);
            message.CreateNewMail(RecipientContent, subjectContent, bodyContent);
            message.CloseNewMessageWindowToSaveAsDraft();

            var draftsFolder = new DraftsPage(driver);
            var draftsMessage = new DraftMessagePage(driver);

            draftsFolder.OpenSavedDraft(subjectContent);
            draftsMessage.VerifySavedDraftContent(RecipientContent, subjectContent, bodyContent);
            draftsFolder.OpenSavedDraft(subjectContent);
            draftsMessage.DeleteCreatedDraft();
        }

        [Test]
        public void CreateMailSaveDraftAndSend()
        {
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