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
            IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect user name!", errorMessage.Text);
        }

        [Test]
        public void NegativeTestWrongPass()
        {
            // Arrange 
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElement(By.Id("loginBtn"));

            // Act
            loginFld.SendKeys("test");
            passFld.SendKeys("test");
            loginBtn.Click();

            // Assert
            IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect password!", errorMessage.Text);
        }

        [Test]
        public void NegativeTestWrongCredentials()
        {
        // Arrange
        IWebElement loginFld = driver.FindElement(By.Id("login"));
        IWebElement passFld = driver.FindElement(By.Id("password"));
        IWebElement loginBtn = driver.FindElement(By.Id("loginBtn"));

        // Act
        loginFld.SendKeys("testLogin");
        passFld.SendKeys("testPass");
        loginBtn.Click();

        // Assert
        IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
        Assert.IsTrue(errorMessage.Displayed);
        Assert.AreEqual("\'testLogin\' user doesn\'t exist!", errorMessage.Text);
        }

        [Test]
        public void PositiveTestRemindPassPresent()
        {
            // Arrange
            IWebElement remindBtn = driver.FindElement(By.Id("remindBtn"));

            // Act 
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));

            // Assert
            Assert.IsTrue(remindBtn.Displayed);
        }
    }
}