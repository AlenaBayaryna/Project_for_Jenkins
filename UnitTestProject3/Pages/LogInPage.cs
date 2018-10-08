using OpenQA.Selenium;
using NUnit.Framework;

namespace UnitTestProject3
{
    class LogInPage : NavigationPage
    {
        private readonly By idTextBox = By.Id("identifierId");
        private readonly By idButton = By.Id("identifierNext");
        private readonly By passwordTextBox = By.Name("password");
        private readonly By passwordButton = By.Id("passwordNext");

        public LogInPage(IWebDriver driver) : base(driver)
        {
        }

        public void PerformLogin(string Email, string Password)
        {
            driver.FindElement(idTextBox).Clear();
            driver.FindElement(idTextBox).SendKeys(Email);
            driver.FindElement(idButton).Click();

            waiter.WaitClickableMethod(passwordTextBox);
            var passField = driver.FindElement(passwordTextBox);
            passField.Clear();
            passField.SendKeys(Password);

            waiter.WaitClickableMethod(passwordButton).Click();
          
        }
        public void VerifyLogIn(string UserName)
        {
            string actualAccountName = base.GetAccountName();
            StringAssert.Contains(UserName, actualAccountName, "Account username is incorrect");
        }
        public void VerifyLogOut()
        {
            Assert.That(driver.FindElement(passwordButton).Enabled, Is.True,
                "Draft doesn't exist");
        }
    }
}
