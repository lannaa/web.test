using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsCalculator.Pages;
using System.Threading;

namespace TestsCalculator
{
    public class CalculatorPageTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            OpenDriver();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("test", "newyork1");

            //set default values
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            SettingsPage settingsPage = new SettingsPage(driver);
            calculatorPage.SettingsBtn.Click();

            settingsPage.DateFormat.SelectByText("dd/MM/yyyy");
            settingsPage.NumberFormat.SelectByText("123 456 789.00");
            settingsPage.SaveBtn.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        //Check min amount
        [TestCase("1", "90", "300 ", "1.74", "0.74")]
        //Check max amount
        [TestCase("100000", "10", "20 ", "100 547.95", "547.95")]
        //Check max percent
        [TestCase("1000", "99.9", "300 ", "1 821.10", "821.10")]
        [TestCase("1000", "100", "300 ", "1 821.92", "821.92")]
        //Check min percent
        [TestCase("1000", "0.1", "300 ", "1 000.82", "0.82")]
        //Check max term
        [TestCase("1000", "90", "365 ", "1 900.00", "900.00")]
        //Check min term
        [TestCase("10000", "90", "0.1", "10 002.47", "2.47")]
        //Check invalid amount - min value
        [TestCase("0", "90", "300 ", "0.00", "0.00")]
        //Check invalid amount - max value
        [TestCase("100001", "30", "300 ", "0.00", "0.00")]
        //Check invalid percent - min value
        [TestCase("1000", "0", "300 ", "1 000.00", "0.00")]
        //Check invalid percent - max value
        [TestCase("1000", "120", "300 ", "1 000.00", "0.00")]
        //Сheck invalid term - min value
        [TestCase("1000", "90", "0", "1 000.00", "0.00")]
        //Check invalid term - max value
        [TestCase("1000", "90", "366 ", "1 000.00", "0.00")]
        public void FillFormTest(string amount, string percent, string term, string income, string interest)
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            calculatorPage.AmountField.SendKeys(amount);
            calculatorPage.PercentField.SendKeys(percent);
            calculatorPage.TermField.SendKeys(term);
            Thread.Sleep(10000);

            //Assert
            Assert.AreEqual(income, calculatorPage.Income);
            Assert.AreEqual(interest, calculatorPage.Interest);
        }

        [Test]
        public void CheckDefaultRadioBtnOptionTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Assert
            Assert.True(calculatorPage.DaysRadioBtn365.Selected);
        }

        [Test]
        public void SelectRadioBtn360()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            calculatorPage.AmountField.SendKeys("1000");
            calculatorPage.PercentField.SendKeys("10");
            calculatorPage.TermField.SendKeys("20 ");
            calculatorPage.DaysRadioBtn360.Click();
            // Thread.Sleep(1000);

            //Assert
            Assert.AreEqual("1 005.56", calculatorPage.Income);
            Assert.AreEqual("5.56", calculatorPage.Interest);
        }

        //select future date
        [TestCase("20", "7 June 2022", "27/06/2022")]
        //select past date
        [TestCase("20", "21 March 2010", "10/04/2010")]
        //end date is 1st day of the Month
        [TestCase("22", "10 October 2022", "01/11/2022")]
        //check Feb has 29 days in Leap Year
        [TestCase("1", "28 February 2024", "29/02/2024")]
        public void SelectTimePeriodTest(string term, string date, string endDate)
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            calculatorPage.TermField.SendKeys(term);
            calculatorPage.StartDate = date;

            //Assert
            Assert.AreEqual(endDate, calculatorPage.EndDate, "Date is incorrect");
        }

        [Test]
        public void CheckDaysInDropdownTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            IList<string> expectedDays = new List<string>();
            for (int j = 1; j < 32; j++)
            {
                expectedDays.Add(j.ToString());
            }

            //Act
            calculatorPage.Month.SelectByText("October");
            IList<string> actualDays = new List<string>();
            for (int j = 0; j < calculatorPage.Day.Options.Count; j++)
            {
                actualDays.Add(calculatorPage.Day.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedDays, actualDays);
        }

        [Test]
        public void CheckMonthInDropdownTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expectedMonths = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            //Act 
            SelectElement s = calculatorPage.Month;

            IList<string> actualMonths = new List<string>();
            for (int j = 0; j < calculatorPage.Month.Options.Count; j++)
            {
                actualMonths.Add(calculatorPage.Month.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedMonths, actualMonths);
        }

        [Test]
        public void CheckYearInDropdownTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            IList<string> expectedYears = new List<string>();
            for (int j = 2010; j < 2026; j++)
            {
                expectedYears.Add(j.ToString());
            }

            //Act 
            SelectElement s = calculatorPage.Year;
            IList<string> actualYears = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualYears.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedYears, actualYears);
        }

        [Test]
        public void CheckDefaultStartDateTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Assert
            Assert.AreEqual(DateTime.Today.ToString("d/M/yyyy"), calculatorPage.StartDate);
        }

        [Test]
        public void CheckSettingsBtnNavigationTest()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            SettingsPage settingsPage = new SettingsPage(driver);

            //Act
            calculatorPage.SettingsBtn.Click();

            //Assert
            Assert.IsTrue(settingsPage.IsOpened);
        }
    }
}
