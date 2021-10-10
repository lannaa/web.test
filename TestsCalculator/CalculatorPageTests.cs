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

        [Test]
        public void PositiveFillForm()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));

            Assert.AreEqual("1005.48", incomeField.GetAttribute("value"));
            Assert.AreEqual("5.48", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveSelectRadioBtn()
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

        [Test]
        public void PositiveSelectDate()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");
            dayDropdown.SendKeys("7");
            monthDropdown.SendKeys("Jun");
            yearDropdown.SendKeys("2022");

            //Assert
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("27/06/2022", endDateField.GetAttribute("value"));
        }
        [Test]
        public void CheckBoundaryForDays()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("22");
            dayDropdown.SendKeys("10"); // why does not work for value 1
            monthDropdown.SendKeys("October");
            yearDropdown.SendKeys("2022");

            //Assert
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("01/11/2022", endDateField.GetAttribute("value"), "Date is incorrect");
        }

        [Test]
        public void CheckDaysInDropdown()
        {
            //Arrange
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));

            //Act
            SelectElement s = new SelectElement(dayDropdown);
            //get all options
            IList<IWebElement> els = s.Options;
            //count options
            int e = els.Count;
            Assert.Greater(e, 30);
            for (int j = 0; j < e; j++)
            {
               Console.WriteLine("Option at " + j + " is: " + els.ElementAt(j).Text);
            }
        }

        [Test]
        public void CheckMonthInDropdown()
        {
            //Arrange
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            //Act
            //object of SelectElement
            SelectElement s = new SelectElement(monthDropdown);
            //get all options
            IList<IWebElement> els = s.Options;
            //count options
            int e = els.Count;
            Assert.AreEqual(e, 12);
            for (int j = 0; j < e; j++)
            {
                Console.WriteLine("Option at " + j + " is: " + els.ElementAt(j).Text);
            }
        }

        [Test]
        public void CheckYearInDropdown()
        {
            //Arrange
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));
            //Act
            //object of SelectElement
            SelectElement s = new SelectElement(yearDropdown);
            //get all options
            IList<IWebElement> els = s.Options;
            //count options
            int e = els.Count;
            Assert.Greater(e, 15);
        }
    }
    }
