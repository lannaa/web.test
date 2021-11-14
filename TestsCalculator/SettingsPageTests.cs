using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using TestsCalculator.Pages;


namespace TestsCalculator
{
    public class SettingsPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/";

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("test", "newyork1");

            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.SettingsBtn.Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void CheckDateFormatOptions()
        {
            // Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedDateFormat = new List<string> { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            // Act
            SelectElement s = settingsPage.DateFormatOptions;

            IList<string> actualDateFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualDateFormat.Add(s.Options.ElementAt(j).Text);
            }
            // Assert
            Assert.AreEqual(expectedDateFormat, actualDateFormat);

        }

        [Test]
        public void CheckNumberFormatOptions()
        {
            // Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedNumberFormat = new List<string> { "123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00" };

            // Act
            SelectElement s = settingsPage.NumberFormatOptions;
            IList<string> actualNumberFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualNumberFormat.Add(s.Options.ElementAt(j).Text);
            }

            // Assert
            Assert.AreEqual(expectedNumberFormat, actualNumberFormat);
        }

        [Test]
        public void CheckCurrencyFormatOptions()
        {
            // Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedCurrencyFormat = new List<string> {"$ - US dollar", "€ - euro", "£ - Great Britain Pound"};

            // Act
            SelectElement s = settingsPage.CurrencyFormatOptions;
            IList<string> actualCurrencyFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualCurrencyFormat.Add(s.Options.ElementAt(j).Text);
            }

            // Assert
            Assert.AreEqual(expectedCurrencyFormat, actualCurrencyFormat);
        }

        [Test]
        public void CancelBtnOpensDepositPage()
        {
            // Arrange
            SettingsPage settingsPage = new SettingsPage(driver);

            // Act
            settingsPage.CancelBtn.Click();

            // Assert
            string actualUrl = driver.Url;
            string expectedUrl = "http://127.0.0.1:8080/Deposit";
            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
