using System;
using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestsCalculator.Pages;

namespace TestsCalculator
{
    public class LoginPageTests
    {
        private IWebDriver driver;
        private string BaseUrl => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["BaseUrl"].Value;

        [SetUp]
        public void Setup()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;

            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            driver = new ChromeDriver(chromeDriverService, options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = BaseUrl;
        }

        [TearDown]
        public void TearDown()
        {
           driver.Close();
        }

        [Test]
        public void PositiveTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Act
            loginPage.Login("test", "newyork1");

            // Assert
            string actualUrl = driver.Url;
            string expectedUrl = $"{BaseUrl}/Deposit";
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void NegativeTestWrongName()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Act
            loginPage.LoginField.SendKeys("negativeTest");
            loginPage.PasswordField.SendKeys("newyork1");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("Incorrect user name!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void NegativeTestWrongPass()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Act
            loginPage.LoginField.SendKeys("test");
            loginPage.PasswordField.SendKeys("test");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("Incorrect password!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void NegativeTestWrongCredentials()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);
   

            // Act
            loginPage.LoginField.SendKeys("testLogin");
            loginPage.PasswordField.SendKeys("testPass");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("\'testLogin\' user doesn\'t exist!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void PositiveTestRemindPassPresent()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Assert
            Assert.IsTrue(loginPage.RemindPassBtn.Displayed);
        }
    }
}