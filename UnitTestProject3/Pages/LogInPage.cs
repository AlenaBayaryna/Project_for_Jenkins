using OpenQA.Selenium;
using NUnit.Framework;

namespace UnitTestProject3
{
    class LogInPage : NavigationPage
    {
        private By idTextBox = By.Id("identifierId");
        private By idButton = By.Id("identifierNext");
        private By passwordTextBox = By.Name("password");
        private By passwordButton = By.Id("passwordNext");

        private const string UserName = "Alyona Testtask";
        private WaitHelpers waiter;

        public LogInPage(IWebDriver driver) : base(driver)
        {
          this.waiter = new WaitHelpers(driver);
        }
        
        public void ToSetUserLoginAndPassword()
        {
            driver.FindElement(idTextBox).Clear();
            driver.FindElement(idTextBox).SendKeys("alyonatest456");
            driver.FindElement(idButton).Click();

            waiter.WaitClickableMethod(passwordTextBox);
            IWebElement passField = driver.FindElement(passwordTextBox);
            passField.Clear();
            passField.SendKeys("Test4567");

            waiter.WaitClickableMethod(passwordTextBox);
            IWebElement passButton = driver.FindElement(passwordButton);
            passButton.Click();
        }
        public void VarifyLogInAction()
        {
            string actualAccountName = base.ToGetAccountNameMethod();
            StringAssert.Contains(UserName, actualAccountName, "Acccount username is incorrect");
        }
        public void VarifyLogOutAction()
        {
            Assert.That(driver.FindElement(passwordButton).Enabled, Is.True,
                "Draft doesn't exist");
        }
    }
}
