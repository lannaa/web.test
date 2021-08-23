using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestsCalculator
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PositiveTest()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // Act 
            loginFld.SendKeys("test");
            passFld.SendKeys("newyork1");
            loginBtn.Click();

            // Assert
            string actualUrl = driver.Url;
            string expectedUrl = "http://localhost:64177/Deposit";
            Assert.AreEqual(expectedUrl, actualUrl);
            driver.Close();
        }

        [Test]
        public void NegativeTestWrongName()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // Act
            loginFld.SendKeys("negativeTest");
            passFld.SendKeys("newyork1");
            loginBtn.Click();

            // Assert
            IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect user name!", errorMessage.Text);
            driver.Close();
        }

        [Test]
        public void NegativeTestWrongPass()
        {
            // Arrange 
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/login";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // Act
            loginFld.SendKeys("test");
            passFld.SendKeys("test");
            loginBtn.Click();

            // Assert
            IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect password!", errorMessage.Text);
            driver.Close();
        }

        [Test]
        public void NegativeTestWrongCredentials()
        {
        // Arrange
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Url = "http://localhost:64177/login";
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
        driver.Close();
        }

        [Test]
        public void PositiveTestRemindPassPresent()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/login";
            IWebElement remindBtn = driver.FindElement(By.Id("remind"));

            // Act 
            // remindBtn.Click();

            // Assert
            // IWebElement remindForm = driver.FindElement(By.Id("remindPasswordView"));
            // Assert.IsTrue(remindForm.Displayed);
            Assert.IsTrue(remindBtn.Displayed);
            driver.Close();
        }
    }
}