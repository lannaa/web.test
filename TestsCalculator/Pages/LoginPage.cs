using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
        public IWebElement RemindPassBtn
        {
            get
            {
                By id = By.Id("remindBtn");
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
                    .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(id));
                return _driver.FindElement(id);
            }
        }

        public void Login(string name, string password)
        {
            LoginField.SendKeys(name);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
