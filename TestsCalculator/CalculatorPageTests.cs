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
    public class CalculatorPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/";

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("test", "newyork1");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        //Check min amount
        [TestCase("1", "90", "300", "1.74", "0.74")]
        //Check max amount
        [TestCase("100000", "10", "20", "100547.95", "547.95")]
        //Check max percent
        [TestCase("1000", "99.9", "300", "1821.10", "821.10")]
        [TestCase("1000", "100", "300", "1821.92", "821.92")]
        //Check min percent
        [TestCase("1000", "0.1", "300", "1000.82", "0.82")]
        //Check max term
        [TestCase("1000", "90", "365", "1900.00", "900.00")]
        //Check min term
        [TestCase("10000", "90", "0.1", "10002.47", "2.47")]
        //Check invalid amount - min value
        [TestCase("0", "90", "300", "0.00", "0.00")]
        //Check invalid amount - max value
        [TestCase("100001", "30", "300", "0.00", "0.00")]
        //Check invalid percent - min value
        [TestCase("1000", "0", "300", "1000.00", "0.00")]
        //Check invalid percent - max value
        [TestCase("1000", "120", "300", "1000.00", "0.00")]
        //Сheck invalid term - min value
        [TestCase("1000", "90", "0", "1000.00", "0.00")]
        //Check invalid term - max value
        [TestCase("1000", "90", "366", "1000.00", "0.00")]
        public void FillForm(string amount, string percent, string term, string income, string interest)
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            calculatorPage.AmountField.SendKeys(amount);
            calculatorPage.PercentField.SendKeys(percent);
            calculatorPage.TermField.SendKeys(term);

            //Assert
            Assert.AreEqual(income, calculatorPage.IncomeField);
            Assert.AreEqual(interest, calculatorPage.InterestField);
        }

        [Test]
        public void CheckDefaultRadioBtnOption()
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
            calculatorPage.TermField.SendKeys("20");
            calculatorPage.DaysRadioBtn360.Click();

            //Assert
            Assert.AreEqual("1005.56", calculatorPage.IncomeField);
            Assert.AreEqual("5.56", calculatorPage.InterestField);
        }

        //select future date
        [TestCase("20", "7", "June", "2022", "27/06/2022")]
        //select past date
        [TestCase("20", "21", "March", "2010", "10/04/2010")]
        //end date is 1st day of the Month
        [TestCase("22", "10", "October", "2022", "01/11/2022")]
        //check Feb has 29 days in Leap Year
        [TestCase("1", "28", "February", "2024", "29/02/2024")]
        public void SelectTimePeriod(string term, string day, string month, string year, string endDate)
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            //Act
            calculatorPage.TermField.SendKeys(term);
            calculatorPage.Day.SelectByText(day);
            calculatorPage.Month.SelectByText(month);
            calculatorPage.Year.SelectByText(year);

            //Assert
            Assert.AreEqual(endDate, calculatorPage.EndDateField, "Date is incorrect");
        }

        [Test]
        public void CheckDaysInDropdown()
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
            SelectElement s = calculatorPage.Day;
            IList<string> actualDays = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualDays.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedDays, actualDays);
        }

        [Test]
        public void CheckMonthInDropdown()
        {
            //Arrange
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expectedMonths = new List<string> {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

            //Act 
            SelectElement s = calculatorPage.Month;

            IList<string> actualMonths = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualMonths.Add(s.Options.ElementAt(j).Text);
            }

            //Assert
            Assert.AreEqual(expectedMonths, actualMonths);
        }

        [Test]
        public void CheckYearInDropdown()
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
    }
}
