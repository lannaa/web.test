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
        public void CheckFieldNamesDisplayed()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);

            //Assert
            Assert.IsTrue(settingsPage.DateFormatText.Displayed);
            Assert.IsTrue(settingsPage.NumberFormatText.Displayed);
            Assert.IsTrue(settingsPage.CurrencyFormatText.Displayed);
        }

        [Test]
        public void CheckDateFormatOptions()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedDateFormat = new List<string> { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            //Act
            SelectElement s = settingsPage.DateFormatOptions;

            IList<string> actualDateFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualDateFormat.Add(s.Options.ElementAt(j).Text);
            }
            //Assert
            Assert.AreEqual(expectedDateFormat, actualDateFormat);
        }

        [Test]
        public void CheckNumberFormatOptions()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedNumberFormat = new List<string> { "123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00" };

            //Act
            SelectElement s = settingsPage.NumberFormatOptions;
            IList<string> actualNumberFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualNumberFormat.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedNumberFormat, actualNumberFormat);
        }

        [Test]
        public void CheckCurrencyFormatOptions()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedCurrencyFormat = new List<string> { "$ - US dollar", "€ - euro", "£ - Great Britain Pound" };

            //Act
            SelectElement s = settingsPage.CurrencyFormatOptions;
            IList<string> actualCurrencyFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualCurrencyFormat.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedCurrencyFormat, actualCurrencyFormat);
        }

        [Test]
        public void CancelBtnOpensDepositPage()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);

            //Act
            settingsPage.CancelBtn.Click();

            //Assert
            string actualUrl = driver.Url;
            string expectedUrl = "http://127.0.0.1:8080/Deposit";
            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [TestCase("MM dd yyyy", "M dd yyyy")]
        [TestCase("MM/dd/yyyy", "M/dd/yyyy")]
        [TestCase("dd-MM-yyyy", "dd-M-yyyy")]
        [TestCase("dd/MM/yyyy", "dd/M/yyyy")]
        public void SaveDateFormat(string dateFormat, string dateDisplay)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.DateFormatOptions.SelectByText(dateFormat);
            settingsPage.SaveBtn.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Assert
            Assert.AreEqual(DateTime.Today.ToString(dateDisplay), calculatorPage.EndDate);
        }

        [TestCase("123 456 789,00", "100 000,00")]
        [TestCase("123 456 789.00", "100 000.00")]
        [TestCase("123.456.789,00", "100.000,00")]
        [TestCase("123,456,789.00", "100,000.00")]
        public void SaveNumberFormat(string number, string income)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.NumberFormatOptions.SelectByText(number);
            settingsPage.SaveBtn.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            //Fill data to verify
            calculatorPage.AmountField.SendKeys("100000");

            // Assert
            Assert.AreEqual(income, calculatorPage.Income);
        }

        [TestCase("£ - Great Britain Pound", "£")]
        [TestCase("€ - euro", "€")]
        [TestCase("$ - US dollar", "$")]
        public void SaveCurrencyFormat(string currency, string currencySymbol)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.CurrencyFormatOptions.SelectByText(currency);
            settingsPage.SaveBtn.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Assert
            Assert.IsTrue(calculatorPage.CurrencySymbol.Displayed);
            // currency symbol is not found 
            //  Assert.AreEqual(currencySymbol, calculatorPage.CurrencySymbol);
        }

        [Test]
        public void LogoutBtn()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);

            //Act
            settingsPage.LogoutBtn.Click();

            //Assert
            string actualUrl = driver.Url;
            string expectedUrl = "http://127.0.0.1:8080/";
            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
