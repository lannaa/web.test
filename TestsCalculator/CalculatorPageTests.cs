using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

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

            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.Id("loginBtn")).Click();
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
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys(amount);
            percentField.SendKeys(percent);
            termField.SendKeys(term);

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));

            Assert.AreEqual(income, incomeField.GetAttribute("value"));
            Assert.AreEqual(interest, interestField.GetAttribute("value"));
        }

        [Test]
        public void CheckDefaultRadioBtnOption()
        {
            //Arrange
            IWebElement daysRadioBtn365 = driver.FindElement(By.Id("d365"));

            //Assert
            Assert.True(daysRadioBtn365.Selected);
        }

        [Test]
        public void SelectRadioBtn360()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement daysRadioBtn360 = driver.FindElement(By.Id("d360"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");
            daysRadioBtn360.Click();

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1005.56", incomeField.GetAttribute("value"));
            Assert.AreEqual("5.56", interestField.GetAttribute("value"));
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
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            termField.SendKeys(term);
            new SelectElement(dayDropdown).SelectByText(day);
            new SelectElement(monthDropdown).SelectByText(month);
            new SelectElement(yearDropdown).SelectByText(year);

            //Assert
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual(endDate, endDateField.GetAttribute("value"), "Date is incorrect");
        }

        [Test]
        public void CheckDaysInDropdown()
        {
            //Arrange
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IList<string> expectedDays = new List<string>();
            for (int j = 1; j < 32; j++)
            {
                expectedDays.Add(j.ToString());
            }

            //Act
            new SelectElement(monthDropdown).SelectByText("October");
            SelectElement s = new SelectElement(dayDropdown);
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
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            List<string> expectedMonths = new List<string> {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

            //Act 
            SelectElement s = new SelectElement(monthDropdown);
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
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));
            IList<string> expectedYears = new List<string>();
            for (int j = 2010; j < 2026; j++)
            {
                expectedYears.Add(j.ToString());
            }

            //Act 
            SelectElement s = new SelectElement(yearDropdown);
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
