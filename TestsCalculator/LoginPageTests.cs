using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TestsCalculator.Pages;

namespace TestsCalculator
{
    public class LoginPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/";
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
            string expectedUrl = "http://127.0.0.1:8080/Deposit";
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

            // Act 
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));
            // why loginPage.RemindPassBtn is not accepted 

            // Assert
            Assert.IsTrue(loginPage.RemindPassBtn.Displayed);
        }
    }
}