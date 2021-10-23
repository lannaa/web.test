using System;
using OpenQA.Selenium;

namespace TestsCalculator.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement LoginField
        {
            get
            {
                return _driver.FindElement(By.Id("login"));
            }
        }
        public IWebElement InfoMessages
        {
            get
            {
                return _driver.FindElement(By.Id("errorMessage"));
            }
        }

        public IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("loginBtn"));
        public IWebElement RemindPassBtn => _driver.FindElement(By.Id("remindBtn"));

        public void Login(string name, string password)
        {
            LoginField.SendKeys(name);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
