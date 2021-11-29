using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using TestsCalculator.Pages;

namespace TestsCalculator
{
    public class SettingsPageTests : BaseTests
    {
        [SetUp]
        public void SetUp()
        {
            OpenDriver();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("test", "newyork1");

            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.SettingsBtn.Click();
        }

        [Test]
        public void CheckDateFormatTest()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedDateFormat = new List<string> { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            //Act
            IList<string> actualDateFormat = new List<string>();
            for (int j = 0; j < settingsPage.DateFormat.Options.Count; j++)
            {
                actualDateFormat.Add(settingsPage.DateFormat.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedDateFormat, actualDateFormat);
            Assert.AreEqual("Date format:", settingsPage.DateText);
        }

        [Test]
        public void CheckNumberFormatTest()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedNumberFormat = new List<string> { "123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00" };

            //Act
            IList<string> actualNumberFormat = new List<string>();
            for (int j = 0; j < settingsPage.NumberFormat.Options.Count; j++)
            {
                actualNumberFormat.Add(settingsPage.NumberFormat.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedNumberFormat, actualNumberFormat);
            Assert.AreEqual("Number format:", settingsPage.NumberText);
        }

        [Test]
        public void CheckCurrencyFormatTest()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            List<string> expectedCurrencyFormat = new List<string> { "$ - US dollar", "€ - euro", "£ - Great Britain Pound" };

            //Act
            SelectElement s = settingsPage.Currency;
            IList<string> actualCurrencyFormat = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualCurrencyFormat.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedCurrencyFormat, actualCurrencyFormat);
            Assert.AreEqual("Default currency:", settingsPage.CurrencyText);
        }

        [Test]
        public void CancelBtnOpensDepositPageTest()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.CancelBtn.Click();

            //Assert
            Assert.IsTrue(calculatorPage.IsOpened);
        }

        [TestCase("MM dd yyyy")]
        [TestCase("MM/dd/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("dd/MM/yyyy")]
        public void SaveDateFormatTest(string dateFormat)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.DateFormat.SelectByText(dateFormat);
            settingsPage.ClickSaveBtn();
            //Assert
            Assert.AreEqual(DateTime.Today.ToString(dateFormat), calculatorPage.EndDate);
        }

        [TestCase("123 456 789,00", "102 739,73")]
        [TestCase("123 456 789.00", "102 739.73")]
        [TestCase("123.456.789,00", "102.739,73")]
        [TestCase("123,456,789.00", "102,739.73")]
        public void SaveNumberFormatTest(string number, string income)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.NumberFormat.SelectByText(number);
            settingsPage.ClickSaveBtn();
            calculatorPage.AmountField.SendKeys("100000");
            calculatorPage.PercentField.SendKeys("10");
            calculatorPage.TermField.SendKeys("100");
            calculatorPage.ClickCalculateBtn();

            // Assert
            Assert.AreEqual(income, calculatorPage.Income);
        }

        [TestCase("£ - Great Britain Pound", "£")]
        [TestCase("€ - euro", "€")]
        [TestCase("$ - US dollar", "$")]
        public void SaveCurrencyFormatTest(string currency, string currencySymbol)
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            settingsPage.Currency.SelectByText(currency);
            settingsPage.ClickSaveBtn();

            //Assert
            Assert.AreEqual(currencySymbol, calculatorPage.CurrencySymbol);
        }

        [Test]
        public void LogoutTest()
        {
            //Arrange
            SettingsPage settingsPage = new SettingsPage(driver);
            LoginPage loginPage = new LoginPage(driver);

            //Act
            settingsPage.LogoutBtn.Click();

            //Assert
            Assert.IsTrue(loginPage.IsOpened);
        }
    }
}
